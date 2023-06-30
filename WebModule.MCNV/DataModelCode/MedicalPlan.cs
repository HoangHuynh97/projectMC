using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraPrinting.Native;

namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Kế hoạch can thiệp", "Medical.Patient.Code")]
   public partial class MedicalPlan
   {
      public MedicalPlan(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
