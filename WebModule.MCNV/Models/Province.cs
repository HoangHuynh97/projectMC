using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class Province
   {
      public static bool TryGetModel(int id, out DataModel.Province model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Province>(id);
         if (model == null) return false;
         return true;
      }

      public static object ComboboxData(List<int> values = null) =>
         (from el in ApplicationDbContext.Current.Session.Query<DataModel.Province>()
          select new { el.Oid, el.Name })
         .OrderBy(it => (values != null && values.Contains(it.Oid) ? "0" : "1") + it.Name).ToList()
         .Select(it => new { it.Oid, Code = it.Oid.ToString("00"), it.Name });
   }
}