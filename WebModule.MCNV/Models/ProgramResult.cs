using System;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Web;
using DevExpress.Utils.Extensions;
using WebModule.MCNV.DataModel;

namespace WebModule.MCNV.Models
{
   public class ProgramResult
   {
      public class DoctorResult
      {
         public int Id { get; set; }
         public string FacilityName { get; }
         public string DoctorName { get; }
         public string Specialize { get; }
         [Required(OnlyIf = "CheckReasonCancel")]
         public string ReasonCancel { get; set; }
         public int? Status { get; set; }
         public decimal? Score1 { get; set; }
         public decimal? Score2 { get; set; }
         public decimal? Score3 { get; set; }
         public decimal? Score4 { get; set; }
         public decimal? Score5 { get; set; }
         public decimal? Score6 { get; set; }
         public decimal? Score7 { get; set; }
         public decimal? Score8 { get; set; }
         public decimal? Score9 { get; set; }
         public decimal? Score10 { get; set; }
         public decimal? Score11 { get; set; }
         public decimal? Score12 { get; set; }
         public decimal? Score13 { get; set; }
         public decimal? Score14 { get; set; }
         public decimal? Score15 { get; set; }
         public decimal? Score16 { get; set; }

         public decimal HoursTotal { get; }
         public decimal HoursLeft { get; }

         public DoctorResult() { }
         public DoctorResult(DataModel.ProgramDoctor model)
         {
            Id = model.Oid;
            FacilityName = model.Doctor.Facility.Name;
            DoctorName = model.Doctor.Name;
            Specialize = model.Doctor.Specialize?.Name;
            ReasonCancel = model.ReasonCancel;
            Status = model.Status?.Oid;
            Score1 = model.Score1;
            Score2 = model.Score2;
            Score3 = model.Score3;
            Score4 = model.Score4;
            Score5 = model.Score5;
            Score6 = model.Score6;
            Score7 = model.Score7;
            Score8 = model.Score8;
            Score9 = model.Score9;
            Score10 = model.Score10;
            Score11 = model.Score11;
            Score12 = model.Score12;
            Score13 = model.Score13;
            Score14 = model.Score14;
            Score15 = model.Score15;
            Score16 = model.Score16;

            if (model.Program.ProgramType == ProgramTypeEnum.Coaching)
            {
               var lst = new List<decimal?>() { Score1, Score2, Score3, Score4, Score5, Score6, Score7, Score8, Score9, Score10, Score11, Score12 };
               foreach (var d in lst)
               {
                  if (d.HasValue) HoursTotal += d.Value;
               }
               HoursLeft = model.Program.TrainingHour - HoursTotal;
            }
         }

         public bool CheckReasonCancel() => this.Status == 3;
      }
      public int Id { get; set; }
      public ProgramTypeEnum ProgramType { get; set; }
      public bool Locked { get; }
      public bool ShowFilter { get; set; }
      public Attachments Attachments { get; set; }
      public List<DoctorResult> Doctors { get; set; }

      public ProgramResult()
      {
         Attachments = new Attachments(nameof(Attachments));
         Doctors = new List<DoctorResult>();
      }
      public ProgramResult(DataModel.Program model)
      {
         Id = model.Oid;
         ProgramType = model.ProgramType;
         Locked = CheckLocked(model);
         Attachments = new Attachments(nameof(Attachments), model.Attachments);
      }

