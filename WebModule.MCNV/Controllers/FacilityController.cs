using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Đơn vị cung cấp dịch vụ")]
   public class FacilityController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Facility.ListInfo());
      }

      public ActionResult IndexPartial(Models.Facility.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Facility.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Facility vm)
      {
         if (!Models.Facility.CheckName(vm.Id, vm.Name))
            ModelState.AddModelError("Name", "Tên đơn vị này đã có sử dụng");
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Json(new { vm.Id, vm.Name });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Facility.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return JavaScript("WebApp.function.view();");
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Facility vm)
      {
         if (!Models.Facility.CheckName(vm.Id, vm.Name))
            ModelState.AddModelError("Name", "Tên đơn vị này đã có sử dụng");
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Facility.CanDelete(id, out var err))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa đơn vị này do đã được sử dụng trong {0}", T(err)));
         var s = Models.Facility.Delete(id);
         if (!string.IsNullOrEmpty(s)) return ShowInfo(T("Lỗi"), T(s));
         return Nothing();
      }

      public ActionResult ViewInfo(int id)
      {
         var vm = Models.Facility.ViewInfo.Load(id);
         if (vm == null) return NotFound();
         return PartialView(vm);
      }
      
      public ActionResult CheckName(int id, string name)
      {
         return Json(Models.Facility.CheckName(id, name));
      }
   }
}