using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class MedicalCancel
   {
      public static bool TryGetModel(int id, out DataModel.MedicalCancel model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.MedicalCancel>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.MedicalCancel>()
         orderby el.OrderIndex, el.Name
         select new { el.Oid, el.Name };
   }
}