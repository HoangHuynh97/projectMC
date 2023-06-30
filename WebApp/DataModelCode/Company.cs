using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebApp.DataModel
{
   [Core.Audit("công ty", "Name")]
   public partial class Company
   {
      public Company(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }

      public IEnumerable<User> GetCompanyUsers()
      {
         return Users.Where(p => !p.Roles.Any(r => r.Role == Constant.Role.System));
      }
   }
}
