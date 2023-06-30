using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Báo cáo")]
   public class ReportViewerController : BaseController
   {
      public static readonly string ReportKey = "ReportViewer";

      private XtraReport CurrentReport
      {
         get { return Session[ReportKey] as XtraReport; }
         set { Session[ReportKey] = value; }
      }

      protected virtual IEnumerable<KeyValuePair<string, List<KeyValuePair<int, string>>>> GetReports() => Menu.GetUserReports();

      public ActionResult Index()
      {
         CurrentReport = null;
         return PartialView("Report", GetReports());
      }

      public ActionResult Preview()
      {
         return PartialView(CurrentReport);
      }

      public ActionResult Export()
      {
         return DocumentViewerExtension.ExportTo(CurrentReport);
      }

      public ActionResult OpenReport(int id)
      {
         if (!HttpContext.CheckReport(id))
            return ShowInfo(T("Lỗi"), T("Bạn không có được phân quyền xem báo cáo"));
         var vm = Models.ReportViewer.OpenReport(id);
         if (vm.Result == Models.ReportViewer.OpenReportResult.ResultEnum.NotFound)
            return NotFound();
         if (vm.Result == Models.ReportViewer.OpenReportResult.ResultEnum.HasParameter)
            return JavaScript("WebApp.function.ShowParameter(" + id + ");");
         CurrentReport = vm.Report;
         return Nothing();
      }

      public ActionResult ShowParameter(int id)
      {
         if (!HttpContext.CheckReport(id))
            return ShowInfo(T("Lỗi"), T("Bạn không có được phân quyền xem báo cáo"));
         var vm = Models.ReportViewer.ReportParameters.Load(id);
         if (vm == null)
            return NotFound();
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult ShowParameter()
      {
         var id = Convert.ToInt32(Request.Form["Id"]);
         if (!HttpContext.CheckReport(id))
            return ShowInfo(T("Lỗi"), T("Bạn không có được phân quyền xem báo cáo"));
         var vm = Models.ReportViewer.ReportParameters.Load(id);
         if (vm == null)
            return NotFound();
         var rpt = new ReportInfo(vm.Name);
         foreach(var p in vm.Parameters)
         {
            p.GetValue(Request.Form);
            p.SetParameter(rpt);
         }
         CurrentReport = rpt.Report;
         return Nothing();
      }
   }
}