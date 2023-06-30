using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Authorize]
   public class HomeController : BaseController
   {
      internal class ChallengeResult : HttpUnauthorizedResult
      {
         public static string XsrfKey = "XsrfWebAppUserId";

         public ChallengeResult(string provider, string redirectUri)
             : this(provider, redirectUri, null)
         {
         }

         public ChallengeResult(string provider, string redirectUri, string userId)
         {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
         }

         public string LoginProvider { get; set; }
         public string RedirectUri { get; set; }
         public string UserId { get; set; }

         public override void ExecuteResult(ControllerContext context)
         {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
            {
               properties.Dictionary[XsrfKey] = UserId;
            }
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
         }
      }

      public ActionResult Index()
      {
         //find displaymode
         var modes = DisplayModeProvider.Instance.Modes;
         IDisplayMode cur = null;
         for (var i = 0; i < modes.Count; i++)
         {
            if (modes[i].CanHandleContext(HttpContext))
            {
               cur = modes[i];
               break;
            }
         }
         if (cur.DisplayModeId == "Mobile" && System.IO.File.Exists(Server.MapPath("~/Mobile/index.html")))
         {
            return Redirect("~/Mobile");
         }

         return View(Models.Home.Create());
      }

      [AllowAnonymous]
      public ActionResult Login(string returnUrl, string message, string reset)
      {
         switch (message)
         {
            case "external":
               ViewBag.ErrorMessage = T("Tài khoản chưa được đăng ký trong hệ thống<br/>Vui lòng đăng nhập lại");
               break;
         }
         var vm = new Models.Login() { ReturnUrl = returnUrl, ResetId = reset };
         return View(vm);
      }

      [AllowAnonymous]
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Login(Models.Login model)
      {
         if (ModelState.IsValid)
         {
            if (!Models.Login.CheckLogin(model.UserName))
               ViewBag.ErrorMessage = T("Tên đăng nhập hoặc mật khẩu không đúng");
            else
            {
               // This doesn't count login failures towards account lockout
               // To enable password failures to trigger account lockout, change to shouldLockout: true
               try
               {
                  var result = SignInManager.PasswordSignIn(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
                  switch (result)
                  {
                     case SignInStatus.Success:
                        if (Url.IsLocalUrl(model.ReturnUrl))
                           return Redirect(model.ReturnUrl);
                        return RedirectToAction("Index");
                     case SignInStatus.LockedOut:
                        ViewBag.ErrorMessage = T("Tài khoản đã bị khóa");
                        break;
                     case SignInStatus.RequiresVerification:
                     //return RedirectToAction("SendCode", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                     case SignInStatus.Failure:
                     default:
                        ViewBag.ErrorMessage = T("Tên đăng nhập hoặc mật khẩu không đúng");
                        break;
                  }
               }
               catch (System.Exception ex)
               {
                  ViewBag.ErrorMessage = T(ex.Message);
               }
            }
         }
         return View(model);
      }

      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult ExternalLogin(string provider, string returnUrl)
      {
         var url = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });
         return new ChallengeResult(provider, url);
      }

      [AllowAnonymous]
      public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
      {
         var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

         // Sign in the user with this external login provider if the user already has a login
         if (loginInfo != null && Models.Login.CheckLogin(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey))
         {
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
               case SignInStatus.Success:
                  if (Url.IsLocalUrl(returnUrl))
                     return Redirect(returnUrl);
                  return Redirect("/");
            }
         }

         return RedirectToAction("Login", new { ReturnUrl = returnUrl, Message = "external" });
      }

      public ActionResult LogOff()
      {
         AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
         return RedirectToAction("Login");
      }

      [AllowAnonymous]
      public ActionResult ChangeLanguage(string code)
      {
         Core.Language.SetCurrentLanguage(code);
         return Nothing();
      }

      public ActionResult ChangeSkin(string name)
      {
         Core.Skin.Current = name;
         return Nothing();
      }

      public ActionResult ChangePassword()
      {
         return PartialView(new Models.ChangePassword());
      }

      [HttpPost]
      public async Task<ActionResult> ChangePassword(Models.ChangePassword vm)
      {
         if (!ModelState.IsValid)
            return PartialView(vm);
         var result = await SignInManager.UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), vm.OldPassword, vm.NewPassword);
         if (!result.Succeeded)
            return ShowInfo(T("Lỗi"), T(result.Errors.First()));
         var user = await SignInManager.UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
         if (user != null)
         {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
         }
         return Nothing();
      }

      public ActionResult ShowDialog(Models.Dialog vm)
      {
         return PartialView(vm);
      }

      public ActionResult UpdateProfile()
      {
         return PartialView(Models.UserProfile.Create());
      }

      [HttpPost]
      public ActionResult UpdateProfile(Models.UserProfile vm)
      {
         if (!ModelState.IsValid)
            return PartialView(vm);
         vm.Save();
         return Nothing();
      }

      public ActionResult CheckEmail(string email)
      {
         return Json(Models.UserProfile.CheckEmail(email));
      }

      [HttpPost]
      public ActionResult UpdateUserImage()
      {
         return BinaryImageEditExtension.GetCallbackResult();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult LinkLogin(string provider)
      {
         return new ChallengeResult(provider, Url.Action("LinkLoginCallback"), User.Identity.GetUserId());
      }

      public ActionResult LinkLoginCallback()
      {
         var loginInfo = AuthenticationManager.GetExternalLoginInfo(ChallengeResult.XsrfKey, User.Identity.GetUserId());
         if (loginInfo != null)
         {
            SignInManager.UserManager.AddLogin(User.Identity.GetUserId<int>(), loginInfo.Login);
         }
         return Redirect("/#/Home/UpdateProfile");
      }

      [HttpPost]
      public ActionResult RemoveLogin(string provider)
      {
         Models.UserProfile.RemoveLogin(provider);
         return Nothing();
      }

      public ActionResult Preview()
      {
         return PartialView(GetCurrentReport());
      }

      public ActionResult Export()
      {
         var rpt = GetCurrentReport();
         if (rpt.Report == null) return Nothing();
         return DocumentViewerExtension.ExportTo(rpt.Report);
      }

      public ActionResult SelectCompany()
      {
         return PartialView(Models.SelectCompany.Create());
      }

      [HttpPost]
      public ActionResult SelectCompany(Models.SelectCompany vm)
      {
         if (vm.Company.HasValue && !ApplicationDbContext.Current.UserModel.GetAccessCompanies().Any(p => p.Oid == vm.Company.Value))
            ModelState.AddModelError("Company", T("Bạn không có quyền truy cập dữ liệu"));

         if (!ModelState.IsValid)
            return PartialView(vm);
         vm.Update();
         var user = SignInManager.UserManager.FindById(User.Identity.GetUserId<int>());
         SignInManager.SignIn(user, false, false);
         return JavaScript("WebApp.homePage();popupSelectCompany.Hide();");
      }

      public ActionResult HomePage()
      {
         if (ApplicationDbContext.Current.CompanyId.HasValue)
            return PartialView(Models.CompanyInfo.Load());
         return Nothing();
      }

      [AllowAnonymous]
      [HttpPost]
      public ActionResult ForgotPassword(string email)
      {
         bool success = false;
         string message = "";
         if (string.IsNullOrEmpty(email))
         {
            message = T("Email không thể để trống");
         }
         else
         {
            if (Models.UserModel.CreateResetPasswordLink(email, out var id, out var user))
            {
               success = EmailProvider.Instance.SendTemplate(email, T("Khôi phục mật khẩu đăng nhập {0}", Request.Url.Host), T("reset_password.html"),
                  new Dictionary<string, string>() {
                     { "link", Url.Action("Login", "Home", new {reset = id}, Request.Url.Scheme)},
                     { "user", user }
                  });
               message = success
                  ? T("Chúng tôi đã gửi hướng dẫn đến email của bạn")
                  : T("Lỗi hệ thống trong quá trình gửi mail");
            }
            else
               message = T("Không tìm thấy email đăng ký trong hệ thống");
         }

         return Json(new { success, message });
      }

      [AllowAnonymous, HttpPost]
      public ActionResult ResetPassword(string id, string password)
      {
         var success = false;
         var message = "";
         var vm = new Models.Login() { ResetId = id, Password = password };
         if (!vm.ValidateResetId())
            message = T("Yêu cầu khôi phục mật khẩu không tồn tại hoặc đã quá hạn");
         else if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 20)
            message = T("Mật khẩu phải ít nhất 6 ký tự, tối đa 20 ký tự");
         else
         {
            var h = SignInManager.UserManager.PasswordHasher.HashPassword(vm.Password);
            var userId = Models.Login.ResetPassword(vm.ResetId, h);
            SignInManager.UserManager.UpdateSecurityStamp(userId);
            SignInManager.SignIn(SignInManager.UserManager.FindById(userId), false, false);
            success = true;
         }
         return Json(new { success, message });
      }

      [AllowAnonymous]
      public ActionResult Help()
      {
         return View();
      }
   }
}