using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Phân quyền sử dụng")]
    public class CompanyPermissionController : BaseController
    {
      public ActionResult Index()
      {
         return PartialView(new Models.CompanyPermission.ListInfo());
      }

      public ActionResult IndexPartial(Models.CompanyPermission.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.CompanyPermission.Create());
      }

      [Logic("Thêm")]
      [HttpPost]
      public ActionResult Add(Models.CompanyPermission vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.CompanyPermission.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa")]
      [HttpPost]
      public ActionResult Edit(Models.CompanyPermission vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa")]
      public ActionResult Delete(int id)
      {
         Models.CompanyPermission.Delete(id);
         return Nothing();
      }

      public ActionResult Export()
      {
         var vm = new Models.CompanyPermission.ListInfo();
         var st = new DevExpress.Web.Mvc.GridViewSettings()
         {
            Name = "Permission"
         };
         st.Columns.Add("Oid", "Id");
         st.Columns.Add("Name");
         st.Columns.Add("Note");
         return DevExpress.Web.Mvc.GridViewExtension.ExportToXlsx(st, vm.GetData());
      }
   }
}