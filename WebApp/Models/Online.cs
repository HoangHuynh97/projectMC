using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebApp.Models
{
   public class Online
   {
      public int Id { get; set; }
      public string UserName { get; set; }
      public string FullName { get; set; }
      public string Phone { get; set; }
      public string Company { get; set; }

      public static List<Online> Load()
      {
         var db = Core.ApplicationDbContext.Current;
         var xpc = new XPCollection<DataModel.User>(db.Session);
         xpc.Sorting.Add(new SortProperty("UserName", DevExpress.Xpo.DB.SortingDirection.Ascending));
         xpc.Criteria = new InOperator("UserName", Core.MessageHub.GetOnline().Keys.ToArray());
         return xpc.Select(u => {
            var vm = new Online()
            {
               Id = u.Oid,
               UserName = u.UserName,
               FullName = u.FullName,
               Phone = u.Phone
            };
            if (u.Roles.Any(p => p.Role == Constant.Role.System))
               vm.Company = "Hệ thống";
            else if (u.CurrentCompany.HasValue)
            {
               var c = db.Session.GetObjectByKey<DataModel.Company>(u.CurrentCompany.Value);
               if (c != null)
                  vm.Company = c.Name;
            }
            else
            {
               var ac = u.GetAccessCompanies();
               if (ac.Count == 1)
                  vm.Company = ac[0].Name;
            }
            return vm;
         }).ToList();
      }
   }
}