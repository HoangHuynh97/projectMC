using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.WebPages;

namespace WebApp
{
   internal class PrecompileViewEngine : VirtualPathProviderViewEngine, IVirtualPathFactory
   {
      private string _appName;
      private readonly Dictionary<string, Dictionary<string, Type>> _mappings;
      private readonly IViewPageActivator _viewPageActivator;

      public readonly static string[] DefaultAreaLocationFormats = new[] {
            "~/Areas/{2}/Views/{1}/{0}.cshtml",
            "~/Areas/{2}/Views/Shared/{0}.cshtml"
        };

      public readonly static string[] DefaultLocationFormats = new[] {
            "~/Views/{1}/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml"
        };

      public readonly static string[] DefaultFileExtensions = new[] {
            "cshtml"
        };

      public PrecompileViewEngine(string appName) : this(appName, null) { }

      public PrecompileViewEngine(string appName, IViewPageActivator viewPageActivator)
      {
         base.AreaViewLocationFormats = DefaultAreaLocationFormats;
         base.AreaMasterLocationFormats = DefaultAreaLocationFormats;
         base.AreaPartialViewLocationFormats = DefaultAreaLocationFormats;
         base.ViewLocationFormats = DefaultLocationFormats;
         base.MasterLocationFormats = DefaultLocationFormats;
         base.PartialViewLocationFormats = DefaultLocationFormats;
         base.FileExtensions = DefaultFileExtensions;

         _appName = appName;
         var asm = from a in AppDomain.CurrentDomain.GetAssemblies()
                   where a.ManifestModule.Name == "WebApp.dll" || a.ManifestModule.Name.StartsWith("WebModule.")
                   select a;
         _mappings = asm.ToDictionary(a => a.ManifestModule.Name,
            a => (from t in a.GetTypes()
                  where typeof(WebPageRenderingBase).IsAssignableFrom(t)
                  let pageVirtualPath = t.GetCustomAttributes(inherit: false).OfType<PageVirtualPathAttribute>().FirstOrDefault()
                  where pageVirtualPath != null
                  select new KeyValuePair<string, Type>(pageVirtualPath.VirtualPath, t)
                 ).ToDictionary(v => v.Key, v => v.Value, StringComparer.OrdinalIgnoreCase)
                 );
         _viewPageActivator = viewPageActivator
             ?? DependencyResolver.Current.GetService<IViewPageActivator>() /* For compatibility, remove this line within next version */
             ?? DefaultViewPageActivator.Current;

      }

      public object CreateInstance(string virtualPath)
      {
         virtualPath = EnsureVirtualPathPrefix(virtualPath);

         if (_appName == "WebApp" && HttpContext.Current.Request.IsLocal && VirtualPathProvider.FileExists(virtualPath))
         {
            // If the physical file on disk is newer and the user's opted in this behavior, serve it instead.
            return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(WebPageRenderingBase));
         }

         if (_mappings.TryGetValue("WebApp.dll", out Dictionary<string, Type> types))
         {
            if (types.TryGetValue(virtualPath, out Type type))
            {
               return _viewPageActivator.Create(null, type);
            }
         }

         return null;
      }

      //called by "System.Web.WebPages.dll" for _ViewStart, Layout, _appstart, we get views by search in main WebApp
      public bool Exists(string virtualPath)
      {
         virtualPath = EnsureVirtualPathPrefix(virtualPath);
         if (_appName == "WebApp" && HttpContext.Current.Request.IsLocal && VirtualPathProvider.FileExists(virtualPath))
         {
            return true;
         }
         if (_mappings.TryGetValue("WebApp.dll", out Dictionary<string, Type> types))
         {
            return types.ContainsKey(virtualPath);
         }
         return false;
      }

      protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
      {
         virtualPath = EnsureVirtualPathPrefix(virtualPath);
         var name = GetAssemblyName(controllerContext, virtualPath);
         if (name.StartsWith(_appName) && HttpContext.Current.Request.IsLocal && VirtualPathProvider.FileExists(virtualPath))
         {
            return false; //continue by other engines
         }
         if (_mappings.TryGetValue(name, out Dictionary<string, Type> types))
         {
            return types.ContainsKey(virtualPath);
         }
         return false;
      }

      protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
      {
         partialPath = EnsureVirtualPathPrefix(partialPath);

         return CreateViewInternal(GetAssemblyName(controllerContext, partialPath), partialPath, masterPath: null, runViewStartPages: false);
      }

      protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
      {
         viewPath = EnsureVirtualPathPrefix(viewPath);

         return CreateViewInternal(GetAssemblyName(controllerContext, viewPath), viewPath, masterPath, runViewStartPages: true);
      }

      private IView CreateViewInternal(string asmName, string viewPath, string masterPath, bool runViewStartPages)
      {
         if (_mappings.TryGetValue(asmName, out Dictionary<string, Type> types))
         {
            if (types.TryGetValue(viewPath, out Type type))
            {
               return new PrecompiledMvcView(viewPath, masterPath, type, runViewStartPages, base.FileExtensions, _viewPageActivator);
            }
         }
         throw new Exception("Some things wrong so far, view:" + viewPath);
      }

      private string GetAssemblyName(ControllerContext controllerContext, string virtualPath)
      {
         var name = "";
         if (virtualPath.StartsWith("~/Views/Shared/")) //override all views in Shared
         {
            name = "WebApp.dll";
            foreach(var a in _mappings)
            {
               if (a.Key != "WebApp.dll" && a.Value.ContainsKey(virtualPath))
               {
                  name = a.Key;
                  break;
               }
            }
         }
         else
         {
            //get assembly by controller
            name = controllerContext.Controller.GetType().Assembly.ManifestModule.Name; 
         }

         return name;
      }

      internal static string EnsureVirtualPathPrefix(string virtualPath)
      {
         if (!String.IsNullOrEmpty(virtualPath))
         {
            // For a virtual path lookups to succeed, it needs to start with a ~/.
            if (!virtualPath.StartsWith("~/", StringComparison.Ordinal))
            {
               virtualPath = "~/" + virtualPath.TrimStart(new[] { '/', '~' });
            }
         }
         return virtualPath;
      }

      internal static string NormalizeBaseVirtualPath(string virtualPath)
      {
         if (!String.IsNullOrEmpty(virtualPath))
         {
            // For a virtual path to combine properly, it needs to start with a ~/ and end with a /.
            virtualPath = EnsureVirtualPathPrefix(virtualPath);
            if (!virtualPath.EndsWith("/", StringComparison.Ordinal))
            {
               virtualPath += "/";
            }
         }
         return virtualPath;
      }
   }
}