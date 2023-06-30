using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Phân loại nạn nhân da cam", nameof(Name))]

   public partial class OrangeVictim
   {
      public OrangeVictim(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
