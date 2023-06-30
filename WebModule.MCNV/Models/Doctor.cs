using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class Doctor
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Doctor> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryDoctor();
         }
      }

      public class ViewInfo
      {
         [Caption("Họ và tên")]
         public string Name { get; }
         [Caption("Giới tính")]
         public string Gender { get; }
         [Caption("Ngày sinh")]
         public string DateBirth { get; }
         [Caption("Đơn vị công tác")]
         public string Facility { get; }
         [Caption("Điện thoại")]
         public string Phone { get; }
         [Caption("Email")]
         public string Email { get; }
         [Caption("Trình độ chuyên môn")]
         public string Qualification { get; }
         [Caption("Chức danh tham gia")]
         public string Title { get; }
         [Caption("Vị trí công tác")]
         public string Position { get; }
         [Caption("Lĩnh vực chuyên môn")]
         public string Specialize { get; }
         [Caption("Ghi chú")]
         public string Note { get; }

         public ViewInfo(DataModel.Doctor model)
         {
            Name = model.Name;
            Gender = model.Gender.Name;
            DateBirth = model.DateBirth?.ToShortDateString();
            Facility = model.Facility.Name;
            Phone = model.Phone;
            Email = model.Email;
            Qualification = model.Qualification?.Name;
            Title = model.Title?.Name;
            Specialize = model.Specialize?.Name;
            Position = model.Position?.Name;
            Note = model.Note;
         }

         public static ViewInfo Load(int id)
         {
            if (TryGetModel(id, out var model)) return new ViewInfo(model);
            return null;
         }
      }

      public int Id { get; set; }
      [Caption("Họ và tên"), Required]
      [Remote("CheckName", "Doctor", "Tên cán bộ này đã có sử dụng", AdditionalFields = "Id,DateBirthText")]
      public string Name { get; set; }
      [Caption("Giới tính"), Required, Template("Gender")]
      public int? Gender { get; set; }
      [Caption("Ngày sinh"), Required]
      public DateTime? DateBirth { get; set; }
      [Caption("Vị trí công tác"), Template("Position")]
      public int? Position { get; set; }
      [Caption("Chức danh tham gia"), Template("Title")]
      public int? Title { get; set; }
      [Caption("Trình độ chuyên môn"), Template("Qualification")]
      public int? Qualification { get; set; }
      [Caption("Lĩnh vực chuyên môn"), Template("Specialize"), Required]
      public int? Specialize { get; set; }
      [Caption("Điện thoại"), Template("Phone")]
      public string Phone { get; set; }
      [Caption("Email"), Email]
      public string Email { get; set; }
      [Caption("Đơn vị công tác"), Required, Template("Facility")]
      public int? Facility { get; set; }
      public string FacilityNameLocked { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public bool Locked { get; }

      public Doctor() { }

      public Doctor(DataModel.Doctor model)
      {
         Id = model.Oid;
         Name = model.Name;
         Gender = model.Gender?.Oid;
         DateBirth = model.DateBirth;
         Facility = model.Facility?.Oid;
         Phone = model.Phone;
         Email = model.Email;
         Title = model.Title?.Oid;
         Qualification = model.Qualification?.Oid;
         Specialize = model.Specialize?.Oid;
         Position = model.Position?.Oid;
         Note = model.Note;
         Locked = CheckLocked(model);
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Doctor model;
         if (Id == 0)
            model = new DataModel.Doctor(db.Session);
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;
         if (Gender == null)
            model.Gender = null;
         else if (Models.Gender.TryGetModel(Gender.Value, out var gender))
            model.Gender = gender;
         model.DateBirth = DateBirth;
         if (Facility == null)
            model.Facility = null;
         else if (Models.Facility.TryGetModel(Facility.Value, out var facility))
            model.Facility = facility;
         model.Phone = Phone;
         model.Email = Email;
         if (Title == null)
            model.Title = null;
         else if (DoctorTitle.TryGetModel(Title.Value, out var title))
            model.Title = title;
         if (Qualification == null)
            model.Qualification = null;
         else if (Models.Qualification.TryGetModel(Qualification.Value, out var qualification))
            model.Qualification = qualification;
         if (Specialize == null)
            model.Specialize = null;
         else if (Models.Specialize.TryGetModel(Specialize.Value, out var specialize))
            model.Specialize = specialize;
         if (Position == null)
            model.Position = null;
         else if (Models.DoctorPosition.TryGetModel(Position.Value, out var position))
            model.Position = position;
         model.Note = Note;
         db.Session.CommitChanges();
         if (string.IsNullOrEmpty(model.Code))
         {
            model.IsAudit = false;
            model.Code = string.Format("{0:00}-{1:000000}", model.Facility.Province.Oid, model.Oid);
            db.Session.CommitChanges();
            model.IsAudit = true;
         }
         Id = model.Oid;
      }

      public static Doctor Create(int? defFacility)
      {
         var vm = new Doctor();
         if (defFacility != null && Models.Facility.TryGetModel(defFacility.Value, out var facility))
         {
            vm.Facility = defFacility;
            vm.FacilityNameLocked = facility.Name;
         }
         return vm;
      }

      public static Doctor Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Doctor(model);
      }

      public static string Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return "Bạn không có quyền xóa dữ liệu";
         if (CheckLocked(model)) return "Dữ liệu đã bị khóa";
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
         return null;
      }

      public static bool TryGetModel(int id, out DataModel.Doctor model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchDoctor(id);
         if (model == null) return false;
         return true;
      }

      public class ComboboxItem
      {
         public int Id { get; set; }
         public string Name { get; set; }
         public string Specialize { get; set; }
      }
      public static List<ComboboxItem> ComboboxDataByFacility(int id, List<int> values) =>
         (from el in ApplicationDbContext.Current.Session.Query<DataModel.Doctor>()
          where el.Company == ApplicationDbContext.Current.CompanyId && el.Facility.Oid == id
          select new ComboboxItem() { Id = el.Oid, Name = el.Name, Specialize = el.Specialize.Name }).ToList()
         .OrderByDescending(it => values.Contains(it.Id)).ThenBy(it => it.Name).ToList();

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.MedicalProcess>()
                  from d in el.Doctors
                  where d.Oid == id
                  select d).Count();
         if (c > 0) return false;
         c = (from el in db.Session.Query<DataModel.Medical>()
              from d in el.Doctors
              where d.Oid == id
              select d).Count();
         if (c > 0) return false;
         c = (from el in db.Session.Query<DataModel.ProgramDoctor>()
              where el.Doctor.Oid == id
              select el).Count();
         return c == 0;
      }

      public static bool CheckLocked(DataModel.Doctor model) => LockedData.CheckLocked(model.DateCreate.Date);

      public static bool CheckName(int id, string name, DateTime? dateBirth)
      {
         if (dateBirth == null) return true;
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Doctor>(CriteriaOperator.Parse("Company=? && Name=? && DateBirth=? && This!=?", db.CompanyId, name, dateBirth, id));
         return model == null;
      }
   }
}