using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Dự án", nameof(Name))]
   public partial class ProgramProject
   {
      public ProgramProject(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
