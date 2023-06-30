using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class MedicalStatus
   {
      public static bool TryGetModel(int id, out DataModel.MedicalStatus model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.MedicalStatus>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.MedicalStatus>()
         select new { el.Oid, el.Name };

      public static int NotStarted = 1;
      public static int Processing = 2;
      public static int Completed = 3;
      public static int Canceled = 4;
      public static int NotProcess = 5;
   }
}