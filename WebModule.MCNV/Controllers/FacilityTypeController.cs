using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Loại hình đơn vị")]
   public class FacilityTypeController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.FacilityType.ListInfo());
      }

      public ActionResult IndexPartial(Models.FacilityType.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.FacilityType.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.FacilityType vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.FacilityType.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.FacilityType vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.FacilityType.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa loại hình đơn vị này do đã được sử dụng trong đơn vị"));
         Models.FacilityType.Delete(id);
         return Nothing();
      }
   }
}