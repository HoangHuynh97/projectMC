using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Hồ sơ PHCN", nameof(NameAudit))]
   public partial class Medical
   {
      public Medical(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }

      protected override void OnSaving()
      {
         Area = Facility.Area;

         base.OnSaving();
      }

      private DateTimeOffset? fDateEvaluation;
      [ValueConverter(typeof(WebApp.Core.DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset? DateEvaluation
      {
         get { return fDateEvaluation; }
         set { SetPropertyValue("DateEvaluation", ref fDateEvaluation, value); }
      }

      private DateTimeOffset? fDateEnd;
      [ValueConverter(typeof(WebApp.Core.DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset? DateEnd
      {
         get { return fDateEnd; }
         set { SetPropertyValue("DateEnd", ref fDateEnd, value); }
      }

      public string NameAudit => string.Join(", ", (new string[] { Code, Patient.Code + " " + Patient.Name }));
   }

}
