using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Gói can thiệp", nameof(Name))]
   public partial class Treatment
   {
      public Treatment(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