      public void Save()
      {
         if (!Program.TryGetModel(Id, out var model)) return;
         Attachments.Update(model.Attachments, "Program", model.Oid.ToString());
         foreach (var it in model.Doctors)
         {
            var data = Doctors.FirstOrDefault(d => d.Id == it.Oid);
            if (data != null)
            {
               if (data.Status == null)
                  it.Status = null;
               else if (TrainingStatus.TryGetModel(data.Status.Value, out var status))
                  it.Status = status;
               it.ReasonCancel = data.ReasonCancel;
               it.Score1 = data.Score1;
               it.Score2 = data.Score2;
               it.Score3 = data.Score3;
               it.Score4 = data.Score4;
               it.Score5 = data.Score5;
               it.Score6 = data.Score6;
               it.Score7 = data.Score7;
               it.Score8 = data.Score8;
               it.Score9 = data.Score9;
               it.Score10 = data.Score10;
               it.Score11 = data.Score11;
               it.Score12 = data.Score12;
               it.Score13 = data.Score13;
               it.Score14 = data.Score14;
               it.Score15 = data.Score15;
               it.Score16 = data.Score16;
            }
         }
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public IQueryable<DoctorResult> GetDoctors()
      {
         var db = ApplicationDbContext.Current;
         var f = db.Session.QueryFacility().Select(it => it.Oid).ToList();
         if (!Program.TryGetModel(Id, out var model)) return null;
         return from el in db.Session.Query<DataModel.ProgramDoctor>()
                where el.Program.Oid == Id && f.Contains(el.Doctor.Facility.Oid)
                orderby el.Doctor.Name
                select new DoctorResult(el);
      }

      public static ProgramResult Load(int id)
      {
         if (!Program.TryGetModel(id, out var model)) return null;
         return new ProgramResult(model);
      }

      public static bool CheckLocked(DataModel.Program model) => model.DateEnd.HasValue && LockedData.CheckLocked(model.DateEnd.Value);

      public static IQueryable<dynamic> Export(int program)
      {
         var db = ApplicationDbContext.Current;
         var f = db.Session.QueryFacility().Select(it => it.Oid).ToList();
         if (!Program.TryGetModel(program, out var model)) return null;
         return from el in db.Session.Query<DataModel.ProgramDoctor>()
                where el.Program.Oid == program && f.Contains(el.Doctor.Facility.Oid)
                orderby el.Doctor.Facility.Name, el.Doctor.Name
                select new
                {
                   el.Doctor.Oid,
                   FacilityName = el.Doctor.Facility.Name,
                   DoctorName = el.Doctor.Name,
                   el.Doctor.Code,
                   Gender = el.Doctor.Gender.Name,
                   Area = el.Doctor.Facility.Area.Name,
                   el.Doctor.DateBirth,
                   Province = el.Doctor.Facility.Province.Name,
                   FacilityType = el.Doctor.Facility.FacilityType.Name,
                   Position = el.Doctor.Position.Name,
                   Qualification = el.Doctor.Qualification.Name,
                   Specialize = el.Doctor.Specialize.Name,
                   Title = el.Doctor.Title.Name,
                   TrainingType = el.Program.Specialize.Oid == el.Doctor.Specialize.Oid && el.Doctor.Qualification.Oid != 5 && el.Doctor.Qualification.Oid != 6
                     ? "Chính quy" : "Không chính quy",
                   StatusType = el.Program.DateStart > DateTime.Today
                     ? "Chưa bắt đầu"
                     : el.Status.Oid == 3
                        ? "Nghỉ học"
                        : el.Program.DateEnd < DateTime.Today
                           ? "Đã hoàn thành"
                           : "Đang theo học",
                   el.Score1,
                   el.Score2,
                   el.Score3,
                   el.Score4,
                   el.Score5,
                   el.Score6,
                   el.Score7,
                   el.Score8,
                   el.Score9,
                   el.Score10,
                   el.Score11,
                   el.Score12,
                   el.Score13,
                   el.Score14,
                   el.Score15,
                   el.Score16,
                   HoursTotal = el.Program.ProgramType == ProgramTypeEnum.Coaching
                     ? el.Score1 + el.Score2 + el.Score3 + el.Score4 + el.Score5 + el.Score6 + el.Score7 + el.Score8 + el.Score9 + el.Score10 + el.Score11 + el.Score12
                     : 0,
                   HoursLeft = el.Program.ProgramType == ProgramTypeEnum.Coaching
                     ? el.Program.TrainingHour - (el.Score1 + el.Score2 + el.Score3 + el.Score4 + el.Score5 + el.Score6 + el.Score7 + el.Score8 + el.Score9 + el.Score10 + el.Score11 + el.Score12)
                     : 0,
                   el.ReasonCancel,
                   Status = el.Status.Name,
                };
      }
   }
}