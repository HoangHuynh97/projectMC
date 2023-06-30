using DevExpress.Data.Filtering;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class UserProfile
   {
      [Core.Caption("Họ và tên")]
      public string FullName { get; set; }
      [Core.Caption("Địa chỉ")]
      public string Address { get; set; }
      [Core.Caption("Điện thoại")]
      public string Phone { get; set; }
      [Core.Email]
      [Core.Caption("Email")]
      [Core.Remote("CheckEmail", "Home", "Email đã có đăng ký")]
      public string Email { get; set; }
      [Core.Caption("Hình ảnh")]
      public byte[] Image { get; set; }
      public Dictionary<string, bool> Logins { get; private set; }

      public static UserProfile Create()
      {
         var model = Core.ApplicationDbContext.Current.UserModel;
         var result = new UserProfile()
         {
            FullName = model.FullName,
            Address = model.Address,
            Phone = model.Phone,
            Email = model.Email,
            Image = model.Image,
            Logins = new Dictionary<string, bool>()
         };
         var types = HttpContext.Current.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
         foreach (var t in types)
            result.Logins.Add(t.AuthenticationType, model.Logins.Any(p => p.LoginProvider == t.AuthenticationType));
         return result;
      }

      public void Save()
      {
         var model = Core.ApplicationDbContext.Current.UserModel;
         model.FullName = FullName;
         model.Address = Address;
         model.Phone = Phone;
         model.Email = Email;
         model.Image = Image;
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static void RemoveLogin(string provider)
      {
         var model = Core.ApplicationDbContext.Current.UserModel;
         var login = model.Logins.FirstOrDefault(p => p.LoginProvider == provider);
         if (login != null)
         {
            login.Delete();
            Core.ApplicationDbContext.Current.Session.CommitChanges();
         }
      }

      internal static bool CheckEmail(string email)
      {
         var model = Core.ApplicationDbContext.Current.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("Oid!=? && Email=?", Core.ApplicationDbContext.Current.UserId.Value, email));
         return model == null;
      }
   }
}