using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class Treatment
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Treatment> GetData()
         {
            var db = ApplicationDbContext.Current;
            var query = from el in db.Session.Query<DataModel.Treatment>()
                        where el.Company == db.CompanyId
                        select el;
            return query;
         }
      }

      public int Id { get; set; }
      [Caption("Tên gói can thiệp"), Required]
      public string Name { get; set; }

      public Treatment() { }

      public Treatment(DataModel.Treatment model)
      {
         Id = model.Oid;
         Name = model.Name;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Treatment model;
         if (Id == 0)
            model = new DataModel.Treatment(db.Session) { };
         else if (!TryGetModel(Id, out model)) return;
         var b = Id != 0 && model.Name != Name;
         model.Name = Name;

         db.Session.CommitChanges();
         Id = model.Oid;
         if (b) UpdateTreatmentsStr(Id);
      }

      public static Treatment Create() => new Treatment();

      public static Treatment Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Treatment(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.Treatment model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Treatment>(id);
         if (model == null || model.Company != db.CompanyId) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.Treatment>()
         where el.Company == ApplicationDbContext.Current.CompanyId
         orderby el.Name ascending
         select new { el.Oid, el.Name };

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.Medical>()
                  from d in el.Treatments
                  where d.Oid == id
                  select d).Count();
         return c == 0;
      }

      /// <summary>
      /// Reupdate TreatmentsStr in Medical due to change treatment's name
      /// </summary>
      public static void UpdateTreatmentsStr(int treatmentId)
      {
         var db = ApplicationDbContext.Current;
         var lst = new XPCollection<DataModel.Medical>(db.Session, CriteriaOperator.Parse("Treatments[Oid=?]", treatmentId));
         foreach (var it in lst)
         {
            it.IsAudit = false;
            var names = it.Treatments.Select(el => el.Name).ToList();
            it.TreatmentsStr = String.Join("; ", names);
         }
         db.Session.CommitChanges();
      }
   }
}