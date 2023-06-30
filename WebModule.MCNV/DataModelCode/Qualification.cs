using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Chuyên môn", nameof(Name))]
   public partial class Qualification
   {
      public Qualification(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
