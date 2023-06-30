using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Quản lý phân quyền", Roles = Constant.Role.System)]
    public class PermissionController : BaseController
    {
      public ActionResult Index()
      {
         return PartialView(new Models.PermissionModel.ListInfo());
      }

      public ActionResult IndexPartial(Models.PermissionModel.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.PermissionModel.Create());
      }

      [Logic("Thêm")]
      [HttpPost]
      public ActionResult Add(Models.PermissionModel vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.PermissionModel.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa")]
      [HttpPost]
      public ActionResult Edit(Models.PermissionModel vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa")]
      public ActionResult Delete(int id)
      {
         Models.PermissionModel.Delete(id);
         return Nothing();
      }

      public ActionResult Export()
      {
         var vm = new Models.PermissionModel.ListInfo();
         var st = new DevExpress.Web.Mvc.GridViewSettings()
         {
            Name = "Permission"
         };
         st.Columns.Add("Oid", "Id");
         st.Columns.Add("Name");
         st.Columns.Add("System");
         st.Columns.Add("Note");
         return DevExpress.Web.Mvc.GridViewExtension.ExportToXlsx(st, vm.GetData());
      }
   }
}