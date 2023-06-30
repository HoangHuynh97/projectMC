using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering.Helpers;

namespace WebModule.MCNV.Models
{
   public enum ProgramTypeEnum
   {
      Coaching = 1,
      ShortTime = 2,
      LongTimeLongTerm = 3,
      LongTimeMidTerm = 4,
      LongTimeContinuous = 5,
      LongTimeIntensive = 6
   }

   public class Program
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Program> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryProgram();
         }
      }

      public class ViewInfo
      {
         [Caption("Dự án")]
         public string Project { get; }
         [Caption("Đơn vị đào tạo")]
         public string Institution { get; }
         [Caption("Chuyên môn đào tạo")]
         public string Specialize { get; }
         [Caption("Thời lượng")]
         public string TrainingTime { get; }
         [Caption("Ngày khai giảng")]
         public string DateStart { get; }
         [Caption("Ngày bế giảng")]
         public string DateEnd { get; }
         [Caption("Ghi chú")]
         public string Note { get; }
         [Caption("Tên khóa học")]
         public string Name { get; }
         [Caption("Tên tiếng Anh")]
         public string NameEnglish { get; }
         [Caption("Loại khóa học")]
         public string ProgramType { get; }
         public List<DetailDoctor> Doctors { get; }
         public string DoctorsId { get; }
         public Attachments Attachments { get; }

         public ViewInfo(string doctorsId, List<DetailDoctor> doctors)
         {
            DoctorsId = doctorsId;
            Doctors = doctors;
         }

         public ViewInfo(DataModel.Program model)
         {
            Name = model.Name;
            NameEnglish = model.NameEnglish;
            Project = model.Project.Name;
            Institution = model.Institution.Name;
            Specialize = model.Specialize?.Name;
            if (model.ProgramType == ProgramTypeEnum.Coaching)
               ProgramType = Language.T("Huấn luyện");
            else if (model.ProgramType == ProgramTypeEnum.ShortTime)
               ProgramType = Language.T("Tập huấn");
            else if (model.ProgramType == ProgramTypeEnum.LongTimeLongTerm)
               ProgramType = Language.T("Đào tạo dài hạn");
            else if (model.ProgramType == ProgramTypeEnum.LongTimeMidTerm)
               ProgramType = Language.T("Đào tạo trung hạn");
            else if (model.ProgramType == ProgramTypeEnum.LongTimeContinuous)
               ProgramType = Language.T("Đào tạo liên tục");
            else if (model.ProgramType == ProgramTypeEnum.LongTimeIntensive)
               ProgramType = Language.T("Đào tạo chuyên sâu");
            TrainingTime = model.TrainingTime;
            DateStart = model.DateStart?.ToShortDateString() ?? "";
            DateEnd = model.DateEnd?.ToShortDateString() ?? "";
            Note = model.Note;
            var dts = GetDoctors(model.Oid).Select(it => it.Doctor).ToList();
            Doctors = dts.Select(it => new DetailDoctor()
            {
               Id = it.Oid,
               Name = it.Name,
               Facility = it.Facility.Name,
               FacilityId = it.Facility.Oid,
               Specialize = it.Specialize?.Name,
            }).ToList();
            DoctorsId = Guid.NewGuid().ToString();
            Attachments = new Attachments(nameof(Attachments), model.Attachments);
         }

         public static ViewInfo Load(int id)
         {
            if (TryGetModel(id, out var model)) return new ViewInfo(model);
            return null;
         }
      }

      public class DetailDoctor
      {
         public int Id { get; set; }
         public string Name { get; set; }
         public string Facility { get; set; }
         public int FacilityId { get; set; }
         public string Specialize { get; set; }
      }

      public class SelectDoctor
      {
         public string DoctorsId { get; set; }
         [Caption("Đơn vị công tác"), Required]
         public int? FacilityId { get; set; }
         [Caption("Chọn cán bộ"), Template("Doctors")]
         public Doctors Doctors { get; set; }

         public void LoadList()
         {
            var db = ApplicationDbContext.Current;
            var items = (List<Models.Program.DetailDoctor>)HttpContext.Current.Session[DoctorsId];
            Doctors = new Doctors(nameof(Doctors), FacilityId ?? 0, items.Select(it => db.Session.GetObjectByKey<DataModel.Doctor>(it.Id)))
            {
               Height = 300
            };
         }

         public void Update()
         {
            var doctors = (List<Models.Program.DetailDoctor>)HttpContext.Current.Session[DoctorsId];
            var ids = Doctors.Items.Select(it => it.Id).ToList();
            var db = ApplicationDbContext.Current;
            //removed
            doctors.RemoveAll(it => it.FacilityId == FacilityId && !ids.Contains(it.Id));
            //add
            var query = from el in db.Session.QueryByCompany<DataModel.Doctor>()
                        where ids.Contains(el.Oid)
                        select new DetailDoctor()
                        {
                           Id = el.Oid,
                           Name = el.Name,
                           Facility = el.Facility.Name,
                           FacilityId = el.Facility.Oid,
                           Specialize = el.Specialize.Name,
                        };
            var lst = query.ToList();
            var added = lst.Where(it => doctors.All(d => it.Id != d.Id)).ToList();
            doctors.AddRange(added);
         }
      }

      public int Id { get; set; }
      [Caption("Dự án"), Required, Template("ProgramProject")]
      public int? Project { get; set; }
      [Caption("Tên khóa học"), Required]
      public string ProgramName { get; set; }
      [Caption("Tên tiếng Anh")]
      public string NameEnglish { get; set; }
      [Caption("Loại khóa học")]
      public ProgramTypeEnum ProgramType { get; set; }

      [Caption("Đơn vị đào tạo"), Required, Template("Institution")]
      public int? Institution { get; set; }
      [Caption("Chuyên môn đào tạo"), Required, Template("Specialize")]
      public int? ProgramSpecialize { get; set; }
      [Caption("Thời lượng"), Required(OnlyIf = "CheckTrainingTime"), Template("TrainingTime")]
      public string TrainingTime { get; set; }
      [Caption("Thời lượng"), Required(OnlyIf = "CheckTrainingHour")]
      public int TrainingHour { get; set; }
      [Caption("Ngày khai giảng")]
      public DateTime? DateStart { get; set; }
      [Caption("Ngày bế giảng")]
      public DateTime? DateEnd { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }
      public bool Locked { get; }
      public List<DetailDoctor> Doctors { get; set; }
      public string DoctorsId { get; set; }

      public Attachments Attachments { get; set; }

      public Program()
      {
         Doctors = new List<DetailDoctor>();
         DoctorsId = Guid.NewGuid().ToString();
         Attachments = new Attachments(nameof(Attachments));
         ProgramType = ProgramTypeEnum.LongTimeLongTerm;
      }

      public Program(DataModel.Program model)
      {
         Id = model.Oid;
         Project = model.Project?.Oid;
         Institution = model.Institution?.Oid;
         ProgramSpecialize = model.Specialize?.Oid;
         TrainingTime = model.TrainingTime;
         DateStart = model.DateStart;
         DateEnd = model.DateEnd;
         Note = model.Note;
         Locked = CheckLocked(model);
         var dts = GetDoctors(model.Oid).Select(it => it.Doctor).ToList();
         Doctors = dts.Select(it => new DetailDoctor()
         {
            Id = it.Oid,
            Name = it.Name,
            Facility = it.Facility.Name,
            FacilityId = it.Facility.Oid,
            Specialize = it.Specialize?.Name,
         }).ToList();
         DoctorsId = Guid.NewGuid().ToString();
         Attachments = new Attachments(nameof(Attachments), model.Attachments);
         ProgramName = model.Name;
         NameEnglish = model.NameEnglish;
         ProgramType = model.ProgramType;
         TrainingTime = model.TrainingTime;
         TrainingHour = model.TrainingHour;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Program model;
         if (Id == 0)
         {
            model = new DataModel.Program(db.Session) { };
            if (Facility.TryGetModel(db.UserModel.GroupId.Value, out var facility))
               model.Facility = facility;
         }
         else if (!TryGetModel(Id, out model)) return;
         if (Models.ProgramProject.TryGetModel(Project.Value, out var project))
            model.Project = project;
         if (Models.Institution.TryGetModel(Institution.Value, out var institution))
            model.Institution = institution;
         if (ProgramSpecialize == null)
            model.Specialize = null;
         else if (Models.Specialize.TryGetModel(ProgramSpecialize.Value, out var specialize))
            model.Specialize = specialize;
         model.TrainingTime = TrainingTime;
         model.DateStart = DateStart;
         model.DateEnd = DateEnd;
         model.Note = Note;
         model.Name = ProgramName;
         model.NameEnglish = NameEnglish;
         model.ProgramType = ProgramType;
            model.TrainingTime = TrainingTime;
         if (ProgramType == ProgramTypeEnum.Coaching)
         {
            model.TrainingHour = TrainingHour;
         }
         else
         {
            model.TrainingHour = 0;
         }
         //remove old doctors
         var dts = GetDoctors(model.Oid).ToList();
         var removed = dts.Where(it => Doctors.All(d => d.Id != it.Doctor.Oid)).ToList();
         foreach (var it in removed) it.Delete();
         //add new doctors 
         foreach (var d in Doctors)
         {
            var obj = dts.FirstOrDefault(it => it.Doctor.Oid == d.Id);
            if (obj == null)
            {
               obj = new DataModel.ProgramDoctor(model.Session)
               {
                  Doctor = model.Session.GetObjectByKey<DataModel.Doctor>(d.Id)
               };
               model.Doctors.Add(obj);
            }
         }
         Attachments.Update(model.Attachments, "Program", model.Oid.ToString());
         model.DoctorsCount = model.Doctors.Count;

         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public bool CheckTrainingHour() => ProgramType == ProgramTypeEnum.Coaching;

      public bool CheckTrainingTime() => ProgramType != ProgramTypeEnum.Coaching;

      public static Program Create() => new Program();

      public static Program Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Program(model);
      }

      public static string Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return "Bạn không có quyền xóa dữ liệu";
         if (CheckLocked(model)) return "Dữ liệu đã bị khóa";

         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
         return null;
      }

      public static bool TryGetModel(int id, out DataModel.Program model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchProgram(id);
         if (model == null || model.Company != db.CompanyId) return false;
         return true;
      }

      public static bool CheckLocked(DataModel.Program model) => model.DateStart.HasValue && LockedData.CheckLocked(model.DateStart.Value);

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryProgram()
         orderby el.DateStart descending
         select new { el.Oid, el.Name };

      /// <summary>
      /// Get list doctor that user have access permission via facility
      /// </summary>
      /// <returns></returns>
      public static IQueryable<DataModel.ProgramDoctor> GetDoctors(int programId)
      {
         var db = ApplicationDbContext.Current;
         var f = db.Session.QueryFacility().Select(it => it.Oid).ToList();
         var query = from el in db.Session.Query<DataModel.ProgramDoctor>()
                     where el.Program.Oid == programId && f.Contains(el.Doctor.Facility.Oid)
                     orderby el.Doctor.Facility.Name, el.Doctor.Name
                     select el;
         return query;
      }


      public static List<string> ListTrainingTime()
      {
         var db = ApplicationDbContext.Current;
         var query = from el in db.Session.QueryByCompany<DataModel.Program>()
                     orderby el.TrainingTime
                     select el.TrainingTime;
         return query.Distinct().ToList();
      }
   }
}