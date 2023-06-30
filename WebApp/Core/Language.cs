using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Xml.Linq;

namespace WebApp.Core
{
   public class Language
   {
      private static string key = "WebApp.Language";
      public static List<Language> List { get; private set; }
      static Language()
      {
         Load();
      }

      /// <summary>
      /// Load data of all language from xml in App_Data/Languages
      /// </summary>
      public static void Load()
      {
         if (List == null)
            List = new List<Language>();
         if (!Directory.Exists(HostingEnvironment.MapPath("/App_Data/Languages"))) return;
         Logger.Info("Load languages");
         var files = Directory.GetFiles(HostingEnvironment.MapPath("/App_Data/Languages/"), "*.xml");
         foreach (var f in files)
         {
            var xml = XDocument.Load(f);
            var lang = List.FirstOrDefault(p => p.Code == xml.Root.Element("Code").Value);
            if (lang == null)
            {
               lang = new Language()
               {
                  Code = xml.Root.Element("Code").Value,
                  Name = xml.Root.Element("Name").Value
               };
               List.Add(lang);
            }
            foreach (var d in xml.Root.Elements("Dictionary"))
            {
               lang.Dictionary[d.Element("Key").Value] = d.Element("Value").Value;
            }
         }
      }

      public static Language Current
      {
         get
         {
            var def = "vi";
            Language lng = null;
            //get from session, can be session=null when first load webpage
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[key] != null)
               lng = (Language)HttpContext.Current.Session[key];
            else
            {
               //get default from cockie
               try
               {
                  def = HttpContext.Current.Request.Cookies[key]?.Value ?? def;
               }
               catch { }
            }
            if (lng == null)
            {
               lng = List.FirstOrDefault(p => p.Code == def);
               if (lng == null)
                  lng = new Language() { Code = def };
               if (HttpContext.Current.Session != null && lng != null)
                  HttpContext.Current.Session[key] = lng;
            }
            return lng;
         }

      }

      public static void SetCurrentLanguage(string code)
      {
         var lng = List.FirstOrDefault(p => p.Code == code);
         if (lng != null)
         {
            HttpContext.Current.Session[key] = lng;
            try
            {
               var ck = new HttpCookie(key, lng.Code)
               {
                  Expires = DateTime.MaxValue
               };
               HttpContext.Current.Response.Cookies.Add(ck);
            }
            catch { }
         }
      }

      public static string T(string key, params object[] values)
      {
         if (string.IsNullOrEmpty(key)) return "";
         if (!Current.Dictionary.TryGetValue(key, out string s))
            Current.Dictionary[key] = s = key;
         if (values != null && values.Length > 0)
            return string.Format(s, values);
         else
            return s;
      }

      public string Code { get; private set; }
      public string Name { get; private set; }
      public Dictionary<string, string> Dictionary { get; private set; }

      public Language()
      {
         Dictionary = new Dictionary<string, string>();
      }

      public void Save()
      {
         var xml = new XElement("Language", new XElement("Code", Code), new XElement("Name", Name));
         xml.Add(Dictionary.Select(p => new XElement("Dictionary", new XElement("Key", p.Key), new XElement("Value", p.Value))));
         xml.Save(HostingEnvironment.MapPath("/App_Data/Languages/" + Code + ".xml"));
      }
   }
}