using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebModule.MCNV.Models
{
   public class Area
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Area> GetData()
         {
            var db = ApplicationDbContext.Current;
            var query = from el in db.Session.Query<DataModel.Area>()
                        where el.Company == db.CompanyId.Value
                        select el;
            return query;
         }
      }

      public int Id { get; set; }
      [Caption("Mã")]
      public string Code { get; set; }
      [Caption("Tên"), Required]
      public string Name { get; set; }
      public List<int> Provinces { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }

      public Area()
      {
         Provinces = new List<int>();
      }
      public Area(DataModel.Area model)
      {
         Id = model.Oid;
         Code = model.Code;
         Name = model.Name;
         Provinces = model.Provinces.Select(it => it.Oid).ToList();
         Note = model.Note;
      }
      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Area model;
         if (Id == 0)
            model = new DataModel.Area(db.Session) ;
         else if (!TryGetModel(Id, out model)) return;
         model.Code = Code;
         model.Name = Name;
         model.Note = Note;
         //remove province
         var dels = model.Provinces.Where(it => !Provinces.Contains(it.Oid)).ToList();
         foreach (var it in dels)
            model.Provinces.Remove(it);
         //add province
         var adds = Provinces.Where(id => model.Provinces.All(it => it.Oid != id)).ToList();
         model.Provinces.AddRange(db.Session.GetObjectsByKey(db.Session.GetClassInfo<DataModel.Province>(), adds, true).Cast<DataModel.Province>());

         db.Session.CommitChanges();
      }

      public static Area Create() => new Area();

      public static Area Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Area(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.Area model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Area>(id);
         if (model == null || model.Company != db.CompanyId.Value) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.Area>()
         where el.Company == ApplicationDbContext.Current.CompanyId.Value
         select new { el.Oid, el.Code, el.Name };

      public List<DataModel.Province> EditProvinces()
      {
         var db = ApplicationDbContext.Current;
         var session = db.Session;
         var exists = from el in session.Query<DataModel.Area>()
                      where el.Company == db.CompanyId
                      from p in el.Provinces
                      select p.Oid;
         var query = from el in session.Query<DataModel.Province>()
                     where Provinces.Contains(el.Oid) || !exists.Contains(el.Oid)
                     select el;
         return query.OrderBy(it => (Provinces.Contains(it.Oid) ? "0" : "1") + it.Name).ToList();
      }

      /// <summary>
      /// Get area contains province
      /// </summary>
      public static DataModel.Area GetArea(DataModel.Province province)
      {
         var db = ApplicationDbContext.Current;
         var query = from el in db.Session.Query<DataModel.Area>()
                     where el.Company == db.CompanyId && el.Provinces.Contains(province)
                     select el;
         return query.FirstOrDefault();
      }

      public static bool CanDelete(int id, out string error)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.Medical>()
                  where el.Area.Oid == id
                  select el).Count();
         if (c > 0)
         {
            error = "Hồ sơ PHCN";
            return false;
         }

         c = (from el in db.Session.Query<DataModel.Patient>()
              where el.Area.Oid == id
              select el).Count();
         if (c > 0)
         {
            error = "Thông tin NKT";
            return false;
         }

         c = (from el in db.Session.Query<DataModel.Facility>()
              where el.Area.Oid == id
              select el).Count();
         if (c > 0)
         {
            error = "Đơn vị cung cấp dịch vụ";
            return false;
         }

         error = "";
         return true;
      }
   }
}