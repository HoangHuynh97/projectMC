using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Can thiệp điều trị", nameof(ProcessDate))]
   public partial class MedicalProcess
   {
      public MedicalProcess(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
