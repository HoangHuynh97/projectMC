using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class MedicalReason
   {
      public static bool TryGetModel(int id, out DataModel.MedicalReason model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchByCompany<DataModel.MedicalReason>(id);
         if (model == null) return false;
         return true;
      }

      public static DataModel.MedicalReason GetModelByName(string name)
      {
         DataModel.MedicalReason model = null;
         if (!string.IsNullOrEmpty(name))
         {
            var db = ApplicationDbContext.Current;
            model = db.Session.FindObject<DataModel.MedicalReason>(CriteriaOperator.Parse("Name==? && Company==?", name, db.CompanyId))
               ?? new DataModel.MedicalReason(db.Session) { Name = name };
         }
         return model;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryByCompany<DataModel.MedicalReason>()
         orderby el.Name
         select new { el.Oid, el.Name };
   }
}