using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Mức độ khuyết tật", nameof(Name))]
   public partial class LevelDisability
   {
      public LevelDisability(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
