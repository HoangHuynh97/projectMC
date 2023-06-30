using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Web.Mvc;
using System.Collections.Specialized;

namespace WebApp.Models
{
   public class DenNgayParameter : IReportParameter
   {
      [Required]
      public DateTime DenNgay { get; set; }

      public DenNgayParameter()
      {
         DenNgay = DateTime.Today;
      }

      public void GetValue(NameValueCollection form)
      {
         DenNgay = EditorExtension.GetValue<DateTime>("DenNgay");
      }

      public void SetParameter(ReportInfo report)
      {
         report.SetParameter("DenNgay", DenNgay);
      }
   }
}