using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Gói can thiệp")]
   public class TreatmentController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Treatment.ListInfo());
      }

      public ActionResult IndexPartial(Models.Treatment.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Treatment.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Treatment vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Json(new { vm.Id, vm.Name });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Treatment.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Treatment vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Treatment.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa gói điều trị này do đã được sử dụng trong bệnh án"));
         Models.Treatment.Delete(id);
         return Nothing();
      }
   }
}