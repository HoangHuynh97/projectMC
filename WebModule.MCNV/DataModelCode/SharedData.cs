using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Duyệt hồ sơ chuyển vùng", "Patient.Name")]
   public partial class SharedData
   {
      public SharedData(Session session) : base(session) { }
      public override void AfterConstruction()
      {
         DateRequest = DateTime.Now;
         base.AfterConstruction();
      }

      private DateTimeOffset fDateRequest;
      [ValueConverter(typeof(WebApp.Core.DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset DateRequest
      {
         get { return fDateRequest; }
         set { SetPropertyValue(nameof(DateRequest), ref fDateRequest, value); }
      }

      [PersistentAlias(nameof(DateRequest))]
      public DateTime LocalDateRequest => DateRequest.LocalDateTime;
   }

}
