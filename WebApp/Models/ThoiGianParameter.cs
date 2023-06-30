using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Web.Mvc;

namespace WebApp.Models
{
   public class ThoiGianParameter : IReportParameter
   {
      [Required]
      public string ThoiGian { get; set; }
      [Required]
      public DateTime TuNgay { get; set; }
      [Required]
      public DateTime DenNgay { get; set; }

      public ThoiGianParameter()
      {
         DenNgay = DateTime.Today;
         TuNgay = new DateTime(DenNgay.Year, DenNgay.Month, 1);
      }

      public void GetValue(NameValueCollection form)
      {
         ThoiGian = EditorExtension.GetValue<string>("ThoiGian");
         TuNgay = Convert.ToDateTime(form["TuNgay"]);
         DenNgay = Convert.ToDateTime(form["DenNgay"]);
      }
      
      public void SetParameter(ReportInfo report)
      {
         report.SetParameter("ThoiGian", ThoiGian);
         report.SetParameter("TuNgay", TuNgay);
         report.SetParameter("DenNgay", DenNgay);
      }
   }
}