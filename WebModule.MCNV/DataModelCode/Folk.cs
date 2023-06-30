using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Dân tộc", nameof(Name))]
   public partial class Folk
   {
      public Folk(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
