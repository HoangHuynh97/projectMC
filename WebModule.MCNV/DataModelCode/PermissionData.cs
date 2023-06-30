using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using WebApp.Core;
namespace WebModule.MCNV.DataModel
{
   [Audit("Phân quyền dữ liệu", nameof(Name))]
   public partial class PermissionData
   {
      public PermissionData(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
