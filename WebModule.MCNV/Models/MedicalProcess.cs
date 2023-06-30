using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using WebApp.Core;
using DevExpress.Utils.Extensions;

namespace WebModule.MCNV.Models
{
   public class MedicalProcess
   {
      public class Process
      {
         public int Id { get; set; }
         public string Url { get; set; }
         public string Name { get; set; }
      }
      public int Id { get; set; }
      public string Name { get; set; }
      public bool Locked { get; set; }
      public bool CanAdd { get; set; }
      public List<Process> Processes { get; set; }

      public MedicalProcess() { }
      public MedicalProcess(DataModel.Medical model)
      {
         Id = model.Oid;
         if (!string.IsNullOrEmpty(model.Code))
            Name = string.Format("{0} - {1} / {2}", model.Code, model.Patient.Code, model.Patient.Name);
         else
            Name = string.Format("{0} / {1}", model.Patient.Code, model.Patient.Name);
         Locked = CheckLocked(model);
         CanAdd = !Locked && model.Status?.Oid != MedicalStatus.NotProcess && model.CompletedDate == null && model.EvaluationDate != null && model.MedicalPlans.Count > 0;
         Processes = new List<Process>()
         {
            new Process() { Id = -1, Url = $"/Medical/InfoProcess/{Id}", Name = Language.T("Hồ sơ PHCN") },
            new Process() { Id = -2, Url = $"/Medical/BeginProcess/{Id}", Name = Language.T("Lượng giá nhu cầu {0}", model.EvaluationDate?.ToShortDateString() ?? "")}
         };
         if (model.Status.Oid != 5)
         {
            if (model.EvaluationDate != null)
               Processes.Add(new Process() { Id = -5, Url = $"/Medical/Plan/{Id}", Name = Language.T("Kế hoạch can thiệp {0}", model.PlanDate?.ToShortDateString() ?? "") });

            Processes.AddRange(model.Processes.OrderBy(it => it.ProcessDate).ThenBy(it => it.ProcessInd).Select(it => new Process()
            {
               Id = it.Oid,
               Url = it.AtFacility ? $"/Medical/FacilityProcess/{it.Oid}" : $"/Medical/DetailProcess/{it.Oid}",
               Name = it.AtFacility ? Language.T("Can thiệp tại viện lần {0} ngày {1} - {2}", it.ProcessInd, it.DateIn.ToShortDateString(), it.DateOut?.ToShortDateString())
                  : Language.T("Can thiệp tại nhà lần {0} ngày {1}", it.ProcessInd, it.ProcessDate.ToShortDateString())
            }));
            if (model.CompletedDate != null && model.Cancel == null)
               Processes.Add(new Process()
               {
                  Id = -3,
                  Url = $"/Medical/EndProcess/{Id}",
                  Name = Language.T("Kết thúc can thiệp {0}", model.CompletedDate?.ToShortDateString() ?? "")
               });
            if (model.CompletedDate != null && model.Cancel != null)
               Processes.Add(new Process()
               {
                  Id = -4,
                  Url = $"/Medical/StopProcess/{Id}",
                  Name = Language.T("Dừng can thiệp {0}", model.CompletedDate?.ToShortDateString() ?? "")
               });
         }
      }

      public static MedicalProcess Create()
      {
         return new MedicalProcess()
         {
            Id = 0,
            Name = "",
            Locked = false,
            CanAdd = false,
            Processes = new List<Process>() { new Process() { Id = -1, Url = "/Medical/InfoProcess", Name = Language.T("Hồ sơ PHCN") } },
         };
      }
      public static MedicalProcess Load(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return null;
         return new MedicalProcess(model);
      }

      public static bool CheckLocked(DataModel.Medical model)
      {
         //shared data
         if (!Facility.TryGetModel(model.Facility.Oid, out var _)) return true;

         return false;
      }

      public static bool UnlockProcess()
      {
         return System.Web.Mvc.Helpers.CheckPermission(HttpContext.Current.Request.RequestContext.HttpContext, "WebModule.MCNV.dll", "Hồ sơ PHCN", "Mở khóa quy trình");
      }

      public static void DeleteEndProcess(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return;
         if (EndProcess.CheckLocked(model)) return;
         var db = ApplicationDbContext.Current;
         model.CompletedDate = null;
         model.EndEvaluation = null;
         model.EndEvaluationTool = null;
         model.EndEvaluationPoint = null;
         model.EndSuccess = false;
         while (model.EndDoctors.Count > 0)
         {
            model.EndDoctors.Remove(model.EndDoctors[0]);
         }
         db.Session.Delete(model.CompletedAttachments);
         UpdateMedicalStatus(model);
         db.Session.CommitChanges();
      }

      public static void DeleteStopProcess(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return;
         if (EndProcess.CheckLocked(model)) return;
         var db = ApplicationDbContext.Current;
         model.CompletedDate = null;
         model.Cancel = null;
         while (model.EndDoctors.Count > 0)
         {
            model.EndDoctors.Remove(model.EndDoctors[0]);
         }
         db.Session.Delete(model.CompletedAttachments);
         UpdateMedicalStatus(model);
         db.Session.CommitChanges();
      }

