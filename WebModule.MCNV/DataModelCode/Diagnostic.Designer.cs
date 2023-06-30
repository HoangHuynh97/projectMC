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

    public partial class Diagnostic : CompanyObject
    {
        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>(nameof(Code), ref fCode, value); }
        }
        string fName;
        [Size(300)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
        }
        DiagnosticType fDiagnosticType;
        [ExplicitLoading(1)]
        public DiagnosticType DiagnosticType
        {
            get { return fDiagnosticType; }
            set { SetPropertyValue<DiagnosticType>(nameof(DiagnosticType), ref fDiagnosticType, value); }
        }
        [Association(@"MedicalDiagnostics", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Medical> Medicals { get { return GetCollection<Medical>(nameof(Medicals)); } }
    }

}