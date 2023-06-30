using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Khóa đào tạo")]
   public class ProgramController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Program.ListInfo());
      }

      public ActionResult IndexPartial(Models.Program.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Thêm")]
      public ActionResult Add()
      {
         var vm = Models.Program.Create();
         Session[vm.DoctorsId] = vm.Doctors;
         return PartialView("EditTemplate", vm);
      }

      [Logic("Thêm"), HttpPost]
      public ActionResult Add(Models.Program vm)
      {
         if (vm.ProgramType == Models.ProgramTypeEnum.Coaching)
            vm.TrainingTime = T("{0} giờ", vm.TrainingHour);

         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         vm.Doctors = (List<Models.Program.DetailDoctor>)Session[vm.DoctorsId];
         UpdateList(vm);
         vm.Save();
         return Nothing();
      }

      [Logic("Sửa")]
      public ActionResult Edit(int id)
      {
         var vm = Models.Program.Load(id);
         if (vm == null) return NotFound();
         if (vm.Locked) return JavaScript("WebApp.function.view();");
         Session[vm.DoctorsId] = vm.Doctors;
         return PartialView("EditTemplate", vm);
      }

      [Logic("Sửa"), HttpPost]
      public ActionResult Edit(Models.Program vm)
      {
         if (vm.ProgramType == Models.ProgramTypeEnum.Coaching)
            vm.TrainingTime = T("{0} giờ", vm.TrainingHour);

         if (!ModelState.IsValid)
            return PartialView("EditTemplate", vm);
         UpdateList(vm);
         vm.Save();
         return Nothing();
      }

      private void UpdateList(Models.Program vm)
      {
         //doctors
         vm.Doctors = (List<Models.Program.DetailDoctor>)Session[vm.DoctorsId];
         //attachments
         var atts = Request.Form["AttachmentsUpdated"];
         if (!string.IsNullOrEmpty(atts))
         {
            vm.Attachments.Items = System.Web.Helpers.Json.Decode<List<Models.Attachment>>(atts);
         }
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         var s = Models.Program.Delete(id);
         if (!string.IsNullOrEmpty(s)) return ShowInfo(T("Lỗi"), T(s));
         return Nothing();
      }

      public ActionResult ViewInfo(int id)
      {
         var vm = Models.Program.ViewInfo.Load(id);
         if (vm == null) return NotFound();
         Session[vm.DoctorsId] = vm.Doctors;
         return PartialView(vm);
      }

      public ActionResult DoctorsViewInfo(string DoctorsId)
      {
         var vm = new Models.Program.ViewInfo(DoctorsId, (List<Models.Program.DetailDoctor>)Session[DoctorsId]);
         return PartialView(vm);
      }

      public ActionResult ProgramDoctors(string DoctorsId)
      {
         return PartialView(new Models.Program() { DoctorsId = DoctorsId, Doctors = (List<Models.Program.DetailDoctor>)Session[DoctorsId] });
      }

      [HttpGet]
      public ActionResult SelectDoctors(string DoctorsId)
      {
         var vm = new Models.Program.SelectDoctor() { DoctorsId = DoctorsId };
         vm.LoadList();
         return PartialView(vm);
      }

      public ActionResult SelectDoctorsPartial(Models.Program.SelectDoctor vm)
      {
         vm.LoadList();
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult SelectDoctors(Models.Program.SelectDoctor vm)
      {
         vm.Doctors = new Models.Doctors("Doctors")
         {
            Items = DevExpress.Web.Mvc.ListBoxExtension.GetSelectedValues<int>("DoctorsDoctors")
               .Select(id => new Models.Doctors.Doctor() { Id = id }).ToList()
         };
         vm.Update();
         return Nothing();
      }
   }
}