using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{

    public partial class ProcessStatus
    {
        public ProcessStatus(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
