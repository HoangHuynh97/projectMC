using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebApp.Models
{
   public class CompanyInfo
   {
      [Required, Caption("Tên công ty")]
      public string Name { get; set; }
      [Caption("Tên in báo cáo")]
      public string FullName { get; set; }
      [Caption("Địa chỉ")]
      public string Address { get; set; }
      [Caption("Điện thoại")]
      public string Phone { get; set; }
      [Caption("Số fax")]
      public string Fax { get; set; }
      [Email]
      [Caption("Email")]
      public string Email { get; set; }
      [Caption("Website")]
      public string Website { get; set; }

      public byte[] Logo { get; set; }
      public string RegiserDate { get; private set; }
      public string ExpireDate { get; private set; }
      public string MaxUser { get; private set; }

      public static CompanyInfo Load()
      {
         var db = ApplicationDbContext.Current;
         var model = db.CompanyModel;
         var cur = Convert.ToInt32(db.Session.Evaluate<DataModel.User>(CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("Companies[Oid=?] && !Roles[Role=?]", db.CompanyId.Value, Constant.Role.System)));
         var vm = new CompanyInfo()
         {
            Name = model.Name,
            FullName = model.FullName,
            Address = model.Address,
            Phone = model.Phone,
            Fax = model.Fax,
            Email = model.Email,
            Website = model.Website,
            Logo = model.Logo,
            RegiserDate = model.RegisterDate.ToShortDateString(),
            ExpireDate = model.ExpireDate?.ToShortDateString(),
            MaxUser = cur + "/" + model.MaxUser
         };

         return vm;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         var model = ApplicationDbContext.Current.CompanyModel;
         model.Name = Name;
         model.FullName = FullName;
         model.Address = Address;
         model.Phone = Phone;
         model.Fax = Fax;
         model.Email = Email;
         model.Website = Website;
         model.Logo = Logo;
         db.Session.CommitChanges();
      }
   }
}