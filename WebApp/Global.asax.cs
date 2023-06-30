using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace WebApp
{
   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
   // visit http://go.microsoft.com/?LinkId=9394801

   public class MvcApplication : System.Web.HttpApplication
   {
      public virtual string Name => "WebApp";

      protected void Application_Start()
      {
         Core.Logger.Info("Starting");
         //localizer
         Localizer.Localizer.Activate();
         Localizer.GridView.Activate();
         Localizer.Editors.Activate();
         Localizer.Report.Activate();

         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);

         var engine = new PrecompileViewEngine(Name);
         ViewEngines.Engines.Insert(0, engine);
         VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);

         //override DisplayMode
         DisplayModeProvider.Instance.Modes.Clear();
         DisplayModeProvider.Instance.Modes.Add(new DefaultDisplayMode("Mobile")
         {
            ContextCondition = context =>
            {
               var m = context.Request.Params["mode"];
               if (!string.IsNullOrEmpty(m))
                  return m == "mobile";
               var t = GetDeviceType(context.GetOverriddenUserAgent() ?? "");
               return t != "tv" && t != "tablet" && context.GetOverriddenBrowser().IsMobileDevice;
            }
         });
         DisplayModeProvider.Instance.Modes.Add(new DefaultDisplayMode());

         //xpo
         Core.Logger.Info("XPO setup");
         string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
         DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
         var ams = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.ManifestModule.Name.StartsWith("WebModule.") || a.ManifestModule.Name == "WebApp.dll").ToArray();
         dict.GetDataStoreSchema(ams);
         DevExpress.Xpo.DB.IDataStore store = new Core.DtoProvider(new System.Data.SqlClient.SqlConnection(conn), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
         DevExpress.Xpo.XpoDefault.DataLayer = new DevExpress.Xpo.ThreadSafeDataLayer(dict, store);
         DevExpress.Xpo.XpoDefault.Session = null;

         ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
         DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new ReportStorage());

         DevExpress.Web.ASPxWebControl.CallbackError += Application_Error;

         //run event after start
         foreach (var a in ams)
         {
            var types = a.DefinedTypes.Where(p => typeof(MvcApplication).IsAssignableFrom(p));
            foreach (var t in types)
            {
               var app = (t.Assembly.CreateInstance(t.FullName) as MvcApplication);
               app.AfterStart();
            }
         }
      }

      protected void Application_Error(object sender, EventArgs e)
      {
         Exception exception = System.Web.HttpContext.Current.Server.GetLastError();
         Core.Logger.Error("Unhandled error", exception);
      }

      protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
      {
         var cult = new System.Globalization.CultureInfo(Core.Language.Current.Code);
         cult.DateTimeFormat.ShortTimePattern = "HH:mm";
         System.Threading.Thread.CurrentThread.CurrentCulture = cult;
         System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
         var names = Core.Skin.Current.Split('_');
         DevExpress.Web.Mvc.DevExpressHelper.Theme = names[0];
         if (names.Length > 1)
            DevExpress.Web.Mvc.DevExpressHelper.GlobalThemeBaseColor = '#' + names[1];
      }

      public virtual void AfterStart() { }

      public virtual string GetDeviceType(string ua)
      {
         string ret = "";
         // Check if user agent is a smart TV - http://goo.gl/FocDk
         if (Regex.IsMatch(ua, @"GoogleTV|SmartTV|Internet.TV|NetCast|NETTV|AppleTV|boxee|Kylo|Roku|DLNADOC|CE\-HTML", RegexOptions.IgnoreCase))
         {
            ret = "tv";
         }
         // Check if user agent is a TV Based Gaming Console
         else if (Regex.IsMatch(ua, "Xbox|PLAYSTATION.3|Wii", RegexOptions.IgnoreCase))
         {
            ret = "tv";
         }
         // Check if user agent is a Tablet
         else if ((Regex.IsMatch(ua, "iP(a|ro)d", RegexOptions.IgnoreCase) || (Regex.IsMatch(ua, "tablet", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(ua, "RX-34", RegexOptions.IgnoreCase)) || (Regex.IsMatch(ua, "FOLIO", RegexOptions.IgnoreCase))))
         {
            ret = "tablet";
         }
         // Check if user agent is an Android Tablet
         else if ((Regex.IsMatch(ua, "Linux", RegexOptions.IgnoreCase)) && (Regex.IsMatch(ua, "Android", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(ua, "Fennec|mobi|HTC.Magic|HTCX06HT|Nexus.One|SC-02B|fone.945", RegexOptions.IgnoreCase)))
         {
            ret = "tablet";
         }
         // Check if user agent is a Kindle or Kindle Fire
         else if ((Regex.IsMatch(ua, "Kindle", RegexOptions.IgnoreCase)) || (Regex.IsMatch(ua, "Mac.OS", RegexOptions.IgnoreCase)) && (Regex.IsMatch(ua, "Silk", RegexOptions.IgnoreCase)))
         {
            ret = "tablet";
         }
         // Check if user agent is a pre Android 3.0 Tablet
         else if ((Regex.IsMatch(ua, @"GT-P10|SC-01C|SHW-M180S|SGH-T849|SCH-I800|SHW-M180L|SPH-P100|SGH-I987|zt180|HTC(.Flyer|\\_Flyer)|Sprint.ATP51|ViewPad7|pandigital(sprnova|nova)|Ideos.S7|Dell.Streak.7|Advent.Vega|A101IT|A70BHT|MID7015|Next2|nook", RegexOptions.IgnoreCase)) || (Regex.IsMatch(ua, "MB511", RegexOptions.IgnoreCase)) && (Regex.IsMatch(ua, "RUTEM", RegexOptions.IgnoreCase)))
         {
            ret = "tablet";
         }
         // Check if user agent is unique Mobile User Agent
         else if ((Regex.IsMatch(ua, "BOLT|Fennec|Iris|Maemo|Minimo|Mobi|mowser|NetFront|Novarra|Prism|RX-34|Skyfire|Tear|XV6875|XV6975|Google.Wireless.Transcoder", RegexOptions.IgnoreCase)))
         {
            ret = "mobile";
         }
         // Check if user agent is an odd Opera User Agent - http://goo.gl/nK90K
         else if ((Regex.IsMatch(ua, "Opera", RegexOptions.IgnoreCase)) && (Regex.IsMatch(ua, "Windows.NT.5", RegexOptions.IgnoreCase)) && (Regex.IsMatch(ua, @"HTC|Xda|Mini|Vario|SAMSUNG\-GT\-i8000|SAMSUNG\-SGH\-i9", RegexOptions.IgnoreCase)))
         {
            ret = "mobile";
         }
         // Check if user agent is Windows Desktop
         else if ((Regex.IsMatch(ua, "Windows.(NT|XP|ME|9)")) && (!Regex.IsMatch(ua, "Phone", RegexOptions.IgnoreCase)) || (Regex.IsMatch(ua, "Win(9|.9|NT)", RegexOptions.IgnoreCase)))
         {
            ret = "desktop";
         }
         // Check if agent is Mac Desktop
         else if ((Regex.IsMatch(ua, "Macintosh|PowerPC", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(ua, "Silk", RegexOptions.IgnoreCase)))
         {
            ret = "desktop";
         }
         // Check if user agent is a Linux Desktop
         else if ((Regex.IsMatch(ua, "Linux", RegexOptions.IgnoreCase)) && (Regex.IsMatch(ua, "X11", RegexOptions.IgnoreCase)))
         {
            ret = "desktop";
         }
         // Check if user agent is a Solaris, SunOS, BSD Desktop
         else if ((Regex.IsMatch(ua, "Solaris|SunOS|BSD", RegexOptions.IgnoreCase)))
         {
            ret = "desktop";
         }
         // Check if user agent is a Desktop BOT/Crawler/Spider
         else if ((Regex.IsMatch(ua, "Bot|Crawler|Spider|Yahoo|ia_archiver|Covario-IDS|findlinks|DataparkSearch|larbin|Mediapartners-Google|NG-Search|Snappy|Teoma|Jeeves|TinEye", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(ua, "Mobile", RegexOptions.IgnoreCase)))
         {
            ret = "desktop";
         }
         return ret;
      }
   }
}