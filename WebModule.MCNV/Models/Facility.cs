using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebModule.MCNV.Models
{
   public class Facility : AddressBase
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Facility> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryFacility();
         }
      }

      public class ViewInfo
      {
         [Caption("Tên đơn vị")]
         public string Name { get; }
         [Caption("Loại hình đơn vị")]
         public string FacilityType { get; }
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

         public ViewInfo(DataModel.Facility model)
         {
            Name= model.Name;
            FacilityType = model.FacilityType.Name;
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
      [Remote("CheckName", "Facility", "Tên đơn vị này đã có sử dụng", AdditionalFields = "Id")]
      public string Name { get; set; }
      [Caption("Điện thoại"), Template("Phone")]
      public string Phone { get; set; }
      [Caption("Email"), Email]
      public string Email { get; set; }
      [Caption("Website")]
      public string Website { get; set; }
      [Caption("Loại hình đơn vị"), Required, Template("FacilityType")]
      public int? FacilityType { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public bool Locked { get; }

      public Facility() { }
      public Facility(DataModel.Facility model)
      {
         Id = model.Oid;
         Name = model.Name;
         Phone = model.Phone;
         Email = model.Email;
         Website = model.Website;
         FacilityType = model.FacilityType?.Oid;
         Province = model.Province.Oid;
         District = model.District.Oid;
         Ward = model.Ward?.Oid;
         Address = model.Address;
         Note = model.Note;
         Locked = CheckLocked(model);
      }
      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Facility model;
         if (Id == 0)
            model = new DataModel.Facility(db.Session) ;
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;
         model.Phone = Phone;
         model.Email = Email;
         model.Website = Website;
         if (Models.FacilityType.TryGetModel(FacilityType.Value, out var ft))
            model.FacilityType = ft;
         if (Models.Province.TryGetModel(Province.Value, out var pr))
            model.Province = pr;
         if (Models.District.TryGetModel(District.Value, out var di))
            model.District = di;
         if (!Ward.HasValue)
            model.Ward = null;
         else if (Models.Ward.TryGetModel(Ward.Value, out var wa))
            model.Ward = wa;
         model.Address = Address;
         model.Note = Note;
         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static Facility Create() => new Facility();

      public static Facility Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Facility(model);
      }

      public static string Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return "Bạn không có quyền xóa dữ liệu";
         if (CheckLocked(model)) return "Dữ liệu đã bị khóa";
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
         return null;
      }

      public static bool TryGetModel(int id, out DataModel.Facility model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchFacility(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryFacility()
         orderby el.Name ascending
         select new { el.Oid, el.Name, Province = el.Province.Name };

      public static bool CanDelete(int id, out string error)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.Medical>()
                  where el.Facility.Oid == id
                  select el).Count();
         if (c > 0)
         {
            error = "Hồ sơ PHCN";
            return false;
         }

         c = (from el in db.Session.Query<DataModel.Doctor>()
              where el.Facility.Oid == id
              select el).Count();
         if (c > 0)
         {
            error = "Cán bộ PHCN";
            return false;
         }

         c = (from el in db.Session.Query<WebApp.DataModel.User>()
              where el.GroupId == id
              select el).Count();
         if (c > 0)
         {
            error = "Người sử dụng";
            return false;
         }

         error = "";
         return true;
      }

      public static bool CheckLocked(DataModel.Facility model) => LockedData.CheckLocked(model.DateCreate.Date);

      public static bool CheckName(int id, string name)
      {
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Facility>(CriteriaOperator.Parse("Company=? && Name=? && This!=?", db.CompanyId, name, id));
         return model == null;
      }
   }
}