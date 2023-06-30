using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public enum PermissionDataType
   {
      None = 0,
      Owner = 1,
      Facility = 2,
      Provinces = 3,
      Area = 4,
      Admin = 9,
   }

   public class PermissionData
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.PermissionData> GetData()
         {
            var db = ApplicationDbContext.Current;
            var query = from el in db.Session.Query<DataModel.PermissionData>()
                        where el.Company == db.CompanyId
                        select el;
            return query;
         }
      }

      public int Id { get; set; }
      [Caption("Tên quyền dữ liệu"), Required]
      public string Name { get; set; }
      public PermissionDataType DataType { get; set; }
      [Caption(""), Template("Provinces")]
      public List<int> Provinces { get; set; }
      [Caption(""), Template("Area")]
      public int? Area { get; set; }
      [Caption("Cho phép chỉnh sửa, xóa dữ liệu đã bị khóa")]
      public bool IgnoreLocked { get; set; }

      public PermissionData()
      {
         DataType = PermissionDataType.Owner;
         Provinces = new List<int>();
      }

      public PermissionData(DataModel.PermissionData model)
      {
         Id = model.Oid;
         Name = model.Name;
         DataType = model.DataType;
         Provinces = new List<int>();
         if (!string.IsNullOrEmpty(model.ParameterIds))
         {
            if (DataType == PermissionDataType.Provinces)
               Provinces.AddRange(model.ParameterIds.Split(';').Select(s => Convert.ToInt32(s)));
            else if (DataType == PermissionDataType.Area)
               Area = Convert.ToInt32(model.ParameterIds);
         }
         IgnoreLocked = model.IgnoreLocked;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.PermissionData model;
         if (Id == 0)
            model = new DataModel.PermissionData(db.Session) { };
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;
         model.DataType = DataType;
         if (model.DataType == PermissionDataType.Provinces)
            model.ParameterIds = string.Join(";", Provinces);
         else if (model.DataType == PermissionDataType.Area)
            model.ParameterIds = Area.ToString();
         else
            model.ParameterIds = null;
         model.IgnoreLocked = IgnoreLocked;
         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static PermissionData Create() => new PermissionData();

      public static PermissionData Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new PermissionData(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.PermissionData model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.PermissionData>(id);
         if (model == null || model.Company != db.CompanyId) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.PermissionData>()
         where el.Company == ApplicationDbContext.Current.CompanyId
         select new { el.Oid, el.Name };
   }
}