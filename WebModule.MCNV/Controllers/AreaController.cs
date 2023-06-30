using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Vùng dự án")]
   public class AreaController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Area.ListInfo());
      }

      public ActionResult IndexPartial(Models.Area.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Area.Create());
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Area vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Area.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Area vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         if (!Models.Area.CanDelete(id, out var err))
            return ShowInfo(T("Lỗi"), T("Bạn không thể xóa vùng dự án này do đã được sử dụng trong {0}", T(err)));
         Models.Area.Delete(id);
         return Nothing();
      }
   }
}