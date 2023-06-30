using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebApp.Core
{
   public class Menu
   {
      private static Dictionary<string, Dictionary<string, Function>> _functions;
      /// <summary>
      /// Get all defined functions and their logics in all modules, combine in all class
      /// </summary>
      public static Dictionary<string, Dictionary<string, Function>> Functions
      {
         get
         {
            if (_functions == null)
            {
               Logger.Info("Load functions defined");
               _functions = new Dictionary<string, Dictionary<string, Function>>();
               var asm = from a in AppDomain.CurrentDomain.GetAssemblies()
                         where a.ManifestModule.Name == "WebApp.dll" || a.ManifestModule.Name.StartsWith("WebModule.")
                         select a;
               Logger.Info("Loaded: " + string.Join(", ", asm.Select(p => p.ManifestModule.Name)));
               foreach (var a in asm)
               {
                  var module = new Dictionary<string, Function>();
                  _functions[a.ManifestModule.Name] = module;

                  var types = a.DefinedTypes.Where(p => p.IsDefined(typeof(FunctionAttribute), false));
                  foreach (var type in types)
                  {
                     var att = (FunctionAttribute)type.GetCustomAttributes(typeof(FunctionAttribute), false)[0];
                     if (!module.TryGetValue(att.Name, out Function f))
                        module[att.Name] = new Function(type, att);
                     else
                        f.Update(type, att);
                  }

               }
            }
            return _functions;
         }
      }

      public class Function
      {
         public string Name { get; private set; }
         public string ControllerName { get; private set; }
         public List<string> Roles { get; private set; }
         public List<string> Users { get; private set; }
         public List<string> Logics { get; private set; }

         public Function(TypeInfo type, FunctionAttribute att)
         {
            Name = att.Name;
            ControllerName = type.Name.Replace("Controller", "");
            Roles = new List<string>();
            Users = new List<string>();
            Logics = new List<string>();
            if (!string.IsNullOrEmpty(att.Roles))
               Roles.AddRange(att.Roles.Split(','));
            if (!string.IsNullOrEmpty(att.Users))
               Users.AddRange(att.Users.Split(','));

            var logicAttrs = type.GetMethods().SelectMany(p => p.GetCustomAttributes(typeof(LogicAttribute), false).OfType<LogicAttribute>()).Union(type.GetCustomAttributes<LogicAttribute>());
            foreach (var l in logicAttrs)
               if (!Logics.Contains(l.Name))
                  Logics.Add(l.Name);
         }

         public void Update(TypeInfo type, FunctionAttribute att)
         {
            if (!string.IsNullOrEmpty(att.Roles))
            {
               foreach (var s in att.Roles.Split(','))
                  if (!Roles.Contains(s))
                     Roles.Add(s);
            }
            if (!string.IsNullOrEmpty(att.Users))
            {
               foreach (var s in att.Users.Split(','))
                  if (!Users.Contains(s))
                     Users.Add(s);
            }
            var logicAttrs = type.GetMethods().SelectMany(p => p.GetCustomAttributes(typeof(LogicAttribute), false).OfType<LogicAttribute>()).Union(type.GetCustomAttributes<LogicAttribute>());
            foreach (var l in logicAttrs)
               if (!Logics.Contains(l.Name))
                  Logics.Add(l.Name);
         }
      }

      public class TabItem
      {
         public string Name { get; set; }
         public List<GroupItem> Groups { get; private set; }

         public TabItem()
         {
            Groups = new List<GroupItem>();
         }
      }

      public class GroupItem
      {
         public string Name { get; set; }
         public string Image { get; set; }
         public string Icon { get; set; }
         public List<FunctionItem> Functions { get; private set; }

         public GroupItem()
         {
            Functions = new List<FunctionItem>();
         }
      }

      public class FunctionItem
      {
         public string Name { get; set; }
         public string Url { get; set; }
         public string Image { get; set; }
         public string Icon { get; set; }
         public string Module { get; set; }
         public string Function { get; set; }
      }

      public static List<TabItem> GetUserMenu()
      {
         var result = new List<TabItem>();
         var context = HttpContext.Current;
         if (context == null || context.User == null)
            throw new Exception("Do not have authenticated");
         var httpContext = context.Request.RequestContext.HttpContext;
         var path = HostingEnvironment.MapPath("/App_Data/menu.xml");
         if (System.IO.File.Exists(path))
         {
            //load menu data user can access
            var root = XElement.Load(path);
            foreach (var xmlTab in root.Elements("Tab"))
            {
               var tab = new TabItem() { Name = xmlTab.Element("Name")?.Value };
               foreach (var xmlGroup in xmlTab.Elements("Group"))
               {
                  var group = new GroupItem()
                  {
                     Name = xmlGroup.Element("Name")?.Value,
                     Image = xmlGroup.Element("Image")?.Value,
                     Icon = xmlGroup.Element("Icon")?.Value,
                  };
                  foreach (var xmlFunc in xmlGroup.Elements("Item"))
                  {
                     var func = new FunctionItem()
                     {
                        Name = xmlFunc.Element("Name")?.Value,
                        Image = xmlFunc.Element("Image")?.Value,
                        Icon = xmlFunc.Element("Icon")?.Value,
                        Module = xmlFunc.Element("Module").Value,
                        Function = xmlFunc.Element("Function").Value
                     };
                     if (string.IsNullOrEmpty(func.Name))
                        func.Name = func.Function;

                     var b = false;
                     if (Functions.TryGetValue(func.Module, out var module))
                     {
                        if (module.TryGetValue(func.Function, out var f))
                        {
                           b = httpContext.CheckPermission(func.Module, f.Name);
                           if (b)
                           {
                              if (func.Module == "WebApp.dll")
                                 func.Url = "/" + f.ControllerName;
                              else
                                 func.Url = "/" + func.Module.Split('.')[1] + '/' + f.ControllerName;
                           }
                        }
                     }
                     if (b)
                        group.Functions.Add(func);
                  }
                  if (group.Functions.Count > 0)
                     tab.Groups.Add(group);
               }
               if (tab.Groups.Count > 0)
                  result.Add(tab);
            }
         }
         else
         {
            //load all functions user can access (for dev module)
            foreach (var m in Functions)
            {
               var tab = new TabItem() { Name = m.Key.Replace(".dll", "").Replace("WebModule.", "") };
               result.Add(tab);
               var group = new GroupItem() { Name = Language.T("Default") };
               tab.Groups.Add(group);

               foreach (var f in m.Value)
               {
                  if (httpContext.CheckPermission(m.Key, f.Value.Name))
                  {
                     var func = new FunctionItem()
                     {
                        Name = f.Key,
                        Module = m.Key,
                        Function = f.Value.Name,
                        Url = (tab.Name != "WebApp" ? "/" + tab.Name + "/" : "/") + f.Value.ControllerName
                     };
                     group.Functions.Add(func);
                  }
               }
            }
         }
         return result;
      }


      public static Dictionary<string, List<KeyValuePair<int, string>>> GetUserReports()
      {
         var db = ApplicationDbContext.Current;
         var result = new Dictionary<string, List<KeyValuePair<int, string>>>();
         var context = HttpContext.Current;
         if (context == null || context.User == null)
            throw new Exception("Do not have authenticated");
         var httpContext = context.Request.RequestContext.HttpContext;
         var query = from r in db.Session.Query<DataModel.ReportLayout>()
                     where r.Category != null && r.Category != "" && r.Active && (r.Company == null || r.Company.Oid == db.CompanyId.Value)
                     orderby r.Category, r.Name
                     select new { r.Oid, r.Name, r.Category, r.Company };
         var arr = query.ToList().Where(p => httpContext.CheckReport(p.Oid)).ToList();
         var custom = arr.Where(p => p.Company != null).Select(p => p.Name).ToList();
         arr = arr.Where(p => p.Company != null || !custom.Contains(p.Name)).ToList();
         return arr.GroupBy(p => p.Category, p => new KeyValuePair<int, string>(p.Oid, p.Name)).ToDictionary(p => p.Key, p => p.ToList());
      }
   }
}