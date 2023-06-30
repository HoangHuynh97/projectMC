using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class ProcessStatus
   {
      public static bool TryGetModel(int id, out DataModel.ProcessStatus model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.ProcessStatus>(id);
         if (model == null) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.ProcessStatus>()
         orderby el.OrderIndex, el.Name
         select new { el.Oid, el.Name };
   }
}