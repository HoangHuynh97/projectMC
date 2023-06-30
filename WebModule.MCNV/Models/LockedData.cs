using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public enum LockedOption
   {
      None = 1,
      ToDate = 2,
      DaySpan = 3,
   }

   public class LockedData
   {
      public LockedOption Option { get; set; }
      [Required(OnlyIf = "CheckOptionDate")]
      public DateTime? OptionDate { get; set; }
      [Required(OnlyIf = "CheckOptionSpan")]
      public int? OptionSpan { get; set; }

      public LockedData() { }

      public LockedData(DataModel.LockedData model)
      {
         Option = model.Option;
         OptionDate = model.OptionDate;
         OptionSpan = model.OptionSpan;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.LockedData>(CriteriaOperator.Parse("Company=?", db.CompanyId));
         if (model == null) model = new DataModel.LockedData(db.Session);
         model.Option = Option;
         if (model.Option == LockedOption.ToDate)
            model.OptionDate = OptionDate;
         else
            model.OptionDate = null;
         if (model.Option == LockedOption.DaySpan)
            model.OptionSpan = OptionSpan;
         else
            model.OptionSpan = null;
         db.Session.CommitChanges();
      }

      public bool CheckOptionDate() => Option == LockedOption.ToDate;
      public bool CheckOptionSpan() => Option == LockedOption.DaySpan;

      public static LockedData Load()
      {
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.LockedData>(CriteriaOperator.Parse("Company=?", db.CompanyId));
         if (model != null) return new LockedData(model);
         return new LockedData()
         {
            Option = LockedOption.None,
         };
      }

      public static bool CheckLocked(DateTime date)
      {
         var db = ApplicationDbContext.Current;
         //admin
         if (db.IsInRole(WebApp.Constant.Role.Admin)) return false;
         var vm = Load();
         //not use feature
         if (vm.Option == LockedOption.None) return false;
         //permission ignore locked
         if (PermissionData.TryGetModel(db.UserModel.DataId.Value, out var pm) && pm.IgnoreLocked) return false;
         //locked by date
         if (vm.Option == LockedOption.ToDate && date.Date <= vm.OptionDate.Value) return true;
         //locked by day
         if (vm.Option == LockedOption.DaySpan && date.Date <= DateTime.Today.AddDays(-vm.OptionSpan.Value)) return true;

         return false;
      }
   }
}