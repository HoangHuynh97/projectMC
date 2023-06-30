using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Đơn vị đào tạo", nameof(Name))]
   public partial class Institution
   {
      public Institution(Session session) : base(session) { }
      public override void AfterConstruction() { base.AfterConstruction(); }

      [Persistent("FullAddress")]
      private string fullAddress;
      [PersistentAlias(nameof(fullAddress))]
      public string FullAddress => fullAddress;

      protected override void OnSaving()
      {
         var lst = new List<string>();
         if (!string.IsNullOrEmpty(Address))
            lst.Add(Address);
         if (Ward != null) lst.Add(Ward.Name);
         if (District != null) lst.Add(District.Name);
         if (Province != null) lst.Add(Province.Name);
         fullAddress = string.Join(", ", lst);

         base.OnSaving();
      }
   }
}
