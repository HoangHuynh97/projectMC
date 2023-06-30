using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebApp.Models
{
   public class Company
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Company> GetData()
         {
            return Core.ApplicationDbContext.Current.Session.Query<DataModel.Company>();
         }
      }

      public int Id { get; set; }
      [Core.Required]
      [Core.Caption("Tên công ty")]
      public string Name { get; set; }
      [Core.Caption("Tên in báo cáo")]
      public string FullName { get; set; }
      [Core.Caption("Địa chỉ")]
      public string Address { get; set; }
      [Core.Caption("Điện thoại")]
      public string Phone { get; set; }
      [Core.Caption("Số fax")]
      public string Fax { get; set; }
      [Core.Email]
      [Core.Caption("Email")]
      public string Email { get; set; }
      [Core.Caption("Website")]
      public string Website { get; set; }
      [Core.Caption("Người liên hệ")]
      public string Director { get; set; }
      [Core.Caption("ĐT liên hệ")]
      public string DirectorPhone { get; set; }
      [Core.Required]
      [Core.Caption("Ngày đăng ký")]
      public DateTime RegiserDate { get; set; }
      [Core.Caption("Ngày hết hạn")]
      public DateTime? ExpireDate { get; set; }
      [Core.Caption("Đang sử dụng")]
      public bool Active { get; set; }
      [Core.Caption("Ghi chú")]
      [Core.Template("MultilineText")]
      public string Note { get; set; }
      public int MaxUser { get; set; }

      public Company() { }
      public Company(DataModel.Company model)
      {
         Id = model.Oid;
         Name = model.Name;
         FullName = model.FullName;
         Address = model.Address;
         Phone = model.Phone;
         Fax = model.Fax;
         Email = model.Email;
         Website = model.Website;
         Director = model.Director;
         DirectorPhone = model.DirectorPhone;
         RegiserDate = model.RegisterDate;
         ExpireDate = model.ExpireDate;
         Active = model.Active;
         Note = model.Note;
         MaxUser = model.MaxUser;
      }

      public void Save()
      {
         DataModel.Company model;
         if (Id == 0)
            model = new DataModel.Company(Core.ApplicationDbContext.Current.Session);
         else
            model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.Company>(Id);
         if (model == null) return;
         model.Name = Name;
         model.FullName = FullName;
         model.Address = Address;
         model.Phone = Phone;
         model.Fax = Fax;
         model.Email = Email;
         model.Website = Website;
         model.Director = Director;
         model.DirectorPhone = DirectorPhone;
         model.RegisterDate = RegiserDate;
         model.ExpireDate = ExpireDate;
         model.Active = Active;
         model.Note = Note;
         model.MaxUser = MaxUser;
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static Company Create()
      {
         var vm = new Company()
         {
            Active = true,
            RegiserDate = DateTime.Today,
            MaxUser = 1
         };
         return vm;
      }

      public static Company Load(int id)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.Company>(id);
         if (model == null) return null;
         return new Company(model);
      }

      public static void Delete(int id)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.Company>(id);
         if (model == null) return;
         model.Delete();
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }
   }
}