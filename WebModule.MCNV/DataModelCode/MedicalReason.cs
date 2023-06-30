using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace WebModule.MCNV.DataModel
{

    public partial class MedicalReason
    {
        public MedicalReason(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
