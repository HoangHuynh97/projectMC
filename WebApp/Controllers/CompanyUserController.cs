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
   [Function("Người sử dụng")]
   public class CompanyUserController : BaseController
   {
      public ActionResult Index()
      {
         return PartialView(new Models.CompanyUser.ListInfo());
      }

      public ActionResult IndexPartial(Models.CompanyUser.ListInfo vm)
      {
         return PartialView(vm);
      }


      [Logic("Thêm")]
      public ActionResult Add()
      {
         if (!Models.CompanyUser.CheckMaxUser())
            return ShowInfo(T("Hệ thống"), T("Bạn không thể tạo người dùng mới do đã đạt giới hạn số lượng người sử dụng tối đa cho phép"));
         return PartialView("EditTemplate", Models.CompanyUser.Create());
      }

      [Logic("Thêm")]
      [HttpPost]
      public ActionResult Add(Models.CompanyUser vm)
      {
         vm.Id = 0;//prevent hacked
         if (!Models.CompanyUser.CheckMaxUser())
            return ShowInfo(T("Hệ thống"), T("Bạn không thể tạo người dùng mới do đã đạt giới hạn số lượng người sử dụng tối đa cho phép"));
         if (!Models.CompanyUser.CheckUserName(vm.Id, vm.UserName))
            ModelState.AddModelError("UserName", T("Tên đăng nhập đã có đăng ký"));
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.CompanyUser.Load(id);
         if (vm == null) return NotFound();
         if (!Models.CompanyUser.CanUpdateAdmin(id) || !Models.CompanyUser.CanUpdateUser(vm.Id))
            return ShowInfo(T("Hệ thống"), T("Bạn không có quyền cập nhật người dùng này"));
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa")]
      [HttpPost]
      public ActionResult Edit(Models.CompanyUser vm)
      {
         if (!Models.CompanyUser.CheckUserName(vm.Id, vm.UserName))
            ModelState.AddModelError("UserName", T("Tên đăng nhập đã có đăng ký"));
         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         if (!Models.CompanyUser.CanUpdateAdmin(vm.Id) || !Models.CompanyUser.CanUpdateUser(vm.Id))
            return ShowInfo(T("Hệ thống"), T("Bạn không có quyền cập nhật người dùng này"));
         vm.Save();
         SignInManager.UserManager.UpdateSecurityStamp(vm.Id);
         return Nothing();
      }

      [Logic("Xóa")]
      public ActionResult Delete(int id)
      {
         if (!Models.CompanyUser.CanUpdateAdmin(id) || !Models.CompanyUser.CanUpdateUser(id))
            return ShowInfo(T("Hệ thống"), T("Bạn không có quyền cập nhật người dùng này"));
         Models.CompanyUser.Delete(id);
         return Nothing();
      }

      public ActionResult Export()
      {
         var vm = new Models.CompanyUser.ListInfo();
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
         st.Columns.Add("Admin");
         st.Columns.Add("Permission.Name", "Permission");
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
         return Json(Models.CompanyUser.CheckUserName(id, userName));
      }

      public ActionResult CheckEmail(int id, string email)
      {
         return Json(Models.CompanyUser.CheckEmail(id, email));
      }

      [Logic("Đổi mật khẩu")]
      public ActionResult ChangePassword(int id)
      {
         if (!Models.CompanyUser.CanUpdateAdmin(id) || !Models.CompanyUser.CanUpdateUser(id))
            return ShowInfo(T("Hệ thống"), T("Bạn không có quyền cập nhật người dùng này"));
         return PartialView(new Models.CompanyUser.ChangePassword() { Id = id });
      }

      [HttpPost]
      [Logic("Đổi mật khẩu")]
      public ActionResult ChangePassword(Models.CompanyUser.ChangePassword vm)
      {
         if (!ModelState.IsValid)
            return PartialView(vm);
         if (!Models.CompanyUser.CanUpdateAdmin(vm.Id) || !Models.CompanyUser.CanUpdateUser(vm.Id))
            return ShowInfo(T("Hệ thống"), T("Bạn không có quyền cập nhật người dùng này"));
         var h = SignInManager.UserManager.PasswordHasher.HashPassword(vm.Password);
         Models.CompanyUser.SetPassword(vm.Id, h);
         SignInManager.UserManager.UpdateSecurityStamp(vm.Id);
         return Nothing();
      }
   }
}