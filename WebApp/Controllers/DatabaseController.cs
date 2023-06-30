using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
   [Core.Function("Cập nhật dữ liệu", Roles = Constant.Role.System)]
   public class DatabaseController : Core.BaseController
   {
      public ActionResult Index()
      {
         return JavaScript($"WebApp.showConfirm('{T("Cập nhật dữ liệu")}','{T("Bạn có muốn cập nhật cơ sở dữ liệu không?")}').done(function() {{WebApp.ajax('Database/Update').done(function() {{WebApp.notify('{T("Đã cập nhật dữ liệu thành công")}');}});}});");
      }

      public ActionResult Update()
      {
         string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
         DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
         var ams = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.ManifestModule.Name.StartsWith("WebModule.") || a.ManifestModule.Name == "WebApp.dll").ToArray();
         dict.GetDataStoreSchema(ams);
         DevExpress.Xpo.DB.IDataStore store = new Core.DtoProvider(new System.Data.SqlClient.SqlConnection(conn), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
         var layer = new DevExpress.Xpo.SimpleDataLayer(dict, store);
         var session = new DevExpress.Xpo.Session(layer);
         session.UpdateSchema();
         return Nothing();
      }
   }
}