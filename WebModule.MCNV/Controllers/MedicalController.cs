using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Hồ sơ PHCN")]
   [Logic("Mở khóa quy trình")]
   public class MedicalController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Medical.ListInfo());
      }

      public ActionResult IndexPartial(Models.Medical.ListInfo vm)
      {
         vm.AdvancedFilter = !Request.Form.AllKeys.Contains("showFilter");
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         return PartialView("Process", Models.MedicalProcess.Create());
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.MedicalProcess.Load(id);
         if (vm == null) return NotFound();
         return PartialView("Process", vm);
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         var s = Models.Medical.Delete(id);
         if (!string.IsNullOrEmpty(s)) return ShowInfo(T("Lỗi"), T(s));
         return Nothing();
      }

      [HttpPost]
      public ActionResult ProcessData(int id)
      {
         var vm = Models.MedicalProcess.Load(id);
         if (vm == null) return NotFound();
         return Json(vm);
      }

      [HttpGet]
      public ActionResult InfoProcess(int? id)
      {
         if (id == null) return PartialView(Models.InfoProcess.Create());
         var vm = Models.InfoProcess.Load(id.Value);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("InfoProcessLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult InfoProcess(Models.InfoProcess vm)
      {
         if (!Models.Medical.CheckCode(vm.Code, vm.Id))
            return ShowInfo(T("Lỗi"), T("Mã bệnh án này đã nhập rồi"));
         if (ModelState.IsValid)
         {
            vm.Save();
            return Json(vm.Id);
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpGet]
      public ActionResult BeginProcess(int id)
      {
         var vm = Models.BeginProcess.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("BeginProcessLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult BeginProcess(Models.BeginProcess vm)
      {
         if (vm.Doctors.Items.Count == 0)
            return ShowInfo(T("Lỗi"), T("Vui lòng chọn cán bộ"));
         if (ModelState.IsValid)
         {
            vm.Save();
            return Nothing();
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpGet]      
      public ActionResult Plan(int id)
      {
         var vm = Models.MedicalPlan.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("PlanLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult Plan(Models.MedicalPlan vm)
      {
         if (vm.Doctors.Items.Count == 0)
            return ShowInfo(T("Lỗi"), T("Vui lòng chọn cán bộ"));

         if (ModelState.IsValid)
         {
            vm.Save();
            return Nothing();
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpPost]
      public ActionResult AddProcess(int id)
      {
         var vm = Models.DetailProcess.Create(id);
         if (vm.Item1 == -1) return NotFound();
         if (vm.Item1 == -2) return ShowInfo(T("Lỗi"), T("Dữ liệu đã bị khóa"));
         if (vm.Item1 == -3) return ShowInfo(T("Lỗi"), T("Dữ liệu chưa hoàn tất để thêm can thiệp"));
         return Json(new Models.MedicalProcess.Process()
         {
            Id = vm.Item1,
            Url = $"/Medical/DetailProcess/{vm.Item1}",
            Name = Language.T("Can thiệp tại nhà lần {0} ngày {1}", vm.Item2, DateTime.Today.ToShortDateString())
         });
      }

      [HttpGet]
      public ActionResult DetailProcess(int id)
      {
         var vm = Models.DetailProcess.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("DetailProcessLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult DetailProcess(Models.DetailProcess vm)
      {
         if (vm.Doctors.Items.Count == 0)
            return ShowInfo(T("Lỗi"), T("Vui lòng chọn cán bộ"));
         if (ModelState.IsValid)
         {
            vm.Save();
            return Nothing();
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpPost]
      public ActionResult DeleteDetailProcess(int id)
      {
         Models.MedicalProcess.DeleteDetailProcess(id);
         return Nothing();
      }

      [HttpPost]
      public ActionResult AddFacilityProcess(int id)
      {
         var vm = Models.FacilityProcess.Create(id);
         if (vm.Item1 == -1) return NotFound();
         if (vm.Item1 == -2) return ShowInfo(T("Lỗi"), T("Dữ liệu đã bị khóa"));
         if (vm.Item1 == -3) return ShowInfo(T("Lỗi"), T("Dữ liệu chưa hoàn tất để thêm can thiệp"));
         return Json(new Models.MedicalProcess.Process()
         {
            Id = vm.Item1,
            Url = $"/Medical/FacilityProcess/{vm.Item1}",
            Name = Language.T("Can thiệp tại viện lần {0} ngày {1}", vm.Item2, DateTime.Today.ToShortDateString())
         });
      }

      [HttpGet]
      public ActionResult FacilityProcess(int id)
      {
         var vm = Models.FacilityProcess.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("FacilityProcessLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult FacilityProcess(Models.FacilityProcess vm)
      {
         if (vm.Doctors.Items.Count == 0)
            return ShowInfo(T("Lỗi"), T("Vui lòng chọn cán bộ"));
         if (ModelState.IsValid)
         {
            vm.Save();
            return Nothing();
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpPost]
      public ActionResult DeleteFacilityProcess(int id)
      {
         Models.MedicalProcess.DeleteFacilityProcess(id);
         return Nothing();
      }

      [HttpPost]
      public ActionResult AddEnd(int id)
      {
         var code = Models.EndProcess.Create(id);
         if (code == 1) return NotFound();
         if (code == 2) return ShowInfo(T("Lỗi"), T("Dữ liệu đã bị khóa"));
         if (code == 3) return ShowInfo(T("Lỗi"), T("Dữ liệu chưa hoàn tất để kết thúc"));
         return Json(new Models.MedicalProcess.Process()
         {
            Id = -3,
            Name = T("Kết thúc can thiệp"),
            Url = $"/Medical/EndProcess/{id}"
         });
      }

      [HttpGet]
      public ActionResult EndProcess(int id)
      {
         var vm = Models.EndProcess.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("EndProcessLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult EndProcess(Models.EndProcess vm)
      {
         if (vm.Doctors.Items.Count == 0)
            return ShowInfo(T("Lỗi"), T("Vui lòng chọn cán bộ"));

         if (ModelState.IsValid)
         {
            vm.Save();
            return Nothing();
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpPost]
      public ActionResult DeleteEndProcess(int id)
      {
         Models.MedicalProcess.DeleteEndProcess(id);
         return Nothing();
      }

      [HttpPost]
      public ActionResult AddStop(int id)
      {
         var code = Models.StopProcess.Create(id);
         if (code == 1) return NotFound();
         if (code == 2) return ShowInfo(T("Lỗi"), T("Dữ liệu đã bị khóa"));
         if (code == 3) return ShowInfo(T("Lỗi"), T("Dữ liệu chưa hoàn tất để kết thúc"));
         return Json(new Models.MedicalProcess.Process()
         {
            Id = -4,
            Name = T("Dừng can thiệp"),
            Url = $"/Medical/StopProcess/{id}"
         });
      }

      [HttpGet]
      public ActionResult StopProcess(int id)
      {
         var vm = Models.StopProcess.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return PartialView("StopProcessLocked", vm);
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult StopProcess(Models.StopProcess vm)
      {
         if (ModelState.IsValid)
         {
            vm.Save();
            return Nothing();
         }
         return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
      }

      [HttpPost]
      public ActionResult DeleteStopProcess(int id)
      {
         Models.MedicalProcess.DeleteStopProcess(id);
         return Nothing();
      }

      [HttpPost]
      public ActionResult CheckCode(string code, int id)
      {
         return Json(Models.Medical.CheckCode(code, id));
      }

      [HttpPost]
      public ActionResult RequestPatientData(int id)
      {
         Models.SharedData.AddRequest(id);
         return Nothing();
      }

      [HttpPost]
      public ActionResult CheckPatientData(int id)
      {
         return Json(Models.Medical.CheckPatientData(id));
      }
   }
}