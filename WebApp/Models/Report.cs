using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraReports.UI;
using System.IO;

namespace WebApp.Models
{
   public class Report
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.ReportLayout> GetData()
         {
            return Core.ApplicationDbContext.Current.Session.Query<DataModel.ReportLayout>();
         }
      }

      public int Id { get; set; }
      [Required, Caption("Tên báo cáo")]
      public string Name { get; set; }
      [Caption("Nhóm báo cáo")]
      public string Category { get; set; }
      [Caption("Đang sử dụng")]
      public bool Active { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public List<string> Parameters { get; set; }
      public int? Company { get; set; }

      public Report() { Parameters = new List<string>(); }
      public Report(DataModel.ReportLayout model)
      {
         Id = model.Oid;
         Name = model.Name;
         Category = model.Category;
         Active = model.Active;
         Note = model.Note;
         if (string.IsNullOrEmpty(model.Parameters))
            Parameters = new List<string>();
         else
            Parameters = model.Parameters.Split(',').ToList();
         Company = model.Company?.Oid;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.ReportLayout model;
         if (Id == 0)
         {
            model = new DataModel.ReportLayout(db.Session);
            var r = new XtraReport();
            using (MemoryStream ms = new MemoryStream())
            {
               r.SaveLayoutToXml(ms);
               model.Layout = ms.GetBuffer();
            }
         }
         else
            model = db.Session.GetObjectByKey<DataModel.ReportLayout>(Id);
         if (model == null) return;
         model.Name = Name;
         model.Category = Category;
         model.Active = Active;
         model.Note = Note;
         model.Parameters = string.Join(",", Parameters);
         if (Company.HasValue)
            model.Company = db.Session.GetObjectByKey<DataModel.Company>(Company.Value);
         else
            model.Company = null;
         db.Session.CommitChanges();
      }

      public static Report Create()
      {
         return new Report() { Active = true };
      }

      public static Report Load(int id)
      {
         var model = ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.ReportLayout>(id);
         if (model == null) return null;
         return new Report(model);
      }

      public static void Delete(int id)
      {
         var model = ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.ReportLayout>(id);
         if (model == null) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static List<string> GetCategories()
      {
         var session = ApplicationDbContext.Current.Session;
         var query = from r in session.Query<DataModel.ReportLayout>()
                     where r.Category != null && r.Category != ""
                     orderby r.Category
                     select r.Category;
         return query.Distinct().ToList();
      }

      public static List<KeyValuePair<string, string>> GetParameters()
      {
         return ReportInfo.ParameterTypes.Select(p => new KeyValuePair<string, string>(p.FullName, p.Name)).ToList();
      }

      public static object  GetCompanies()
      {
         var session = ApplicationDbContext.Current.Session;
         var query = from c in session.Query<DataModel.Company>()
                     orderby c.Name
                     select new { c.Oid, c.Name };
         return query.ToList();
      }
   }
}