using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using WebApp.Core;
using System.Web.Mvc;
using DevExpress.XtraReports.UI;

namespace WebApp.Models
{
   public class ReportViewer
   {
      public class OpenReportResult
      {
         public enum ResultEnum
         {
            Valid,
            NotFound,
            HasParameter
         }

         public ResultEnum Result { get; private set; }
         public XtraReport Report { get; private set; }

         public OpenReportResult(ResultEnum result, XtraReport report = null)
         {
            Result = result;
            Report = report;
         }
      }

      public static OpenReportResult OpenReport(int id)
      {
         var db = ApplicationDbContext.Current;
         var model = db.Session.GetObjectByKey<DataModel.ReportLayout>(id);
         if (model == null)
            return new OpenReportResult(OpenReportResult.ResultEnum.NotFound);
         if (!string.IsNullOrEmpty(model.Parameters))
            return new OpenReportResult(OpenReportResult.ResultEnum.HasParameter);
         var rpt = new ReportInfo(model.Name);
         return new OpenReportResult(OpenReportResult.ResultEnum.Valid, rpt.Report);
      }

      public class ReportParameters
      {
         public int Id { get; set; }
         public string Name { get; set; }
         public List<IReportParameter> Parameters { get; set; }

         public static ReportParameters Load(int id)
         {
            var session = HttpContext.Current.Session;
            var db = ApplicationDbContext.Current;
            var model = db.Session.GetObjectByKey<DataModel.ReportLayout>(id);
            if (model == null || string.IsNullOrEmpty(model.Parameters))
               return null;
            var ps = model.Parameters.Split(',');
            var result = new ReportParameters() { Id = model.Oid, Name = model.Name, Parameters = new List<IReportParameter>()};
            foreach (var s in ps)
            {
               object v = session[s];
               if (v == null)
               {
                  var tmp = s.Split('.');
                  v = Activator.CreateInstance(tmp[0] == "WebApp" ? tmp[0] : tmp[0] + "." + tmp[1], s).Unwrap();
                  session[s] = v;
               }
               result.Parameters.Add((IReportParameter)v);
            }
            return result;
         }
      }
   }
}