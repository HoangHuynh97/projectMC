using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace WebApp.Core
{
   public class BaseController : Controller
   {
      ApplicationSignInManager _signInManager;
      protected ApplicationSignInManager SignInManager
      {
         get
         {
            if (_signInManager == null)
               _signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            return _signInManager;
         }
      }

      ApplicationUserManager _userManager;
      protected ApplicationUserManager UserManager
      {
         get
         {
            if (_userManager == null)
               _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            return _userManager;
         }
      }

      private IAuthenticationManager _authenticationManager;
      protected IAuthenticationManager AuthenticationManager
      {
         get
         {
            if (_authenticationManager == null)
               _authenticationManager = HttpContext.GetOwinContext().Authentication;
            return _authenticationManager;
         }
      }

      protected string T(string key, params object[] values)
      {
         return Language.T(key, values);
      }

      protected bool CheckPermission(string logic)
      {
         var func = this.GetType().GetCustomAttributes(typeof(FunctionAttribute), false)[0] as FunctionAttribute;
         return CheckPermission(func.Name, logic);
      }

      protected bool CheckPermission(string function, string logic = "")
      {
         var module = this.GetType().Assembly.ManifestModule.Name;
         return this.HttpContext.CheckPermission(module, function, logic);
      }

      protected ActionResult ShowInfo(string title, string msg)
      {
         return JavaScript("WebApp.showInfo('" + title + "', '" + msg + "');");
      }

      protected ActionResult NotFound()
      {
         return ShowInfo(T("Lỗi"), T("Không tìm thấy dữ liệu"));
      }

      protected ActionResult Nothing()
      {
         Response.ContentType = "text/html";
         return null;
      }

      protected ReportInfo SetCurrentReport(string name)
      {
         var rpt = new ReportInfo(name);
         Session["CurrentReport"] = rpt;
         return rpt;
      }

      protected ReportInfo GetCurrentReport()
      {
         return (ReportInfo)Session["CurrentReport"];
      }

      #region signalR method
      static readonly Lazy<IHubContext> instance = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<MessageHub>());

      protected void SendTo(Func<DataModel.User, bool> predicate, string method, object data)
      {
         var users = ApplicationDbContext.Current.CompanyModel.GetCompanyUsers().Where(u => u.Active && u.UserName != User.Identity.Name).Where(predicate).Select(p => p.UserName).ToList();
         instance.Value?.Clients.Users(users).message(method, data);
      }

      protected void SendToAll(string method, object data)
      {
         SendTo(u => true, method, data);
      }

      protected void SendToAdmin(string method, object data)
      {
         SendTo(u => u.Roles.Any(p => p.Role == Constant.Role.Admin), method, data);
      }

      protected void SendNotify(string msg)
      {
         instance.Value?.Clients.All.notify(msg);
      }
      #endregion
   }
}