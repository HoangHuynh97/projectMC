using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebApp.DataModel
{

   public partial class Permission
   {
      public Permission(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
