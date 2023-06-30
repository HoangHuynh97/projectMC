using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Đơn vị đào tạo")]
   public class InstitutionController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Institution.ListInfo());
      }

      public ActionResult IndexPartial(Models.Institution.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Institution.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Institution vm)
      {
         if (!Models.Institution.CheckName(vm.Id, vm.Name))
            ModelState.AddModelError("Name", "Tên đơn vị này đã có sử dụng");
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Json(new { vm.Id, vm.Name });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Institution.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return JavaScript("WebApp.function.view();");
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Institution vm)
      {
         if (!Models.Institution.CheckName(vm.Id, vm.Name))
            ModelState.AddModelError("Name", "Tên đơn vị này đã có sử dụng");
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Institution.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa cơ sở này do đã được sử dụng trong Khóa đào tạo"));
         Models.Institution.Delete(id);
         return Nothing();
      }

      public ActionResult ViewInfo(int id)
      {
         var vm = Models.Institution.ViewInfo.Load(id);
         if (vm == null) return NotFound();
         return PartialView(vm);
      }

      public ActionResult CheckName(int id, string name)
      {
         return Json(Models.Institution.CheckName(id, name));
      }
   }
}