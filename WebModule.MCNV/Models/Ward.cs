using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class Ward
   {
      public static bool TryGetModel(int id, out DataModel.Ward model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Ward>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxForValue(int? ward)
      {
         if (!ward.HasValue) return null;
         var db = ApplicationDbContext.Current;
         var model = db.Session.GetObjectByKey<DataModel.Ward>(ward);
         var query = from el in db.Session.Query<DataModel.Ward>()
                     where el.District.Oid == model.District.Oid
                     orderby el.Name ascending
                     select new { el.Oid, el.Name };
         return query;
      }

      public static IQueryable ComboboxForDistrict(int? district)
      {
         if (!district.HasValue) return null;
         var db = ApplicationDbContext.Current;
         return from el in db.Session.Query<DataModel.Ward>()
                where el.District.Oid == district
                orderby el.Name ascending
                select new { el.Oid, el.Name };
      }
   }
}