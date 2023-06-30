using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Thông tin NKT")]
   public class PatientController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Patient.ListInfo());
      }

      public ActionResult IndexPartial(Models.Patient.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Patient.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Patient vm)
      {
         if (!Models.Patient.CheckCMND(vm.Id, vm.CMND))
            ModelState.AddModelError(nameof(vm.CMND), T("Số CMND này đã nhập rồi"));
         if (!Models.Patient.CheckCCCD(vm.Id, vm.CCCD))
            ModelState.AddModelError(nameof(vm.CCCD), T("Số CCCD này đã nhập rồi"));
         if (!Models.Patient.CheckBHYT(vm.Id, vm.BHYT))
            ModelState.AddModelError(nameof(vm.BHYT), T("Số thẻ BHYT này đã nhập rồi"));
         if (string.IsNullOrEmpty(vm.CMND) && string.IsNullOrEmpty(vm.CCCD) && string.IsNullOrEmpty(vm.BHYT))
            return ShowInfo(T("Lỗi"), T("Cần nhập ít nhất 1 thông tin của số CMND, số CCCD, thẻ BHYT"));
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Json(new { vm.Id, vm.Name });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Patient.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return JavaScript("WebApp.function.view();");
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Patient vm)
      {
         if (!Models.Patient.CheckCMND(vm.Id, vm.CMND))
            ModelState.AddModelError(nameof(vm.CMND), T("Số CMND này đã nhập rồi"));
         if (!Models.Patient.CheckCCCD(vm.Id, vm.CCCD))
            ModelState.AddModelError(nameof(vm.CCCD), T("Số CCCD này đã nhập rồi"));
         if (!Models.Patient.CheckBHYT(vm.Id, vm.BHYT))
            ModelState.AddModelError(nameof(vm.BHYT), T("Số thẻ BHYT này đã nhập rồi"));
         if (string.IsNullOrEmpty(vm.CMND) && string.IsNullOrEmpty(vm.CCCD) && string.IsNullOrEmpty(vm.BHYT))
            return ShowInfo(T("Lỗi"), T("Cần nhập ít nhất 1 thông tin của số CMND, số CCCD, thẻ BHYT"));
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Patient.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa bệnh nhân này do đã có lập hồ sơ PHCN"));
         var s = Models.Patient.Delete(id);
         if (!string.IsNullOrEmpty(s)) return ShowInfo(T("Lỗi"), T(s));
         return Nothing();
      }

      public ActionResult ViewInfo(int id)
      {
         var vm = Models.Patient.ViewInfo.Load(id);
         if (vm == null) return NotFound();
         return PartialView(vm);
      }

      public ActionResult CheckCMND(int id, string cmnd)
      {
         return Json(Models.Patient.CheckCMND(id, cmnd));
      }

      public ActionResult CheckCCCD(int id, string cccd)
      {
         return Json(Models.Patient.CheckCCCD(id, cccd));
      }

      public ActionResult CheckBHYT(int id, string bhyt)
      {
         return Json(Models.Patient.CheckBHYT(id, bhyt));
      }
   }
}