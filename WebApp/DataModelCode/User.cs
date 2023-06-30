using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebApp.DataModel
{

   public partial class User
   {
      public User(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }

      private DateTimeOffset? fLastLogin;
      [ValueConverter(typeof(Core.DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset? LastLogin
      {
         get { return fLastLogin; }
         set { SetPropertyValue("LastLogin", ref fLastLogin, value); }
      }

      private DateTimeOffset? fResetPasswordExpire;
      [ValueConverter(typeof(Core.DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset? ResetPasswordExpire
      {
         get { return fResetPasswordExpire; }
         set { SetPropertyValue("ResetPasswordExpire", ref fResetPasswordExpire, value); }
      }

      public List<Company> GetAccessCompanies()
      {
         if (Roles.Any(p => p.Role == Constant.Role.System) && this.Roles.Any(p => p.Role == Constant.Role.Admin))
            return (new XPCollection<Company>(Session)).ToList();
         return Companies.Where(p => p.Active && (p.ExpireDate == null || p.ExpireDate > DateTime.Today)).ToList();
      }
   }

}
