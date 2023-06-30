using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class Institution
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Institution> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryByCompany<DataModel.Institution>();
         }
      }

      public class ViewInfo
      {
         [Caption("Tên đơn vị")]
         public string Name { get; }
         [Caption("Điện thoại")]
         public string Phone { get; }
         [Caption("Email")]
         public string Email { get; }
         [Caption("Website")]
         public string Website { get; }
         [Caption("Địa chỉ")]
         public string Address { get; }
         [Caption("Ghi chú")]
         public string Note { get; }

         public ViewInfo(DataModel.Institution model)
         {
            Name = model.Name;
            Phone = model.Phone;
            Email = model.Email;
            Website = model.Website;
            Address = model.FullAddress;
            Note = model.Note;
         }

         public static ViewInfo Load(int id)
         {
            if (TryGetModel(id, out var model)) return new ViewInfo(model);
            return null;
         }
      }

      public int Id { get; set; }
      [Caption("Tên đơn vị"), Required]
      [Remote("CheckName", "Institution", "Tên đơn vị này đã có sử dụng", AdditionalFields = "Id")]
      public string Name { get; set; }
      [Caption("Điện thoại"), Template("Phone")]
      public string Phone { get; set; }
      [Caption("Email"), Email]
      public string Email { get; set; }
      [Caption("Website")]
      public string Website { get; set; }
      [Caption("Tỉnh/Thành phố"), Template("Province")]
      public int? Province { get; set; }
      [Caption("Quận/Huyện"), Template("District")]
      public int? District { get; set; }
      [Caption("Phường/Xã"), Template("Ward")]
      public int? Ward { get; set; }
      [Caption("Số nhà, tên đường...")]
      public string Address { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public bool Locked { get; }

      public Institution() { }

      public Institution(DataModel.Institution model)
      {
         Id = model.Oid;
         Name = model.Name;
         Note = model.Note;
         Phone = model.Phone;
         Email = model.Email;
         Website = model.Website;
         Province = model.Province?.Oid;
         District = model.District?.Oid;
         Ward = model.Ward?.Oid;
         Address = model.Address;
         Locked = CheckLocked(model);
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Institution model;
         if (Id == 0)
            model = new DataModel.Institution(db.Session) { };
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;
         model.Note = Note;
         model.Phone = Phone;
         model.Email = Email;
         model.Website = Website;
         if (!Province.HasValue)
            model.Province = null;
         else if (Models.Province.TryGetModel(Province.Value, out var pr))
            model.Province = pr;
         if (!District.HasValue)
            model.District = null;
         else if (Models.District.TryGetModel(District.Value, out var di))
            model.District = di;
         if (!Ward.HasValue)
            model.Ward = null;
         else if (Models.Ward.TryGetModel(Ward.Value, out var wa))
            model.Ward = wa;
         model.Address = Address;

         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static Institution Create() => new Institution();

      public static Institution Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Institution(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.Institution model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Institution>(id);
         if (model == null || model.Company != db.CompanyId) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryByCompany<DataModel.Institution>()
         select new { el.Oid, el.Name };

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.QueryByCompany<DataModel.Program>()
                  where el.Institution.Oid == id
                  select el).Count();
         return c == 0;
      }

      public static bool CheckLocked(DataModel.Institution model) => LockedData.CheckLocked(model.DateCreate.Date);

      public static bool CheckName(int id, string name)
      {
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Institution>(CriteriaOperator.Parse("Company=? && Name=? && This!=?", db.CompanyId, name, id));
         return model == null;
      }
   }
}