using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Mẫu báo cáo", Roles = Constant.Role.System)]
   public class ReportController : BaseController
   {
      public ActionResult Index()
      {
         return PartialView(new Models.Report.ListInfo());
      }

      public ActionResult IndexPartial(Models.Report.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Report.Create());
      }

      [Logic("Thêm")]
      [HttpPost]
      public ActionResult Add(Models.Report vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Report.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa")]
      [HttpPost]
      public ActionResult Edit(Models.Report vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa")]
      public ActionResult Delete(int id)
      {
         Models.Report.Delete(id);
         return Nothing();
      }

      [Logic("Thiết kế mẫu")]
      public ActionResult Designer(int id)
      {
         return View(id);
      }
   }
}