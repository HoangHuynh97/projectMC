using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using WebApp.Core;

namespace WebApp.DataModel
{

   public partial class ReportLayout
   {
      public ReportLayout(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }

      private DateTimeOffset fLastUpdate;
      [ValueConverter(typeof(DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset LastUpdate
      {
         get { return fLastUpdate; }
         set { SetPropertyValue("LastUpdate", ref fLastUpdate, value); }
      }

      protected override void OnSaving()
      {
         fLastUpdate = DateTimeOffset.Now;
         base.OnSaving();
      }
   }

}