      public static void DeleteDetailProcess(int id)
      {
         if (!DetailProcess.TryGetModel(id, out var model)) return;
         if (DetailProcess.CheckLocked(model)) return;
         var db = ApplicationDbContext.Current;
         var medical = model.Medical;
         db.Session.Delete(model.Attachments);
         model.Delete();
         UpdateMedicalStatus(medical);
         db.Session.CommitChanges();
      }

      public static void DeleteFacilityProcess(int id)
      {
         if (!FacilityProcess.TryGetModel(id, out var model)) return;
         if (FacilityProcess.CheckLocked(model)) return;
         var db = ApplicationDbContext.Current;
         var medical = model.Medical;
         db.Session.Delete(model.Attachments);
         model.Delete();
         UpdateMedicalStatus(medical);
         db.Session.CommitChanges();
      }

      public static bool CheckValidData(DataModel.Medical model)
      {
         if (model.Processes.Count == 0)
         {
            if (model.EvaluationDate == null || model.EvaluationTool == null || model.Doctors.Count == 0) return false;
         }
         else
         {
            var lst = model.Processes.OrderBy(it => it.ProcessDate).ThenBy(it => it.Oid).ToList();
            var last = lst[lst.Count - 1];
            if (!last.ValidData) return false;
         }
         return true;
      }

      /// <summary>
      /// Update medical status base on data or by a specific status
      /// </summary>
      public static void UpdateMedicalStatus(DataModel.Medical model, int status = 0)
      {
         if (status == 0)
         {
            if (model.CompletedDate != null) status = model.Cancel != null ? MedicalStatus.Canceled : MedicalStatus.Completed;
            else if (model.Processes.Count > 0) status = MedicalStatus.Processing;
            else status = MedicalStatus.NotStarted;
         }

         if (MedicalStatus.TryGetModel(status, out var st))
            model.Status = st;

         //update index of detail process
         var athome = model.Processes.Where(it => !it.AtFacility).OrderBy(it => it.ProcessDate).ThenBy(it => it.DateCreate).ToList();
         for (var i = 0; i < athome.Count; i++)
            athome[i].ProcessInd = i + 1;
         var atfacility = model.Processes.Where(it => it.AtFacility).OrderBy(it => it.DateIn).ThenBy(it => it.DateCreate).ToList();
         for (var i = 0; i < atfacility.Count; i++)
            atfacility[i].ProcessInd = i + 1;

         //update info last user, date and combine note explain
         model.LastUser = ApplicationDbContext.Current.UserModel;
         model.LastDate = DateTime.Now;
         var lst = new List<string>() { model.Note };
         if (model.Status.Oid == MedicalStatus.NotProcess)
            lst.Add(model.Reason?.Name);
         else if (model.Status.Oid == MedicalStatus.Canceled)
            lst.Add(model.Cancel?.Name);
         else
            lst.Add(model.EndEvaluation);
         model.Explanation = string.Join("; ", lst.Where(s => !string.IsNullOrEmpty(s)));
      }
   }

   public class InfoProcess
   {
      public int Id { get; set; }
      public bool Locked { get; set; }
      [Caption("Mã bệnh án"), Remote("CheckCode", "Medical", "Mã bệnh án này đã nhập rồi", AdditionalFields = "Id")]
      public string Code { get; set; }
      [Caption("Bệnh nhân"), Template("Patient"), Required]
      public int? Patient { get; set; }
      [Caption("Dạng khuyết tật")]
      public string Disabilities { get; set; }
      [Caption("Đơn vị cung cấp dịch vụ"), Template("Facility"), Required]
      public int? Facility { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }

      public bool PatientFacilityEditable { get; }
      public bool CanAccessPatient { get; }
      public string FacilityOfPatient { get; }
      [Caption("Mã định danh NKT")]
      public string PatientCode { get; }
      [Caption("Họ và tên")]
      public string PatientName { get; }
      [Caption("Giới tính")]
      public string PatientGender { get; }
      [Caption("Ngày sinh")]
      public string PatientDateBirth { get; }
      [Caption("Địa chỉ")]
      public string PatientAddress { get; }
      [Caption("Điện thoại")]
      public string PatientPhone { get; set; }
      [Caption("Nạn nhân da cam")]
      public string PatientOrangeVictim { get; }
      [Caption("Đối tượng khuyết tật")]
      public string PatientClassification { get; }
      [Caption("Mức độ khuyết tật")]
      public string PatientLevelDisability { get; }
      [Caption("Đơn vị cung cấp dịch vụ")]
      public string FacilityName { get; }
      [Caption("Địa chỉ")]
      public string FacilityAddress { get; }
      public bool HasRequestPatient { get; }

      public InfoProcess()
      {
         PatientFacilityEditable = true;
      }

