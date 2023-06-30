using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Dự án")]
   public class ProgramProjectController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.ProgramProject.ListInfo());
      }

      public ActionResult IndexPartial(Models.ProgramProject.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.ProgramProject.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.ProgramProject vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.ProgramProject.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.ProgramProject vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.ProgramProject.CanDelete(id))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa dự án này do đã được sử dụng trong khóa đào tạo"));
         Models.ProgramProject.Delete(id);
         return Nothing();
      }
   }
}