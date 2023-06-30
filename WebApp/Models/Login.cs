using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApp.Models
{
   public class Login
   {
      [Required]
      public string UserName { get; set; }

      [Required]
      public string Password { get; set; }

      public bool RememberMe { get; set; }
      public string ReturnUrl { get; set; }
      public string ResetId { get; set; }

      [Required]
      public bool Agree { get; set; }
      public string Agreement => System.Web.Configuration.WebConfigurationManager.AppSettings["Agreement"];
      public string Copyright => System.Web.Configuration.WebConfigurationManager.AppSettings["Copyright"];
      public bool AllowForgotPassword => !string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["SmtpServer"]);

      public bool ValidateResetId()
      {
         if (string.IsNullOrEmpty(ResetId)) return false;
         var db = Core.ApplicationDbContext.Current;
         var user = db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("ResetPasswordId==?", ResetId));
         return user != null && user.ResetPasswordExpire > DateTime.Now;
      }

      public static bool CheckLogin(string userName)
      {
         var db = Core.ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("UserName=?", userName));
         if (model == null)
            return false;
         if (!model.Active)
            return false;
         var companies = model.GetAccessCompanies();
         if (companies.Count == 0)
            return model.Roles.Any(it => it.Role == Constant.Role.System);
         return true;
      }

      public static bool CheckLogin(string loginProvider, string providerKey)
      {
         var session = Core.ApplicationDbContext.Current.Session;
         var model = session.FindObject<DataModel.User>(CriteriaOperator.Parse("Logins[LoginProvider=? && ProviderKey=?]", loginProvider, providerKey));
         if (model == null)
            return false;
         return CheckLogin(model.UserName);
      }

      public static int ResetPassword(string id, string passwordHash)
      {
         var db = Core.ApplicationDbContext.Current;
         var user = db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("ResetPasswordId==?", id));
         user.PasswordHash = passwordHash;
         user.ResetPasswordId = null;
         user.ResetPasswordExpire = null;
         db.Session.CommitChanges();
         return user.Oid;
      }
   }
}