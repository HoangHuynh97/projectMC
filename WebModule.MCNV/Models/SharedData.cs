using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class SharedData
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.SharedData> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QuerySharedData();
         }
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static void UpdateAccept(int id, bool accept)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Accept = accept;
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.SharedData model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchSharedData(id);
         if (model == null) return false;
         return true;
      }

      public static void AddRequest(int patientId)
      {
         if (HasSentRequest(patientId)) return;
         var db = ApplicationDbContext.Current;
         var model = new DataModel.SharedData(db.Session)
         {
            UserRequest = db.UserModel,
            FacilityRequest = db.Session.GetObjectByKey<DataModel.Facility>(db.UserModel.GroupId),
            Patient = db.Session.GetObjectByKey<DataModel.Patient>(patientId),
         };
         model.FacilityReceive = model.Patient.Facility;
         db.Session.CommitChanges();
      }

      public static bool HasSentRequest(int patientId)
      {
         var db = ApplicationDbContext.Current;
         var query = from el in db.Session.QueryByCompany<DataModel.SharedData>()
                     where el.FacilityRequest.Oid == db.UserModel.GroupId && el.Patient.Oid == patientId
                        && el.FacilityReceive.Oid == el.Patient.Facility.Oid
                     select el;
         return query.Any();
      }
   }
}