using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class Qualification
   {
      public static bool TryGetModel(int id, out DataModel.Qualification model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Qualification>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.Qualification>()
         orderby el.Index
         select new { el.Oid, el.Name };
   }
}