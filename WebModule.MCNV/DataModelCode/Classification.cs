using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Đối tượng khuyết tật", nameof(Name))]
   public partial class Classification
   {
      public Classification(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
