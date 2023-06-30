using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Cán bộ y tế", nameof(Name))]
   public partial class Doctor
   {
      public Doctor(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
