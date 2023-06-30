using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Báo cáo dịch vụ")]
   public class MedicalReportController : WebApp.Controllers.ReportViewerController
   {
      protected override IEnumerable<KeyValuePair<string, List<KeyValuePair<int, string>>>> GetReports()
      {
         const string key = "Dịch vụ: ";
         return base.GetReports().Where(it => it.Key.StartsWith(key))
            .Select(it => new KeyValuePair<string, List<KeyValuePair<int, string>>>(it.Key.Replace(key, ""), it.Value));
      }
   }
}