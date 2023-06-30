using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class Patient : AddressBase
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Patient> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryPatient();
         }
      }

      public class ViewInfo
      {
         [Caption("Họ và tên")]
         public string Name { get; }
         [Caption("Giới tính")]
         public string Gender { get; }
         [Caption("Dân tộc")]
         public string Folk { get; }
         [Caption("Số CMND")]
         public string CMND { get; }
         [Caption("Số CCCD")]
         public string CCCD { get; }
         [Caption("Số thẻ BHYT")]
         public string BHYT { get; }
         [Caption("Ngày sinh")]
         public string DateBirth { get; }
         [Caption("Điện thoại")]
         public string Phone { get; }
         [Caption("Địa chỉ")]
         public string Address { get; }
         [Caption("Đối tượng khuyết tật")]
         public string Classification { get; }
         [Caption("Mức độ khuyết tật")]
         public string LevelDisability { get; }
         [Caption("Nạn nhân da cam")]
         public string OrangeVictim { get; }
         [Caption("Ghi chú")]
         public string Note { get; }
         [Caption("Dạng khuyết tật")]
         public string Disabilities { get; set; }
         public bool ShowLevel { get; }
         public ViewInfo(DataModel.Patient model)
         {
            Name = model.Name;
            Gender = model.Gender.Name;
            Folk = model.Folk?.Name;
            DateBirth = model.DateBirth.ToShortDateString();
            Phone = model.Phone;
            CMND = model.CMND;
            CCCD = model.CCCD;
            BHYT = model.BHYT;
            Classification = model.Classification.Name;
            LevelDisability = model.LevelDisability?.Name;
            OrangeVictim = model.OrangeVictim.Name;
            Address = model.FullAddress;
            Note = model.Note;
            Disabilities = model.DisabilitiesStr;
            ShowLevel = model.Classification.Oid != 3;
         }

         public static ViewInfo Load(int id)
         {
            if (!TryGetModel(id, out var model)) return null;
            return new ViewInfo(model);
         }
      }

      public int Id { get; set; }
      [Caption("Họ và tên"), Required]
      public string Name { get; set; }
      [Caption("Giới tính"), Template("Gender"), Required]
      public int? Gender { get; set; }
      [Caption("Dân tộc"), Template("Folk"), Required]
      public int? Folk { get; set; } = 1;
      [Caption("Số CMND"), Template("CMND"), Regex("[0-9]{9}", "Không hợp lệ"), Remote("CheckCMND", "Patient", "Số CMND này đã nhập rồi", AdditionalFields = "Id")]
      public string CMND { get; set; }
      [Caption("Số CCCD"), Template("CCCD"), Regex("[0-9]{12}", "Không hợp lệ"), Remote("CheckCCCD", "Patient", "Số CCCD này đã nhập rồi", AdditionalFields = "Id")]
      public string CCCD { get; set; }
      [Caption("Số thẻ BHYT"), Template("BHYT"), Regex("[0-9]{10}", "Không hợp lệ"), Remote("CheckBHYT", "Patient", "Số thẻ BHYT này đã nhập rồi", AdditionalFields = "Id")]
      public string BHYT { get; set; }
      [Caption("Ngày sinh"), Required]
      public DateTime DateBirth { get; set; }
      [Caption("Điện thoại"), Template("Phone")]
      public string Phone { get; set; }
      [Caption("Đối tượng khuyết tật"), Template("Classification"), Required]
      public int? Classification { get; set; } = 1;

      [Caption("Mức độ khuyết tật"), Template("LevelDisability"), Required(OnlyIf = "CheckLevelDisability")]
      public int? LevelDisability { get; set; }
      [Caption("Nạn nhân da cam"), Template("OrangeVictim"), Required]
      public int? OrangeVictim { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      [Caption("Dạng khuyết tật"), Template("Disabilities"), Required]
      public string Disabilities { get; set; }
      public string DisabilitiesValue { get; set; }
      public bool Locked { get; }

      public Patient() { }
      public Patient(DataModel.Patient model)
      {
         Id = model.Oid;
         Name = model.Name;
         Gender = model.Gender.Oid;
         Folk = model.Folk?.Oid;
         DateBirth = model.DateBirth;
         Phone = model.Phone;
         CMND = model.CMND;
         CCCD = model.CCCD;
         BHYT = model.BHYT;
         Classification = model.Classification.Oid;
         if (Classification != 3)
            LevelDisability = model.LevelDisability?.Oid;
         OrangeVictim = model.OrangeVictim.Oid;
         Province = model.Province.Oid;
         District = model.District.Oid;
         Ward = model.Ward?.Oid;
         Address = model.Address;
         Note = model.Note;
         Disabilities = model.DisabilitiesStr;
         DisabilitiesValue = string.Join(";", model.Disabilities.Select(el => el.Oid));
         Locked = CheckLocked(model);
      }
      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Patient model;
         if (Id == 0)
            model = new DataModel.Patient(db.Session);
         else if (!TryGetModel(Id, out model)) return;

         model.Name = Name;
         if (Models.Gender.TryGetModel(Gender.Value, out var gender))
            model.Gender = gender;
         if (Models.Folk.TryGetModel(Folk.Value, out var folk))
            model.Folk = folk;
         model.DateBirth = DateBirth;
         model.Phone = Phone;
         model.CMND = CMND;
         model.CCCD = CCCD;
         model.BHYT = BHYT;
         if (Models.Classification.TryGetModel(Classification.Value, out var cl))
            model.Classification = cl;
         if (Classification == 3)
         {
            if (model.LevelDisability?.Oid != 4) model.LevelDisability = db.Session.GetObjectByKey<DataModel.LevelDisability>(4);
         }
         else if (Models.LevelDisability.TryGetModel(LevelDisability.Value, out var lv))
            model.LevelDisability = lv;
         if (Models.OrangeVictim.TryGetModel(OrangeVictim.Value, out var or))
            model.OrangeVictim = or;
         if (Models.Province.TryGetModel(Province.Value, out var pr))
            model.Province = pr;
         if (Models.District.TryGetModel(District.Value, out var di))
            model.District = di;
         if (!Ward.HasValue)
            model.Ward = null;
         else if (Models.Ward.TryGetModel(Ward.Value, out var wa))
            model.Ward = wa;
         model.Address = Address;
         model.Note = Note;
         UpdateDisabilities(model);

         db.Session.CommitChanges();
         if (string.IsNullOrEmpty(model.Code))
         {
            model.IsAudit = false;
            model.Code = string.Format("{0:00}-{1:000000}", model.Province.Oid, model.Oid);
            db.Session.CommitChanges();
            model.IsAudit = true;
         }
         Id = model.Oid;
      }

      private void UpdateDisabilities(DataModel.Patient model)
      {
         model.DisabilitiesStr = Disabilities;
         var ids = new List<int>();
         if (!string.IsNullOrEmpty(DisabilitiesValue))
            ids.AddRange(DisabilitiesValue.Split(';').Select(el => int.Parse(el)));

         //remove
         for (var i = 0; i < model.Disabilities.Count;)
         {
            if (!ids.Contains(model.Disabilities[i].Oid))
               model.Disabilities.Remove(model.Disabilities[i]);
            else
               i++;
         }
         //add
         foreach (var id in ids)
         {
            if (!model.Disabilities.Any(el => el.Oid == id))
            {
               model.Disabilities.Add(model.Session.GetObjectByKey<DataModel.Disability>(id));
            }
         }
      }

      public static Patient Create() => new Patient();

      public static Patient Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Patient(model);
      }

      public static string Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return "Bạn không có quyền xóa dữ liệu";
         if (CheckLocked(model)) return "Dữ liệu đã bị khóa";
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
         return null;
      }

      public bool CheckLevelDisability()
      {
         return Classification != 3;
      }

      public static bool TryGetModel(int id, out DataModel.Patient model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchPatient(id);
         if (model == null) return false;
         return true;
      }

      public static bool CheckLocked(DataModel.Patient model)
      {
         //shared data
         if (!Facility.TryGetModel(model.Facility.Oid, out var _)) return true;
         //locked
         if (LockedData.CheckLocked(model.DateCreate.Date)) return true;

         return false;
      }

      /// <summary>
      /// Get all patients to prevent duplicate between facility/user,...
      /// </summary>
      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryByCompany<DataModel.Patient>()
         orderby el.Name ascending
         select new { el.Oid, el.Code, el.Name, el.FullAddress, el.Phone, el.CMND, el.CCCD, el.BHYT };

      public static bool CheckCMND(int id, string s)
      {
         if (string.IsNullOrEmpty(s)) return true;
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Patient>(CriteriaOperator.Parse("Company==? && This!=? && CMND==?", db.CompanyId, id, s));
         return model == null;
      }

      public static bool CheckCCCD(int id, string s)
      {
         if (string.IsNullOrEmpty(s)) return true;
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Patient>(CriteriaOperator.Parse("Company==? && This!=? && CCCD==?", db.CompanyId, id, s));
         return model == null;
      }

      public static bool CheckBHYT(int id, string s)
      {
         if (string.IsNullOrEmpty(s)) return true;
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Patient>(CriteriaOperator.Parse("Company==? && This!=? && BHYT==?", db.CompanyId, id, s));
         return model == null;
      }

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.Medical>()
                  where el.Patient.Oid == id
                  select el).Count();
         return c == 0;
      }
   }
}