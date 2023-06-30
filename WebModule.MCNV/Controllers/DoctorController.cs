using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Cán bộ PHCN")]
   public class DoctorController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Doctor.ListInfo());
      }

      public ActionResult IndexPartial(Models.Doctor.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add(int? Facility)
      {
         return PartialView("EditTemplate", Models.Doctor.Create(Facility));
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Doctor vm)
      {
         if (!Models.Doctor.CheckName(vm.Id, vm.Name, vm.DateBirth))
            ModelState.AddModelError("Name", "Tên cán bộ này đã có sử dụng");
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         var specialize = "";
         if (vm.Specialize != null && Models.Service.TryGetModel(vm.Specialize.Value, out var sv))
            specialize = sv.Name;
         return Json(new { vm.Id, vm.Name, Specialize = specialize });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Doctor.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return JavaScript("WebApp.function.view();");
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Doctor vm)
      {
         if (!Models.Doctor.CheckName(vm.Id, vm.Name, vm.DateBirth))
            ModelState.AddModelError("Name", "Tên cán bộ này đã có sử dụng");
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Doctor.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa cán bộ này do đã được sử dụng trong can thiệp điều trị hoặc đào tạo"));
         var s = Models.Doctor.Delete(id);
         if (!string.IsNullOrEmpty(s)) return ShowInfo(T("Lỗi"), T(s));
         return Nothing();
      }

      public ActionResult ViewInfo(int id)
      {
         var vm = Models.Doctor.ViewInfo.Load(id);
         if (vm == null) return NotFound();
         return PartialView(vm);
      }

      public ActionResult CheckName(int id, string name, string dateBirthText)
      {
         DateTime? d = null;
         if (!string.IsNullOrEmpty(dateBirthText))
            d = Convert.ToDateTime(dateBirthText);
         return Json(Models.Doctor.CheckName(id, name, d));
      }

   }
}