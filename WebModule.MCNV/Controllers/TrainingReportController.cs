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
   [Function("Báo cáo đào tạo")]
   public class TrainingReportController : WebApp.Controllers.ReportViewerController
   {
      protected override IEnumerable<KeyValuePair<string, List<KeyValuePair<int, string>>>> GetReports()
      {
         const string key = "Đào tạo: ";
         return base.GetReports().Where(it => it.Key.StartsWith(key))
            .Select(it => new KeyValuePair<string, List<KeyValuePair<int, string>>>(it.Key.Replace(key, ""), it.Value));
      }
   }
}