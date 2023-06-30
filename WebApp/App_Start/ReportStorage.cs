using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;

namespace WebApp
{
   public class ReportStorage : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
   {
      //OWIN context do not run here, so it can not get ApplicationDbContext
      public override bool CanSetData(string url)
      {
         var session = new Session();
         return session.GetObjectByKey<DataModel.ReportLayout>(int.Parse(url)) != null;
      }
      public override bool IsValidUrl(string url)
      {
         return int.TryParse(url, out int id);
      }

      public override byte[] GetData(string url)
      {
         var session = new Session();
         var rpt = session.GetObjectByKey<DataModel.ReportLayout>(int.Parse(url));
         if (rpt == null)
            return null;
         return rpt.Layout;
      }

      public override Dictionary<string, string> GetUrls()
      {
         var session = new Session();
         var xpc = new XPCollection<DataModel.ReportLayout>(session);
         return xpc.ToDictionary(d => d.Oid.ToString(), d => d.Name);
      }

      public override void SetData(DevExpress.XtraReports.UI.XtraReport report, string url)
      {
         var session = new Session();
         var rpt = session.GetObjectByKey<DataModel.ReportLayout>(int.Parse(url));
         if (rpt != null)
         {
            using (MemoryStream ms = new MemoryStream())
            {
               //don't save connection
               if (report.DataSource is DevExpress.DataAccess.Sql.SqlDataSource ds)
                  ds.ConnectionParameters = null;

               report.SaveLayoutToXml(ms);
               rpt.Layout = ms.GetBuffer();
            }
            rpt.Save();
         }
      }

      public override string SetNewData(DevExpress.XtraReports.UI.XtraReport report, string defaultUrl)
      {
         var session = new Session();
         var rpt = new DataModel.ReportLayout(session)
         {
            Name = defaultUrl
         };
         using (MemoryStream ms = new MemoryStream())
         {
            //don't save connection
            if (report.DataSource is DevExpress.DataAccess.Sql.SqlDataSource ds)
               ds.ConnectionParameters = null;

            report.SaveLayoutToXml(ms);
            rpt.Layout = ms.GetBuffer();
         }
         rpt.Save();
         return rpt.Oid.ToString();
      }
   }
}