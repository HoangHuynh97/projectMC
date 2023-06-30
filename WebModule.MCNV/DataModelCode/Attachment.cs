using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Tập tin đính kèm", nameof(Name))]
   public partial class Attachment
   {
      public Attachment(Session session) : base(session) { }
      public override void AfterConstruction()
      {
         UniqueId = Guid.NewGuid().ToString();
         base.AfterConstruction();
      }

      protected override void OnDeleting()
      {
         //delete file
         var file = System.Web.Hosting.HostingEnvironment.MapPath(FileUrl);
         if (System.IO.File.Exists(file)) System.IO.File.Delete(file);

         base.OnDeleting();
      }
   }

}
