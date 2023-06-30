using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class DiagnosticType
   {
      public static bool TryGetModel(int id, out DataModel.DiagnosticType model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.DiagnosticType>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.DiagnosticType>()
         select new { el.Oid, el.Name };
   }
}