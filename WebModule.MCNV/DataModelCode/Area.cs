using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Vùng dự án", nameof(Name))]
   public partial class Area
   {
      public Area(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
