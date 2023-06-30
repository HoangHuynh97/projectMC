using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{

    public partial class CompanyObject
    {
        public CompanyObject(Session session) : base(session) { }
        public override void AfterConstruction() {
         Company = WebApp.Core.ApplicationDbContext.Current.CompanyId.Value;
         base.AfterConstruction(); }
    }

}
