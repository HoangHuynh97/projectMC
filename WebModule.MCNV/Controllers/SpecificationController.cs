using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Kỹ thuật PHCN")]
   public class SpecificationController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Specification.ListInfo());
      }

      public ActionResult IndexPartial(Models.Specification.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Specification.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Specification vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Json(new { vm.Id, vm.Name });
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Specification.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Specification vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         Models.Specification.Delete(id);
         return Nothing();
      }
   }
}