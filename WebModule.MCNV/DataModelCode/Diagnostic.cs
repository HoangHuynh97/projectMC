using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using WebApp.Core;

namespace WebModule.MCNV.DataModel
{
   [Audit("Chẩn đoán", "Name")]
    public partial class Diagnostic
    {
        public Diagnostic(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
