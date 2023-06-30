using System.Collections.Specialized;
using WebApp.Core;
using System.Linq;
using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Web.Mvc;

namespace WebModule.MCNV.Models
{
   public class FacilitiesParameter : IReportParameter
   {
      public int FacilitiesType { get; set; }
      public int? Facility { get; set; }
      public int? Province { get; set; }
      public int? Area { get; set; }

      public string FacilitiesName { get; set; }

      public List<int> Ids { get; }
      public FacilitiesParameter()
      {
         Ids = new List<int>();
         var f = ApplicationDbContext.Current.Session.QueryFacility().ToList();
         FacilitiesName = Language.T("Tất cả");
         if (f.Count == 1)
         {
            FacilitiesType = 1;
            FacilitiesName = f[0].Name;
            Facility = f[0].Oid;
            Ids.Add(f[0].Oid);
         }
      }

      public void GetValue(NameValueCollection form)
      {
         if (HasMulti)
         {
            FacilitiesName = EditorExtension.GetValue<string>("FacilitiesName");
            FacilitiesType = Convert.ToInt32(form["FacilitiesType"]);
            Facility = EditorExtension.GetValue<int?>("Facility");
            Province = EditorExtension.GetValue<int?>("Province");
            Area = EditorExtension.GetValue<int?>("Area");
         }
      }

      public void SetParameter(ReportInfo report)
      {
         if (HasMulti)
         {
            var f = ApplicationDbContext.Current.Session.QueryFacility();
            Ids.Clear();
            switch (FacilitiesType)
            {
               case 0: //all
                  Ids.AddRange(f.Select(it => it.Oid));
                  break;
               case 1: //facility
                  if (Facility.HasValue)
                     Ids.Add(Facility.Value);
                  break;
               case 2: //province
                  if (Province.HasValue)
                     Ids.AddRange(f.Where(it => it.Province.Oid == Province.Value).Select(it => it.Oid));
                  break;
               case 3: //area
                  if (Area.HasValue)
                     Ids.AddRange(f.Where(it => it.Area.Oid == Area.Value).Select(it => it.Oid));
                  break;
            }
         }
         report.SetParameter("FacilitiesName", FacilitiesName);
         report.SetParameter("Facilities", Ids);
      }

      private Session session => ApplicationDbContext.Current.Session;
      public bool HasMulti => session.QueryFacility().Count() > 1;
      public object ListFacility() => session.QueryFacility().OrderBy(it => it.Name).Select(it => new { it.Oid, it.Name, it.FullAddress });
      public object ListProvince() => (from f in session.QueryFacility()
                                       group f by new { f.Province.Oid, f.Province.Name } into g
                                       orderby g.Key.Name
                                       select new { g.Key.Oid, g.Key.Name }).ToList().Select(it => new { it.Oid, it.Name, Code = it.Oid.ToString("00") });
      public object ListArea() => from f in session.QueryFacility()
                                  group f by new { f.Area.Oid, f.Area.Code, f.Area.Name } into g
                                  orderby g.Key.Name
                                  select new { g.Key.Oid, g.Key.Code, g.Key.Name };
   }
}