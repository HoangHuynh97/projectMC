using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Phân quyền dữ liệu")]
   public class PermissionDataController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.PermissionData.ListInfo());
      }

      public ActionResult IndexPartial(Models.PermissionData.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.PermissionData.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.PermissionData vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.PermissionData.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.PermissionData vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         Models.PermissionData.Delete(id);
         return Nothing();
      }
   }
}