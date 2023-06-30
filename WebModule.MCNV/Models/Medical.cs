using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebModule.MCNV.Models
{
   public class Medical
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public bool AdvancedFilter { get; set; }
         public IQueryable<DataModel.Medical> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryMedical();
         }
      }

      public static string Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return "Không có quyền xóa dữ liệu";
         if (LockedData.CheckLocked(model.DateCreate.Date)) return "Dữ liệu đã bị khóa";
         model.Session.Delete(model.EvaluationAttachments);
         model.Session.Delete(model.CompletedAttachments);
         foreach (var p in model.Processes)
            model.Session.Delete(p.Attachments);
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
         return null;
      }

      public static bool TryGetModel(int id, out DataModel.Medical model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchMedical(id);
         if (model == null) return false;
         return true;
      }

      public static bool CheckCode(string code, int id)
      {
         if (string.IsNullOrEmpty(code)) return true;
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.Medical>()
                  where el.Company == db.CompanyId && el.Oid != id && el.Code == code
                  select el).Count();
         return c == 0;
      }

      public static bool CheckPatientData(int id)
      {
         return Patient.TryGetModel(id, out var _);
      }
   }
}