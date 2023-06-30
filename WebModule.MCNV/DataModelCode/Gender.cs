using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Giới tính", nameof(Name))]
   public partial class Gender
   {
      public Gender(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
