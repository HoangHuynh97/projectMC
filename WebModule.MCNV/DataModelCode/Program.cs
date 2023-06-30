using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Khóa đào tạo", "Name")]
   public partial class Program
   {
      public Program(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
