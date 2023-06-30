using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class AuditLog
   {
      public DateTime TuNgay { get; set; }
      public DateTime DenNgay { get; set; }
      public bool ShowFilter { get; set; }

      public AuditLog()
      {
         TuNgay = DenNgay = DateTime.Today;
      }

      public List<Core.Audit.AuditInfo> GetData()
      {
         return Core.Audit.GetAudits(TuNgay, DenNgay);
      }

      public static List<Core.Audit.AuditDataInfo> GetDetails(int id)
      {
         return Core.Audit.GetAuditData(id);
      }
   }
}