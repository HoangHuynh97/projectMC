using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebApp.Models
{
   public class UserIdParameter : IReportParameter
   {
      public int UserId { get; set; }

      public void GetValue(NameValueCollection form)
      {
         UserId = EditorExtension.GetValue<int>("UserId");
      }

      public void SetParameter(ReportInfo report)
      {
         report.SetParameter("UserId", UserId);
      }

      public List<KeyValuePair<int, string>> GetList()
      {
         var result = new List<KeyValuePair<int, string>>
         {
            new KeyValuePair<int, string>(0, Language.T("Tất cả"))
         };
         result.AddRange(ApplicationDbContext.Current.CompanyModel.GetCompanyUsers().Select(p => new KeyValuePair<int, string>(p.Oid, p.FullName)).OrderBy(p => p.Value));
         return result;
      }
   }
}