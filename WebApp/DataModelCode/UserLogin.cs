using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebApp.DataModel
{

   public partial class UserLogin
   {
      public UserLogin(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }
   }

}
