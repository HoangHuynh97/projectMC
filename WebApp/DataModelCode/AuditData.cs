using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebApp.DataModel
{

   public partial class AuditData
   {
      public AuditData(Session session) : base(session) { }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         AuditDate = DateTimeOffset.Now;
      }

      private DateTimeOffset fAuditDate;
      [ValueConverter(typeof(Core.DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset AuditDate
      {
         get { return fAuditDate; }
         set { SetPropertyValue("AuditDate", ref fAuditDate, value); }
      }
   }
}
