﻿using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Chức danh", nameof(Name))]
   public partial class DoctorTitle
   {
      public DoctorTitle(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
