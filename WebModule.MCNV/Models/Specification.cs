using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class Specification
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public object GetData()
         {
            var db = ApplicationDbContext.Current;
            var query = from el in db.Session.Query<DataModel.Specification>()
                        where el.Company == db.CompanyId.Value
                        select new { el.Oid, el.Name, Service = el.Service.Name, ServiceIndex = el.Service.OrderIndex };
            return query.ToList();
         }
      }

      public int Id { get; set; }
      [Caption("Tên kỹ thuật"), Required]
      public string Name { get; set; }
      [Caption("Hoạt động PHCN"), Required, Template("Service")]
      public int? Service { get; set; }

      public Specification() { }
      public Specification(DataModel.Specification model)
      {
         Id = model.Oid;
         Name = model.Name;
         Service = model.Service.Oid;
      }
      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Specification model;
         if (Id == 0)
            model = new DataModel.Specification(db.Session);
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;
         if (Models.Service.TryGetModel(Service.Value, out var sv))
            model.Service = sv;
         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static Specification Create() => new Specification();

      public static Specification Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Specification(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.Specification model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Specification>(id);
         if (model == null || model.Company != db.CompanyId.Value) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.Specification>()
         where el.Company == ApplicationDbContext.Current.CompanyId.Value
         orderby el.OrderIndex, el.Name ascending
         select new { el.Oid, el.Name };
   }
}