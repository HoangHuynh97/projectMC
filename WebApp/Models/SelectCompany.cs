using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class SelectCompany
   {
      [Core.Required]
      public int? Company { get; set; }

      public static SelectCompany Create()
      {
         var vm = new SelectCompany()
         {
            Company = Core.ApplicationDbContext.Current.CompanyId
         };
         return vm;
      }

      public List<KeyValuePair<int, string>> GetData()
      {
         return Core.ApplicationDbContext.Current.UserModel.GetAccessCompanies()
            .Select(p => new KeyValuePair<int, string>(p.Oid, p.Name))
            .OrderBy(p => p.Value)
            .ToList();
      }

      public void Update()
      {
         var db = Core.ApplicationDbContext.Current;
         var user = db.UserModel;
         if (user.GetAccessCompanies().Any(c => c.Oid==Company.Value))
         {
            user.CurrentCompany = Company.Value;
            db.Session.CommitChanges();
         }
      }
   }
}