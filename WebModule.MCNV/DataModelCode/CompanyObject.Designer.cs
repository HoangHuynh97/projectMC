﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace WebModule.MCNV.DataModel
{

    [NonPersistent]
    public partial class CompanyObject : WebApp.DataModel.BaseObject
    {
        int fCompany;
        public int Company
        {
            get { return fCompany; }
            set { SetPropertyValue<int>(nameof(Company), ref fCompany, value); }
        }
    }

}
