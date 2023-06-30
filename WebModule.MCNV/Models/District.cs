using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class District
   {
      public static bool TryGetModel(int id, out DataModel.District model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.District>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxForValue(int? district)
      {
         if (!district.HasValue) return null;
         var db = ApplicationDbContext.Current;
         var model = db.Session.GetObjectByKey<DataModel.District>(district);
         var query = from el in db.Session.Query<DataModel.District>()
                     where el.Province.Oid == model.Province.Oid
                     orderby el.Name ascending
                     select new { el.Oid, el.Name };
         return query;
      }

      public static IQueryable ComboboxForProvince(int? province)
      {
         if (!province.HasValue) return null;
         var db = ApplicationDbContext.Current;
         return from el in db.Session.Query<DataModel.District>()
                where el.Province.Oid == province
                orderby el.Name ascending
                select new { el.Oid, el.Name };
      }
   }
}