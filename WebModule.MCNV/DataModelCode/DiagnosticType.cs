using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Nhóm bệnh", nameof(Name))]
   public partial class DiagnosticType
   {
      public DiagnosticType(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
