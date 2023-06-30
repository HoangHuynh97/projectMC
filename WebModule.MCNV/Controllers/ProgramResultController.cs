using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.Core;
using WebModule.MCNV.Models;

namespace WebModule.MCNV.Controllers
{
   [Function("Kết quả đào tạo")]
   public class ProgramResultController : BaseController
   {
      const string LASTKEY = "LastProgram";
      public ActionResult Index()
      {
         ViewBag.ProgramId = Session[LASTKEY];
         return PartialView();
      }

      public ActionResult IndexPartial(int program)
      {
         Session[LASTKEY] = program;
         var vm = Models.ProgramResult.Load(program);
         if (vm == null) return NotFound();
         return PartialView(vm);
      }

      public ActionResult Doctors(int program)
      {
         var vm = Models.ProgramResult.Load(program);
         if (vm == null) return NotFound();
         return PartialView(vm);
      }

      [HttpPost]
      public ActionResult Save(Models.ProgramResult vm)
      {
         if (!ModelState.IsValid)
            return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
         vm.Save();
         return Nothing();
      }

      public ActionResult Export(int program)
      {
         var vm = Models.Program.Load(program);
         if (vm == null) return NotFound();
         var st = new GridViewSettings()
         {
            Name = "KetQuaDaoTao",
            KeyFieldName = "Oid",
         };
         st.SettingsBehavior.AutoExpandAllGroups = true;

         st.Columns.Add("Area", T("Mã vùng"));
         st.Columns.Add("Code", T("Mã định danh"));
         st.Columns.Add("DoctorName", T("Họ và tên"));
         st.Columns.Add("Gender", T("Giới tính"));
         st.Columns.Add("DateBirth", T("Năm sinh"));
         st.Columns.Add("FacilityName", T("Đơn vị công tác"));
         st.Columns.Add("Province", T("Tỉnh/Thành phố"));
         st.Columns.Add("FacilityType", T("Loại hình"));
         st.Columns.Add("Position", T("Vị trí công tác"));
         st.Columns.Add("Qualification", T("Trình độ chuyên môn"));
         st.Columns.Add("Specialize", T("Lĩnh vực chuyên môn"));
         st.Columns.Add("Title", T("Chức danh tham gia"));
         st.Columns.Add("TrainingType", T("Hệ đào tạo"));
         if (vm.ProgramType == Models.ProgramTypeEnum.Coaching)
         {
            st.Columns.Add("Score1", T("Tháng thứ 1"));
            st.Columns.Add("Score2", T("Tháng thứ 2"));
            st.Columns.Add("Score3", T("Tháng thứ 3"));
            st.Columns.Add("Score4", T("Tháng thứ 4"));
            st.Columns.Add("Score5", T("Tháng thứ 5"));
            st.Columns.Add("Score6", T("Tháng thứ 6"));
            st.Columns.Add("Score7", T("Tháng thứ 7"));
            st.Columns.Add("Score8", T("Tháng thứ 8"));
            st.Columns.Add("Score9", T("Tháng thứ 9"));
            st.Columns.Add("Score10", T("Tháng thứ 10"));
            st.Columns.Add("Score11", T("Tháng thứ 11"));
            st.Columns.Add("Score12", T("Tháng thứ 12"));

            st.Columns.Add("Score13", T("GAS lần 1"));
            st.Columns.Add("Score14", T("GAS lần 2"));
            st.Columns.Add("Score15", T("GAS lần 3"));
            st.Columns.Add("Score16", T("GAS lần 4"));

            st.Columns.Add("HoursTotal", T("Số giờ đã học"));
            st.Columns.Add("HoursLeft", T("Số giờ còn lại"));
         }
         else if (vm.ProgramType == ProgramTypeEnum.ShortTime || vm.ProgramType == ProgramTypeEnum.LongTimeContinuous || vm.ProgramType == ProgramTypeEnum.LongTimeIntensive)
         {
            st.Columns.Add("Score1", T("Điểm đầu vào"));
            st.Columns.Add("Score2", T("Điểm đầu ra"));
            st.Columns.Add("Score3", T("% Cải thiện"));
         }
         else if (vm.ProgramType == ProgramTypeEnum.LongTimeLongTerm || vm.ProgramType == ProgramTypeEnum.LongTimeMidTerm)
         {
            var lt = st.Columns.AddBand(T("Điểm lý thuyết"));
            lt.Columns.Add("Score1", T("Học kỳ 1"));
            lt.Columns.Add("Score2", T("Học kỳ 2"));
            lt.Columns.Add("Score3", T("Học kỳ 3"));
            lt.Columns.Add("Score4", T("Điểm cuối khóa"));
            lt.Columns.Add("Score5", T("Điểm tổng kết"));

            var th = st.Columns.AddBand(T("Điểm thực hành"));
            th.Columns.Add("Score6", T("Học kỳ 1"));
            th.Columns.Add("Score7", T("Học kỳ 2"));
            th.Columns.Add("Score8", T("Học kỳ 3"));
            th.Columns.Add("Score9", T("Điểm cuối khóa"));
            th.Columns.Add("Score10", T("Điểm tổng kết"));
         }
         st.Columns.Add(col =>
         {
            col.FieldName = "Status";
            col.Caption = T("Đánh giá cuối kỳ");
            col.EditorProperties().ComboBox(cmb =>
            {
               cmb.ValueField = "Oid";
               cmb.ValueType = typeof(int);
               cmb.TextField = "Name";
               cmb.BindList(TrainingStatus.ComboboxData());
            });
         });
         st.Columns.Add("ReasonCancel", T("Lý do nghỉ học"));
         st.Columns.Add("StatusType", T("Tình trạng"));

         return GridViewExtension.ExportToXlsx(st, ProgramResult.Export(program));
      }
   }
}