using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Microsoft.AspNet.Identity.Owin;

namespace WebApp.Models
{
   public class CompanyPermission
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Permission> GetData()
         {
            var db = ApplicationDbContext.Current;
            return from p in db.Session.Query<DataModel.Permission>()
                   where p.Company.Value == db.CompanyId.Value
                   select p;
         }
      }

      public int Id { get; set; }
      [Required, Caption("Tên quyền sử dụng")]
      public string Name { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public List<string> Permissions { get; private set; }
      public string PermissionStr { get; set; }
      public List<int> Reports { get; private set; }
      public string ReportStr { get; set; }

      public CompanyPermission()
      {
         Permissions = new List<string>();
         Reports = new List<int>();
      }

      public CompanyPermission(DataModel.Permission model)
      {
         Id = model.Oid;
         Name = model.Name;
         Note = model.Note;
         Permissions = model.Details.Select(p=>p.Module + "/" + p.Function + "/" + p.Logics).ToList();
         Reports = model.Reports.Select(p => p.Oid).ToList();
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         var session = db.Session;
         DataModel.Permission model;
         if (Id == 0)
            model = new DataModel.Permission(session) { Company = db.CompanyId.Value };
         else
            model = session.GetObjectByKey<DataModel.Permission>(Id);
         if (model == null || model.Company.Value != db.CompanyId.Value) return;
         model.Name = Name;
         model.Note = Note;
         //update detail permission
         var hasChanged = false;
         if (string.IsNullOrEmpty(PermissionStr))
         {
            var del = model.Details.ToList();
            if (del.Count > 0)
               hasChanged = true;
            session.Delete(del);
         }
         else
         {
            Permissions = PermissionStr.Split(';').ToList();
            var del = model.Details.Where(d => !Permissions.Any(p => p.StartsWith(d.Module + "/" + d.Function + "/"))).ToList();
            if (del.Count > 0)
               hasChanged = true;
            session.Delete(del);

            foreach (var pm in Permissions)
            {
               var arr = pm.Split('/');
               var detail = model.Details.FirstOrDefault(d => pm.StartsWith(d.Module + "/" + d.Function + "/"));
               if (detail == null)
               {
                  detail = new DataModel.PermisionDetail(session)
                  {
                     Permission = model,
                     Module = arr[0],
                     Function = arr[1]
                  };
                  model.Details.Add(detail);
               }
               if (detail.Logics != arr[2])
                  hasChanged = true;
               detail.Logics = arr[2];
            }
         }
         //update reports
         if (string.IsNullOrEmpty(ReportStr))
         {
            if (model.Reports.Count > 0)
               hasChanged = true;
            while (model.Reports.Count > 0)
               model.Reports.Remove(model.Reports[0]);
         }
         else
         {
            Reports = ReportStr.Split(';').Select(p => Convert.ToInt32(p)).ToList();
            var del = model.Reports.Where(d => !Reports.Contains(d.Oid)).ToList();
            if (del.Count > 0)
               hasChanged = true;
            foreach (var d in del)
               model.Reports.Remove(d);

            var add = Reports.Where(p => !model.Reports.Any(c => c.Oid == p)).ToList();
            if (add.Count > 0)
               hasChanged = true;
            foreach (var a in add)
               model.Reports.Add(model.Session.GetObjectByKey<DataModel.ReportLayout>(a));
         }
         session.CommitChanges();
         //force relogin for users in permission
         if (hasChanged)
         {
            var mn = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
            var users = new XPCollection<DataModel.User>(session, CriteriaOperator.Parse("Permission=?", model.Oid));
            foreach (var u in users)
               mn.UpdateSecurityStampAsync(u.Oid);
         }
      }

      public static CompanyPermission Create()
      {
         return new CompanyPermission();
      }

      public static CompanyPermission Load(int id)
      {
         var db = ApplicationDbContext.Current;
         var session = db.Session;
         var model = session.GetObjectByKey<DataModel.Permission>(id);
         if (model == null || model.Company.Value != db.CompanyId.Value)
            return null;
         return new CompanyPermission(model);
      }

      public static void Delete(int id)
      {
         var db = ApplicationDbContext.Current;
         var session = db.Session;
         var model = session.GetObjectByKey<DataModel.Permission>(id);
         if (model == null || model.Company.Value != db.CompanyId.Value)
            return;
         model.Delete();
         session.CommitChanges();
      }
   }
}