using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace WebModule.MCNV.DataModel
{
   [WebApp.Core.Audit("Bệnh nhân", nameof(Name))]
   public partial class Patient
   {
      public Patient(Session session) : base(session) { }

      [Persistent("FullAddress")]
      private string fullAddress;
      [PersistentAlias(nameof(fullAddress))]
      public string FullAddress => fullAddress;

      public override void AfterConstruction() {
         var db = WebApp.Core.ApplicationDbContext.Current;
         Facility = db.Session.GetObjectByKey<Facility>(db.UserModel.GroupId);
         base.AfterConstruction(); 
      }
      protected override void OnSaving()
      {
         var lst = new List<string>();
         if (!string.IsNullOrEmpty(Address))
            lst.Add(Address);
         if (Ward != null) lst.Add(Ward.Name);
         if (District != null) lst.Add(District.Name);
         if (Province != null) lst.Add(Province.Name);
         fullAddress = string.Join(", ", lst);

         Age = DateCreate.Year - DateBirth.Year + 1;

         Area = Models.Area.GetArea(Province);

         base.OnSaving();
      }

      public int AgeGroup
      {
         get
         {
            if (Age < 18) return 1;
            return 2;
         }
      }
   }
}
