using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Dịch vụ PHCN", nameof(Name))]
   public partial class Service
   {
      public Service(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