      public InfoProcess(DataModel.Medical model)
      {
         Id = model.Oid;
         Code = model.Code;
         Patient = model.Patient.Oid;
         Disabilities = model.Patient.DisabilitiesStr;
         Facility = model.Facility?.Oid;
         Note = model.Note;
         Locked = CheckLocked(model);

         PatientFacilityEditable = false;
         PatientCode = model.Patient.Code;
         PatientName = model.Patient.Name;
         CanAccessPatient = Models.Patient.TryGetModel(model.Patient.Oid, out var patient);
         if (CanAccessPatient)
         {
            PatientGender = patient.Gender.Name;
            PatientDateBirth = patient.DateBirth.ToShortDateString();
            PatientPhone = patient.Phone;
            PatientAddress = patient.FullAddress;
            PatientClassification = patient.Classification.Name;
            PatientLevelDisability = patient.LevelDisability.Name;
            PatientOrangeVictim = patient.OrangeVictim.Name;
         }
         else
         {
            FacilityOfPatient = model.Patient.Facility?.Name;
            HasRequestPatient = SharedData.HasSentRequest(model.Patient.Oid);
         }
         FacilityName = model.Facility.Name;
         FacilityAddress = model.Facility.FullAddress;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Medical model;
         if (Id == 0)
            model = new DataModel.Medical(db.Session);
         else if (!Medical.TryGetModel(Id, out model)) return;

         model.Code = Code;
         model.Patient = db.Session.GetObjectByKey<DataModel.Patient>(Patient);
         if (Models.Facility.TryGetModel(Facility.Value, out var facility))
            model.Facility = facility;
         MedicalProcess.UpdateMedicalStatus(model);
         model.Note = Note;
         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static InfoProcess Create() => new InfoProcess();

      public static InfoProcess Load(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return null;
         return new InfoProcess(model);
      }

      public static bool CheckLocked(DataModel.Medical model)
      {
         if (MedicalProcess.CheckLocked(model)) return true;
         if (LockedData.CheckLocked(model.DateCreate.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         return model.EvaluationDate != null;
      }
   }

   public class BeginProcess
   {
      public int Id { get; set; }
      [Caption("Ngày lượng giá nhu cầu"), Required]
      public DateTime? EvaluationDate { get; set; }
      [Caption("Công cụ đánh giá"), Template("EvaluationTool"), Required]
      public int? EvaluationTool { get; set; }
      [Caption("Công cụ đánh giá")]
      public string EvaluationToolName { get; }
      [Caption("Tổng điểm")]
      public string EvaluationPoint { get; set; }
      [Caption("Kết quả lượng giá")]
      public string Evaluation { get; set; }
      [Caption("Chẩn đoán"), Template("Diagnostics"), Required]
      public string Diagnostics { get; set; }
      public string DiagnosticsValue { get; set; }
      [Caption("Gói can thiệp"), Template("Treatments")]
      public string Treatments { get; set; }
      public string TreatmentsValue { get; set; }
      [Caption("Không thực hiện can thiệp, điều trị"), Required]
      public bool? NoProcess { get; set; }
      [Caption("Lĩnh vực can thiệp"), Template("Services"), Required(OnlyIf = "CheckService")]
      public string Services { get; set; }
      public string ServicesValue { get; set; }
      public Attachments Attachments { get; set; }
      public int Facility { get; set; }
      [Caption("Cán bộ lượng giá"), Template("Doctors")]
      public Doctors Doctors { get; set; }
      public bool Locked { get; set; }
      [Caption("Lý do không can thiệp"), Template("MedicalReason"), Required(OnlyIf = "CheckReasonCancel")]
      public string ReasonCancel { get; set; }

      public BeginProcess()
      {
         Attachments = new Attachments(nameof(BeginProcess));
         Doctors = new Doctors(nameof(BeginProcess));
      }
      public BeginProcess(DataModel.Medical model)
      {
         Id = model.Oid;
         EvaluationDate = model.EvaluationDate;
         EvaluationTool = model.EvaluationTool?.Oid;
         EvaluationToolName = model.EvaluationTool?.Name ?? "";
         EvaluationPoint = model.EvaluationPoint;
         Evaluation = model.Evaluation;
         Diagnostics = model.DiagnosticsStr;
         DiagnosticsValue = string.Join(";", model.Diagnostics.Select(el => el.Oid));
         Treatments = model.TreatmentsStr;
         TreatmentsValue = string.Join(";", model.Treatments.Select(el => el.Oid));
         Services = model.ServicesStr;
         ServicesValue = string.Join(";", model.Services.Select(el => el.Oid));
         var ind = 0;
         Attachments = new Attachments(nameof(BeginProcess), model.EvaluationAttachments,
            att => $"{model.EvaluationDate.Value.ToString("yyMMdd")}_{model.Patient.Code}_Lượng giá_{model.Session.GetObjectByKey<WebApp.DataModel.User>(att.UserCreate).UserName}_{++ind}");
         Facility = model.Facility.Oid;
         Doctors = new Doctors(nameof(BeginProcess), Facility, model.Doctors);
         Locked = CheckLocked(model);
         if (model.Status == null || model.EvaluationDate == null)
            NoProcess = null;
         else
            NoProcess = model.Status.Oid == 5;
         ReasonCancel = model.Reason?.Name;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         if (!Medical.TryGetModel(Id, out var model)) return;
         if (CheckLocked(model)) return;

         model.EvaluationDate = EvaluationDate;
         if (!EvaluationTool.HasValue)
            model.EvaluationTool = null;
         else if (Models.EvaluationTool.TryGetModel(EvaluationTool.Value, out var evt))
            model.EvaluationTool = evt;
         model.EvaluationPoint = EvaluationPoint;
         model.Evaluation = Evaluation;
         model.DiagnosticsStr = Diagnostics;
         if (NoProcess.Value)
         {
            model.TreatmentsStr = "";
            while (model.Treatments.Count > 0)
               model.Treatments.Remove(model.Treatments[0]);
            model.ServicesStr = "";
            while (model.Services.Count > 0)
               model.Services.Remove(model.Services[0]);
         }
         else
         {
            model.TreatmentsStr = Treatments;
            UpdateTreatments(model);
            model.ServicesStr = Services;
            UpdateServices(model);
         }
         UpdateDiagnostics(model);
         Attachments.Update(model.EvaluationAttachments, "Medical", model.Oid.ToString());
         Doctors.Update(model.Doctors);
         if (model.DateEvaluation == null)
            model.DateEvaluation = DateTime.Now;
         if (!NoProcess.Value)
            model.Reason = null;
         else
            model.Reason = MedicalReason.GetModelByName(ReasonCancel);
         MedicalProcess.UpdateMedicalStatus(model, NoProcess.Value ? MedicalStatus.NotProcess : 0);
         db.Session.CommitChanges();
      }

      private void UpdateDiagnostics(DataModel.Medical model)
      {
         var ids = new List<int>();
         if (!string.IsNullOrEmpty(DiagnosticsValue))
            ids.AddRange(DiagnosticsValue.Split(';').Select(el => int.Parse(el)));

         //remove
         for (var i = 0; i < model.Diagnostics.Count;)
         {
            if (!ids.Contains(model.Diagnostics[i].Oid))
               model.Diagnostics.Remove(model.Diagnostics[i]);
            else
               i++;
         }
         //add
         foreach (var id in ids)
         {
            if (!model.Diagnostics.Any(el => el.Oid == id))
            {
               model.Diagnostics.Add(model.Session.GetObjectByKey<DataModel.Diagnostic>(id));
            }
         }
      }

      private void UpdateTreatments(DataModel.Medical model)
      {
         var ids = new List<int>();
         if (!string.IsNullOrEmpty(TreatmentsValue))
            ids.AddRange(TreatmentsValue.Split(';').Select(el => int.Parse(el)));

         //remove
         for (var i = 0; i < model.Treatments.Count;)
         {
            if (!ids.Contains(model.Treatments[i].Oid))
               model.Treatments.Remove(model.Treatments[i]);
            else
               i++;
         }
         //add
         foreach (var id in ids)
         {
            if (!model.Treatments.Any(el => el.Oid == id))
            {
               model.Treatments.Add(model.Session.GetObjectByKey<DataModel.Treatment>(id));
            }
         }
      }

      private void UpdateServices(DataModel.Medical model)
      {
         var ids = new List<int>();
         if (!string.IsNullOrEmpty(ServicesValue))
            ids.AddRange(ServicesValue.Split(';').Select(el => int.Parse(el)));

         //remove
         for (var i = 0; i < model.Services.Count;)
         {
            if (!ids.Contains(model.Services[i].Oid))
               model.Services.Remove(model.Services[i]);
            else
               i++;
         }
         //add
         foreach (var id in ids)
         {
            if (!model.Services.Any(el => el.Oid == id))
            {
               model.Services.Add(model.Session.GetObjectByKey<DataModel.Service>(id));
            }
         }
      }

      public bool CheckService() => !NoProcess.Value;

      public bool CheckReasonCancel() => NoProcess.Value;

      public static BeginProcess Load(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return null;
         return new BeginProcess(model);
      }

      public static bool CheckLocked(DataModel.Medical model)
      {
         if (MedicalProcess.CheckLocked(model)) return true;
         if (model.DateEvaluation != null && LockedData.CheckLocked(model.DateEvaluation.Value.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         if (model.Processes.Count > 0) return true;
         return false;
      }
   }

   public class MedicalPlan
   {
      public class Detail
      {
         public int Id { get; set; }
         public DateTime PlanDate { get; set; }
         public string PlanDateStr => PlanDate.ToShortDateString();
         public int? Doctor { get; set; }
         public string DoctorName { get; }
         public string Specialize { get; }
         public string Specifications { get; set; }
         public bool Deleted { get; set; }
         public DateTime? EndDate { get; set; }
         public string EndDateStr => EndDate?.ToShortDateString() ?? "";

         public Detail() { }


         public Detail(DataModel.MedicalPlan model)
         {
            Id = model.Oid;
            PlanDate = model.PlanDate;
            Doctor = model.Doctor.Oid;
            DoctorName = model.Doctor.Name;
            Specialize = model.Doctor.Specialize.Name;
            Specifications = model.Specifications;
            EndDate = model.EndDate;
         }
      }
      public int Id { get; set; }
      [Caption("Ngày lập kế hoạch"), Required]
      public DateTime? PlanDate { get; set; }
      [Caption("Mục tiêu can thiệp"), Required, Template("MultilineText5")]
      public string PlanGoal { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string PlanNote { get; set; }
      [Caption("Cán bộ lập kế hoạch"), Template("Doctors")]
      public Doctors Doctors { get; set; }

      public Attachments Attachments { get; set; }
      public List<Detail> Details { get; set; }
      public bool Locked { get; set; }
      public int Facility { get; }
      public List<int> Services { get; }
      public DateTime MinDate { get; }
      public DateTime MaxDate { get; }

      public MedicalPlan()
      {
         Attachments = new Attachments(nameof(MedicalPlan));
         Doctors = new Doctors(nameof(MedicalPlan));
         Details = new List<Detail>();
         MinDate = DateTime.Today;
         MaxDate = MinDate.AddDays(90);
      }

      public MedicalPlan(DataModel.Medical model)
      {
         Id = model.Oid;
         PlanDate = model.PlanDate;
         PlanGoal = model.PlanGoal;
         PlanNote = model.PlanNote;
         var ind = 0;
         Attachments = new Attachments(nameof(MedicalPlan), model.PlanAttachments,
            att => $"{att.DateCreate.Date.ToString("yyMMdd")}_{model.Patient.Code}_Kế hoạch_{model.Session.GetObjectByKey<WebApp.DataModel.User>(att.UserCreate).UserName}_{++ind}");
         Details = model.MedicalPlans.Select(it => new Detail(it)).OrderBy(it => it.PlanDate).ThenBy(it => it.DoctorName).ToList();
         Locked = CheckLocked(model);
         Facility = model.Facility.Oid;
         Services = model.Services.Select(it => it.Oid).ToList();
         MinDate = model.EvaluationDate ?? model.DateCreate.Date;
         MaxDate = model.CompletedDate ?? MinDate.AddDays(90);
         Doctors = new Doctors(nameof(MedicalPlan), Facility, model.PlanDoctors);
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         if (!Medical.TryGetModel(Id, out var model)) return;
         if (CheckLocked(model)) return;

         model.PlanDate = PlanDate;
         model.PlanGoal = PlanGoal;
         model.PlanNote = PlanNote;
         Attachments.Update(model.PlanAttachments, "Medical", model.Oid.ToString());
         Doctors.Update(model.PlanDoctors);
         //update details
         foreach (var plan in Details)
         {
            if (plan.Deleted)
            {
               model.MedicalPlans.Remove(e => e.Oid == plan.Id);
            }
            else
            {
               var p = model.MedicalPlans.FirstOrDefault(e => e.Oid == plan.Id);
               if (p == null)
               {
                  p = new DataModel.MedicalPlan(model.Session);
                  p.Medical = model;
                  model.MedicalPlans.Add(p);
               }
               if (Models.Doctor.TryGetModel(plan.Doctor.Value, out var doctor))
                  p.Doctor = doctor;
               p.PlanDate = plan.PlanDate;
               p.EndDate = plan.EndDate;
               p.Specifications = plan.Specifications;
            }
         }

         db.Session.CommitChanges();
      }

      public object GetDoctors()
      {
         var query = from d in ApplicationDbContext.Current.Session.QueryDoctor()
                     where d.Facility.Oid == Facility
                     select new { d.Oid, d.Name, Specialize = d.Specialize.Name };
         return query.ToList();
      }

      public List<string> GetSpecifications()
      {
         var query = from el in ApplicationDbContext.Current.Session.QueryByCompany<DataModel.Specification>()
                     where Services.Contains(el.Service.Oid)
                     orderby el.Service.OrderIndex, el.Service.Name, el.OrderIndex, el.Name
                     select el.Name;
         return query.Distinct().ToList();
      }

      public static MedicalPlan Load(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return null;
         return new MedicalPlan(model);
      }

      public static bool CheckLocked(DataModel.Medical model)
      {
         if (MedicalProcess.CheckLocked(model)) return true;
         if (model.DateEvaluation != null && LockedData.CheckLocked(model.DateEvaluation.Value.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         if (model.Processes.Count > 0) return true;
         return false;
      }
   }

   public class DetailProcess
   {
      public int Id { get; set; }
      [Required]
      public DateTime ProcessDate { get; set; }
      [Required]
      public int? Status { get; set; }
      public string StatusName { get; }
      [Required(OnlyIf = "CheckProcessReason")]
      public string Reason { get; set; }
      [Required]
      public int? Service { get; set; }
      public string ServiceName { get; }
      public Attachments Attachments { get; set; }
      public bool Locked { get; set; }
      public DateTime MinDate { get; set; }
      public DetailProcess()
      {
         Attachments = new Attachments(nameof(DetailProcess) + Id);
         Doctors = new Doctors(nameof(DetailProcess));
      }
      [Caption("Cán bộ y tế thực hiện can thiệp"), Template("Doctors")]
      public Doctors Doctors { get; set; }

      public DetailProcess(DataModel.MedicalProcess model)
      {
         Id = model.Oid;
         ProcessDate = model.ProcessDate;
         Status = model.Status?.Oid;
         StatusName = model.Status?.Name;
         Reason = model.Reason;
         Service = model.Service?.Oid;
         ServiceName = model.Service?.Name;
         var ind = 0;
         Attachments = new Attachments(nameof(DetailProcess) + Id, model.Attachments,
            att => $"{model.ProcessDate.ToString("yyMMdd")}_{model.Medical.Patient.Code}_Tại nhà_{model.ProcessInd}_{model.Session.GetObjectByKey<WebApp.DataModel.User>(att.UserCreate).UserName}_{++ind}");
         Locked = CheckLocked(model);
         MinDate = model.Medical.EvaluationDate ?? model.Medical.DateCreate.Date;
         var arr = model.Medical.Processes.Where(it => it.Oid != model.Oid && it.ProcessDate <= model.ProcessDate).ToList();
         if (arr.Count > 0)
            MinDate = arr.Max(it => it.AtFacility ? it.DateOut ?? it.DateIn : it.ProcessDate);
         Doctors = new Doctors(nameof(DetailProcess) + Id, model.Medical.Facility.Oid, model.Doctors);
      }

      public void Save()
      {
         if (!TryGetModel(Id, out var model)) return;
         if (CheckLocked(model)) return;

         model.ProcessDate = ProcessDate;
         if (Service == null)
            model.Service = null;
         else if (Models.Service.TryGetModel(Service.Value, out var sv))
            model.Service = sv;
         if (Status == null)
            model.Status = null;
         else if (ProcessStatus.TryGetModel(Status.Value, out var st))
            model.Status = st;
         model.Reason = Reason;
         model.ValidData = true;
         Attachments.Update(model.Attachments, "Medical", model.Medical.Oid.ToString());
         Doctors.Update(model.Doctors);
         MedicalProcess.UpdateMedicalStatus(model.Medical);
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public bool CheckProcessReason()
      {
         return Status == 4;
      }

      public static DetailProcess Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new DetailProcess(model);
      }

      public static Tuple<int, int> Create(int id)
      {
         if (!Medical.TryGetModel(id, out var medical)) return Tuple.Create(-1, 0);//not found
         if (MedicalProcess.CheckLocked(medical)) return Tuple.Create(-2, 0);//locked
         if (!MedicalProcess.CheckValidData(medical)) return Tuple.Create(-3, 0);//not evaluation/comple prev process yet
         var db = ApplicationDbContext.Current;
         var model = new DataModel.MedicalProcess(db.Session)
         {
            Medical = medical,
            ProcessDate = DateTime.Today,
         };
         model.ProcessInd = medical.Processes.Where(it => !it.AtFacility).Count();
         db.Session.CommitChanges();
         return Tuple.Create(model.Oid, model.ProcessInd);
      }

      public static bool TryGetModel(int id, out DataModel.MedicalProcess model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchByCompany<DataModel.MedicalProcess>(id);
         if (model == null) return false;
         if (!Medical.TryGetModel(model.Medical.Oid, out var _)) return false;
         return true;
      }

      public static bool CheckLocked(DataModel.MedicalProcess model)
      {
         if (MedicalProcess.CheckLocked(model.Medical)) return true;
         if (LockedData.CheckLocked(model.DateCreate.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         if (model.Medical.CompletedDate != null) return true;
         var ids = model.Medical.Processes.OrderBy(it => it.ProcessDate).Select(it => it.Oid).ToList();
         if (ids.IndexOf(model.Oid) < ids.Count - 1) return true;
         return false;
      }
   }

   public class FacilityProcess
   {
      public int Id { get; set; }
      [Required]
      public DateTime DateIn { get; set; }
      public DateTime? DateOut { get; set; }
      [Required]
      public int? Status { get; set; }
      public string StatusName { get; }
      [Required(OnlyIf = "CheckProcessReason")]
      public string Reason { get; set; }
      [Required]
      public int? Service { get; set; }
      public string ServiceName { get; }
      public Attachments Attachments { get; set; }
      public bool Locked { get; set; }
      public DateTime MinDate { get; set; }
      public FacilityProcess()
      {
         Attachments = new Attachments(nameof(FacilityProcess) + Id);
         Doctors = new Doctors(nameof(FacilityProcess));
      }
      [Caption("Cán bộ y tế thực hiện can thiệp"), Template("Doctors")]
      public Doctors Doctors { get; set; }

      public FacilityProcess(DataModel.MedicalProcess model)
      {
         Id = model.Oid;
         DateIn = model.DateIn;
         DateOut = model.DateOut;
         Status = model.Status?.Oid;
         StatusName = model.Status?.Name;
         Reason = model.Reason;
         Service = model.Service?.Oid;
         ServiceName = model.Service?.Name;
         var ind = 0;
         Attachments = new Attachments(nameof(FacilityProcess) + Id, model.Attachments,
            att => $"{model.DateIn.ToString("yyMMdd")}_{model.Medical.Patient.Code}_Tại viện_{model.ProcessInd}_{model.Session.GetObjectByKey<WebApp.DataModel.User>(att.UserCreate).UserName}_{++ind}");
         Locked = CheckLocked(model);
         MinDate = model.Medical.EvaluationDate ?? model.Medical.DateCreate.Date;
         var arr = model.Medical.Processes.Where(it => it.Oid != model.Oid && it.ProcessDate <= model.ProcessDate).ToList();
         if (arr.Count > 0)
            MinDate = arr.Max(it => it.AtFacility ? it.DateOut ?? it.DateIn : it.ProcessDate);
         Doctors = new Doctors(nameof(FacilityProcess) + Id, model.Medical.Facility.Oid, model.Doctors);
      }

      public void Save()
      {
         if (!TryGetModel(Id, out var model)) return;
         if (CheckLocked(model)) return;

         model.DateIn = DateIn;
         model.DateOut = DateOut;
         model.ProcessDate = model.DateIn;
         if (Service == null)
            model.Service = null;
         else if (Models.Service.TryGetModel(Service.Value, out var sv))
            model.Service = sv;
         if (Status == null)
            model.Status = null;
         else if (ProcessStatus.TryGetModel(Status.Value, out var st))
            model.Status = st;
         model.Reason = Reason;
         model.ValidData = true;
         Attachments.Update(model.Attachments, "Medical", model.Medical.Oid.ToString());
         Doctors.Update(model.Doctors);
         MedicalProcess.UpdateMedicalStatus(model.Medical);
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public bool CheckProcessReason()
      {
         return Status == 4;
      }

      public static FacilityProcess Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new FacilityProcess(model);
      }

      public static Tuple<int, int> Create(int id)
      {
         if (!Medical.TryGetModel(id, out var medical)) return Tuple.Create(-1, 0);//not found
         if (MedicalProcess.CheckLocked(medical)) return Tuple.Create(-2, 0);//locked
         if (!MedicalProcess.CheckValidData(medical)) return Tuple.Create(-3, 0);//not evaluation/comple prev process yet
         var db = ApplicationDbContext.Current;
         var model = new DataModel.MedicalProcess(db.Session)
         {
            Medical = medical,
            DateIn = DateTime.Today,
            AtFacility = true
         };
         model.ProcessInd = medical.Processes.Where(it => it.AtFacility).Count();
         model.ProcessDate = model.DateIn;
         db.Session.CommitChanges();
         return Tuple.Create(model.Oid, model.ProcessInd);
      }

      public static bool TryGetModel(int id, out DataModel.MedicalProcess model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.FetchByCompany<DataModel.MedicalProcess>(id);
         if (model == null) return false;
         if (!Medical.TryGetModel(model.Medical.Oid, out var _)) return false;
         return true;
      }

      public static bool CheckLocked(DataModel.MedicalProcess model)
      {
         if (MedicalProcess.CheckLocked(model.Medical)) return true;
         if (LockedData.CheckLocked(model.DateCreate.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         if (model.Medical.CompletedDate != null) return true;
         var ids = model.Medical.Processes.OrderBy(it => it.ProcessDate).Select(it => it.Oid).ToList();
         if (ids.IndexOf(model.Oid) < ids.Count - 1) return true;
         return false;
      }
   }

   public class EndProcess
   {
      public int Id { get; set; }
      [Caption("Ngày dừng can thiệp"), Required]
      public DateTime? CompletedDate { get; set; }
      [Caption("Công cụ đánh giá"), Template("EvaluationTool"), Required]
      public int? EndEvaluationTool { get; set; }
      [Caption("Công cụ đánh giá")]
      public string EndEvaluationToolName { get; set; }
      [Caption("Tổng điểm")]
      public string EndEvaluationPoint { get; set; }
      [Required]
      public bool? EndSuccess { get; set; }
      [Caption("Kết quả chi tiết"), Required]
      public string EndEvaluation { get; set; }
      public Attachments Attachments { get; set; }
      public bool Locked { get; set; }
      public DateTime MinDate { get; }
      [Caption("Cán bộ y tế lượng giá và đóng ca"), Template("Doctors")]
      public Doctors Doctors { get; set; }
      public EndProcess()
      {
         Attachments = new Attachments(nameof(EndProcess));
         Doctors = new Doctors("EndDoctors");
      }
      public EndProcess(DataModel.Medical model)
      {
         Id = model.Oid;
         CompletedDate = model.CompletedDate;
         var ind = 0;
         Attachments = new Attachments(nameof(EndProcess), model.CompletedAttachments,
            att => $"{model.CompletedDate.Value.ToString("yyMMdd")}_{model.Patient.Code}_Kết thúc_{model.Session.GetObjectByKey<WebApp.DataModel.User>(att.UserCreate).UserName}_{++ind}");
         Locked = CheckLocked(model);
         MinDate = model.EvaluationDate ?? model.DateCreate.Date;
         if (model.Processes.Count > 0)
            MinDate = model.Processes.Max(it => it.AtFacility ? it.DateOut ?? it.DateIn : it.ProcessDate);
         EndEvaluation = model.EndEvaluation;
         EndEvaluationPoint = model.EndEvaluationPoint;
         EndEvaluationTool = model.EndEvaluationTool?.Oid;
         EndEvaluationToolName = model.EndEvaluationTool?.Name ?? "";
         EndSuccess = model.EndSuccess;
         Doctors = new Doctors("EndDoctors", model.Facility.Oid, model.EndDoctors);
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         if (!Medical.TryGetModel(Id, out var model)) return;
         if (CheckLocked(model)) return;

         model.CompletedDate = CompletedDate;
         model.Cancel = null;
         model.EndEvaluation = EndEvaluation;
         model.EndEvaluationPoint = EndEvaluationPoint;
         if (EndEvaluationTool == null)
            model.EndEvaluationTool = null;
         else if (EvaluationTool.TryGetModel(EndEvaluationTool.Value, out var tool))
            model.EndEvaluationTool = tool;
         model.EndSuccess = EndSuccess;
         Attachments.Update(model.CompletedAttachments, "Medical", model.Oid.ToString());
         Doctors.Update(model.EndDoctors);
         MedicalProcess.UpdateMedicalStatus(model);
         db.Session.CommitChanges();
      }

      public static EndProcess Load(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return null;
         return new EndProcess(model);
      }

      public static bool CheckLocked(DataModel.Medical model)
      {
         if (MedicalProcess.CheckLocked(model)) return true;
         if (model.DateEnd != null && LockedData.CheckLocked(model.DateEnd.Value.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         return false;
      }

      public static int Create(int id)
      {
         var db = ApplicationDbContext.Current;
         if (!Medical.TryGetModel(id, out var model)) return 1; //not found
         if (CheckLocked(model)) return 2;//locked
         if (!MedicalProcess.CheckValidData(model)) return 3;//not completed data
         if (model.CompletedDate == null)
         {
            model.CompletedDate = DateTime.Today;
            model.DateEnd = DateTime.Now;
            db.Session.CommitChanges();
         }
         return 0;
      }
   }

   public class StopProcess
   {
      public int Id { get; set; }
      [Caption("Ngày dừng can thiệp"), Required]
      public DateTime? CompletedDate { get; set; }
      [Caption("Lý do dừng can thiệp"), Template("MedicalCancel"), Required]
      public int? ProcessCancel { get; set; }
      [Caption("Lý do dừng can thiệp")]
      public string ProcessCancelName { get; }
      public bool Locked { get; set; }
      public DateTime MinDate { get; }
      [Caption("Cán bộ y tế xác nhận dừng can thiệp"), Template("Doctor"), Required]
      public int? Doctor { get; set; }
      [Caption("Cán bộ y tế xác nhận dừng can thiệp")]
      public string DoctorName { get; set; }
      public List<Doctors.Doctor> Doctors { get; }
      public StopProcess() { }
      public StopProcess(DataModel.Medical model)
      {
         Id = model.Oid;
         CompletedDate = model.CompletedDate;
         ProcessCancel = model.Cancel?.Oid;
         ProcessCancelName = model.Cancel?.Name ?? "";
         Locked = CheckLocked(model);
         MinDate = model.EvaluationDate ?? model.DateCreate.Date;
         if (model.Processes.Count > 0)
            MinDate = model.Processes.Max(it => it.AtFacility ? it.DateOut ?? it.DateIn : it.ProcessDate);
         var d = model.EndDoctors.FirstOrDefault();
         Doctor = d?.Oid;
         DoctorName = d?.Name;
         Doctors = Models.Doctors.GetListByFacility(model.Facility.Oid);
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         if (!Medical.TryGetModel(Id, out var model)) return;
         if (CheckLocked(model)) return;

         model.CompletedDate = CompletedDate;
         if (ProcessCancel == null)
            model.Cancel = null;
         else if (MedicalCancel.TryGetModel(ProcessCancel.Value, out var cancel))
            model.Cancel = cancel;
         var d = model.EndDoctors.FirstOrDefault();
         if ((d == null || d.Oid != Doctor.Value) && Models.Doctor.TryGetModel(Doctor.Value, out var doctor))
         {
            while (model.EndDoctors.Count > 0) model.EndDoctors.Remove(model.EndDoctors[0]);
            model.EndDoctors.Add(doctor);
         }
         MedicalProcess.UpdateMedicalStatus(model);
         db.Session.CommitChanges();
      }

      public static StopProcess Load(int id)
      {
         if (!Medical.TryGetModel(id, out var model)) return null;
         return new StopProcess(model);
      }

      public static bool CheckLocked(DataModel.Medical model)
      {
         if (MedicalProcess.CheckLocked(model)) return true;
         if (model.DateEnd != null && LockedData.CheckLocked(model.DateEnd.Value.Date)) return true;
         if (MedicalProcess.UnlockProcess()) return false;
         return false;
      }

      public static int Create(int id)
      {
         var db = ApplicationDbContext.Current;
         if (!Medical.TryGetModel(id, out var model)) return 1; //not found
         if (CheckLocked(model)) return 2;//locked
         if (!MedicalProcess.CheckValidData(model)) return 3;//not completed data
         if (model.CompletedDate == null)
         {
            model.CompletedDate = DateTime.Today;
            model.DateEnd = DateTime.Now;
            if (MedicalCancel.TryGetModel(4, out var def))
               model.Cancel = def;
            db.Session.CommitChanges();
         }
         return 0;
      }
   }
}