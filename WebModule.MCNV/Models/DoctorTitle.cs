using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class DoctorTitle
   {
      public static bool TryGetModel(int id, out DataModel.DoctorTitle model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.DoctorTitle>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.DoctorTitle>()
         orderby el.Index
         select new { el.Oid, el.Name };
   }
}