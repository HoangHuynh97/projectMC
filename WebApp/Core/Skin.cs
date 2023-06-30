using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Core
{
   public class Skin
   {
      private static readonly string keyName = "WebApp.Skin";
      public static string Current
      {
         get
         {
            string skin = ""; //use default theme in webconfig (devExpress.themes)
            //get from session, can be session=null when first load webpage
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[keyName] != null)
               skin = (string)HttpContext.Current.Session[keyName];
            else
            {
               //get default from cockie
               try
               {
                  var cookie = HttpContext.Current.Request.Cookies[keyName];
                  if (cookie != null) skin = cookie.Value;
                  if (HttpContext.Current.Session != null)
                     HttpContext.Current.Session[keyName] = skin;
               }
               catch { }
            }
            return skin;
         }
         set
         {
            try
            {
               var ck = new HttpCookie(keyName, value);
               ck.Expires = DateTime.Today.AddYears(10);
               HttpContext.Current.Response.Cookies.Add(ck);
            }
            catch { }
            HttpContext.Current.Session[keyName] = value;
         }
      }

      private static Dictionary<string, List<string>> _list;
      public static Dictionary<string, List<string>> List
      {
         get
         {
            if (_list == null)
            {
               _list = new Dictionary<string, List<string>>
               {
                  ["Đơn giản"] = new List<string>() { "Default", "DevEx", "Metropolis", "MetropolisBlue", "Moderno", "iOS" },
                  ["Google"] = new List<string>() { "MaterialCompact_0091ea", "MaterialCompact_00bfa5", "MaterialCompact_43a047", "MaterialCompact_e65100", "MaterialCompact_6a1b9a", "MaterialCompact_424242" },
                  ["Office 365"] = new List<string>() { "Office365_0091ea", "Office365_00bfa5", "Office365_43a047", "Office365_e65100", "Office365_6a1b9a", "Office365_424242" },
                  ["Office 2010"] = new List<string>() { "Office2010Blue", "Office2010Black", "Office2010Silver" },
                  ["Office 2003"] = new List<string>() { "Office2003Blue", "Office2003Olive", "Office2003Silver" },
                  ["Màu sắc"] = new List<string>() { "Aqua", "BlackGlass", "Glass", "Mulberry", "PlasticBlue", "RedWine", "SoftOrange", "Youthful" }
               };
            }
            return _list;
         }
      }
   }
}