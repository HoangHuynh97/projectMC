using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Công ty", Roles = Constant.Role.System)]
   public class CompanyController : Core.BaseController
   {
      public ActionResult Index()
      {
         return PartialView(new Models.Company.ListInfo());
      }

      public ActionResult IndexPartial(Models.Company.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.Company.Create());
      }

      [Logic("Thêm")]
      [HttpPost]
      public ActionResult Add(Models.Company vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Company.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa")]
      [HttpPost]
      public ActionResult Edit(Models.Company vm)
      {
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Xóa")]
      public ActionResult Delete(int id)
      {
         Models.Company.Delete(id);
         return Nothing();
      }

      public ActionResult Export()
      {
         var vm = new Models.Company.ListInfo();
         var st = new DevExpress.Web.Mvc.GridViewSettings()
         {
            Name = "Company"
         };
         st.Columns.Add("Oid", "Id");
         st.Columns.Add("Name");
         st.Columns.Add("FullName");
         st.Columns.Add("Address");
         st.Columns.Add("Phone");
         st.Columns.Add("Fax");
         st.Columns.Add("Director");
         st.Columns.Add("DirectorPhone");
         st.Columns.Add("RegisterDate");
         st.Columns.Add("ExpireDate");
         st.Columns.Add("Active");
         st.Columns.Add("Note");
         return DevExpress.Web.Mvc.GridViewExtension.ExportToXlsx(st, vm.GetData());
      }
   }
}