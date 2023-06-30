using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Quản lý tài khoản", Roles = Constant.Role.System)]
   public class UserController : BaseController
   {
      public ActionResult Index()
      {
         return PartialView(new Models.UserModel.ListInfo());
      }

      public ActionResult IndexPartial(Models.UserModel.ListInfo vm)
      {
         return PartialView(vm);
      }


      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("EditTemplate", Models.UserModel.Create());
      }

      [Logic("Thêm")]
      [HttpPost]
      public ActionResult Add(Models.UserModel vm)
      {
         if (!Models.UserModel.CheckUserName(vm.Id, vm.UserName))
            ModelState.AddModelError("UserName", T("Tên đăng nhập đã có đăng ký"));
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);

         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.UserModel.Load(id);
         if (vm == null) return NotFound();
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa")]
      [HttpPost]
      public ActionResult Edit(Models.UserModel vm)
      {
         if (!Models.UserModel.CheckUserName(vm.Id, vm.UserName))
            ModelState.AddModelError("UserName", T("Tên đăng nhập đã có đăng ký"));
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         SignInManager.UserManager.UpdateSecurityStamp(vm.Id);
         return Nothing();
      }

      [Logic("Xóa")]
      public ActionResult Delete(int id)
      {
         Models.UserModel.Delete(id);
         return Nothing();
      }

      public ActionResult Export()
      {
         var vm = new Models.UserModel.ListInfo();
         var st = new DevExpress.Web.Mvc.GridViewSettings()
         {
            Name = "User"
         };
         st.Columns.Add("Oid", "Id");
         st.Columns.Add("UserName");
         st.Columns.Add("FullName");
         st.Columns.Add("Address");
         st.Columns.Add("Phone");
         st.Columns.Add("Email");
         st.Columns.Add("System");
         st.Columns.Add("Admin");
         st.Columns.Add("Permission.Name","Permission");
         st.Columns.Add("Active");
         st.Columns.Add("Note");
         return GridViewExtension.ExportToXlsx(st, vm.GetData());
      }

      [HttpPost]
      public ActionResult UpdateUserImage()
      {
         return BinaryImageEditExtension.GetCallbackResult();
      }

      public ActionResult CheckUserName(int id, string userName)
      {
         return Json(Models.UserModel.CheckUserName(id, userName));
      }

      public ActionResult CheckEmail(int id, string email)
      {
         return Json(Models.UserModel.CheckEmail(id, email));
      }

      [Logic("Đổi mật khẩu")]
      public ActionResult ChangePassword(int id)
      {
         return PartialView(new Models.UserModel.ChangePassword() { Id = id });
      }

      [HttpPost]
      [Logic("Đổi mật khẩu")]
      public ActionResult ChangePassword(Models.UserModel.ChangePassword vm)
      {
         if (!ModelState.IsValid)
            return PartialView(vm);
         var h = SignInManager.UserManager.PasswordHasher.HashPassword(vm.Password);
         Models.UserModel.SetPassword(vm.Id, h);
         SignInManager.UserManager.UpdateSecurityStamp(vm.Id);
         return Nothing();
      }
   }
}