using DevExpress.Data.Filtering;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Core
{
   public interface IReportParameter
   {
      void SetParameter(ReportInfo report);
      void GetValue(NameValueCollection form);
   }

   public class ReportInfo
   {
      public int Id { get; private set; }
      public string Name { get; private set; }
      public XtraReport Report { get; private set; }

      public ReportInfo(string name)
      {
         //get report from database
         var session = ApplicationDbContext.Current.Session;
         //custom by company
         var model = session.FindObject<DataModel.ReportLayout>(CriteriaOperator.Parse("Name=? && Company=?", name, ApplicationDbContext.Current.CompanyId.Value));
         //or get default
         if (model == null)
            model = session.FindObject<DataModel.ReportLayout>(CriteriaOperator.Parse("Name=?", name));
         if (model == null)
         {
            model = new DataModel.ReportLayout(session)
            {
               Name = name,
               Active = true
            };
            var r = new XtraReport();
            using (MemoryStream ms = new MemoryStream())
            {
               r.SaveLayoutToXml(ms);
               model.Layout = ms.GetBuffer();
            }
            session.CommitChanges();
         }
         //return object
         Report = new XtraReport();
         if (model != null)
         {
            Id = model.Oid;
            Name = model.Name;
            if (model.Layout != null)
               using (var stream = new MemoryStream(model.Layout))
               {
                  Report.LoadLayoutFromXml(stream);
                  //To force the LoadConnection method call,
                  if (Report.DataSource is DevExpress.DataAccess.Sql.SqlDataSource ds)
                     ds.ConnectionParameters = null;
               }
         }
         //set default parameter
         SetParameter("Company", ApplicationDbContext.Current.CompanyId.Value);
         SetParameter("User", ApplicationDbContext.Current.UserId.Value);
         SetParameter("UserName", ApplicationDbContext.Current.UserModel.FullName);
         if (DefaultParameters != null)
         {
            var pms = DefaultParameters();
            foreach (var it in pms)
               SetParameter(it.Key, it.Value);
         }
      }

      public void SetParameter(string key, object value)
      {
         if (Report != null && Report.Parameters[key] != null)
         {
            Report.Parameters[key].Value = value;
            Report.Parameters[key].Visible = false;
         }
      }

      private static List<Type> _parameterTypes;
      public static List<Type> ParameterTypes
      {
         get
         {
            if (_parameterTypes == null)
            {
               var p = typeof(IReportParameter);
               var asm = from a in AppDomain.CurrentDomain.GetAssemblies()
                         where a.ManifestModule.Name == "WebApp.dll" || a.ManifestModule.Name.StartsWith("WebModule.")
                         select a;
               _parameterTypes = asm.SelectMany(a => a.GetTypes()).Where(t => p.IsAssignableFrom(t) && t.IsClass).OrderBy(t => t.Name).ToList();
            }
            return _parameterTypes;
         }
      }

      public static Func<Dictionary<string, object>> DefaultParameters;
   }
}