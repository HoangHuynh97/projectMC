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
   public class PermissionModel
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Permission> GetData()
         {
            return ApplicationDbContext.Current.Session.Query<DataModel.Permission>();
         }
      }

      public int Id { get; set; }
      [Required, Caption("Tên quyền sử dụng")]
      public string Name { get; set; }
      [Caption("Phân quyền cho người dùng hệ thống")]
      public bool System { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public int? Company { get; private set; }
      public List<string> Permissions { get; private set; }
      public string PermissionStr { get; set; }
      public List<int> Reports { get; private set; }
      public string ReportStr { get; set; }

      public PermissionModel()
      {
         Permissions = new List<string>();
         Reports = new List<int>();
      }

      public PermissionModel(DataModel.Permission model)
      {
         Id = model.Oid;
         Name = model.Name;
         Note = model.Note;
         System = model.System;
         Company = model.Company;
         Permissions = model.Details.Select(p=>p.Module + "/" + p.Function + "/" + p.Logics).ToList();
         Reports = model.Reports.Select(p => p.Oid).ToList();
      }

      public void Save()
      {
         var session = ApplicationDbContext.Current.Session;
         DataModel.Permission model;
         if (Id == 0)
            model = new DataModel.Permission(session);
         else
            model = session.GetObjectByKey<DataModel.Permission>(Id);
         if (model == null) return;
         model.Name = Name;
         if (model.Company == null)
            model.System = System;
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

      public static PermissionModel Create()
      {
         return new PermissionModel();
      }

      public static PermissionModel Load(int id)
      {
         var model = ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.Permission>(id);
         if (model == null)
            return null;
         return new PermissionModel(model);
      }

      public static void Delete(int id)
      {
         var session = ApplicationDbContext.Current.Session;
         var model = session.GetObjectByKey<DataModel.Permission>(id);
         if (model == null)
            return;
         model.Delete();
         session.CommitChanges();
      }
   }
}