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

    /// <summary>
    /// Phường xã
    /// </summary>
    public partial class Ward : XPLiteObject
    {
        int fOid;
        [Key]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
        }
        District fDistrict;
        [Association(@"WardsReferencesDistrict")]
        public District District
        {
            get { return fDistrict; }
            set { SetPropertyValue<District>(nameof(District), ref fDistrict, value); }
        }
    }

}
