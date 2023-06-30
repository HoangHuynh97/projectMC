using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Chẩn đoán")]
   public class DiagnosticController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Diagnostic.ListInfo());
      }

      public ActionResult IndexPartial(Models.Diagnostic.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Diagnostic.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Diagnostic vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Json(new { vm.Id, vm.Code, vm.Name });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Diagnostic.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Diagnostic vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Diagnostic.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa chẩn đoán này do đã được sử dụng trong bệnh án"));
         Models.Diagnostic.Delete(id);
         return Nothing();
      }
   }
}