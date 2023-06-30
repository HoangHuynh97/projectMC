using DevExpress.Web.Mvc;
using System;
using System.Collections.Specialized;
using WebApp.Core;

namespace WebApp.Models
{
   public class PeriodParameter : IReportParameter
   {
      [Required]
      public string ThoiGian { get; set; }
      [Required]
      public DateTime TuNgay { get; set; }
      [Required]
      public DateTime DenNgay { get; set; }

      public PeriodParameter()
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