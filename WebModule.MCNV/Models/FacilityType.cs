using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class FacilityType
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.FacilityType> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryByCompany<DataModel.FacilityType>();
         }
      }

      public int Id { get; set; }
      [Caption("Tên loại đơn vị"), Required]
      public string Name { get; set; }

      public FacilityType() { }

      public FacilityType(DataModel.FacilityType model)
      {
         Id = model.Oid;
         Name = model.Name;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.FacilityType model;
         if (Id == 0)
            model = new DataModel.FacilityType(db.Session) { };
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;

         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static FacilityType Create() => new FacilityType();

      public static FacilityType Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new FacilityType(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.FacilityType model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchByCompany<DataModel.FacilityType>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryByCompany<DataModel.FacilityType>()
         select new { el.Oid, el.Name };

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.QueryByCompany<DataModel.Facility>()
                  where el.FacilityType.Oid == id
                  select el).Count();
         return c == 0;
      }
   }
}