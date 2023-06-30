﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.WebPages;
    using DevExpress.Utils;
    using DevExpress.Web;
    using DevExpress.Web.ASPxThemes;
    using DevExpress.Web.Mvc;
    using DevExpress.Web.Mvc.UI;
    using WebModule.MCNV.Models;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Program/EditTemplate.cshtml")]
    public partial class _Views_Program_EditTemplate_cshtml : WebApp.Core.BaseWebViewPage<Program>
    {
        public _Views_Program_EditTemplate_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"dxss_programedit\"");

WriteLiteral(">\r\n   function onProgramDoctorsClick() {\r\n      WebApp.showDialog({\r\n         tit" +
"le: \'");

            
            #line 6 "..\..\Views\Program\EditTemplate.cshtml"
            Write(H("Chọn cán bộ tham gia"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n         url: \'");

            
            #line 7 "..\..\Views\Program\EditTemplate.cshtml"
          Write(Url.Action("SelectDoctors"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n         data: { DoctorsId: \'");

            
            #line 8 "..\..\Views\Program\EditTemplate.cshtml"
                        Write(Model.DoctorsId);

            
            #line default
            #line hidden
WriteLiteral(@"' },
         icon: 'people_usergroup_32x32',
         width: 500,
      }).done(function () {
         grdDoctors.PerformCallback();
      });
   }
   function UpdateUITraining() {
      var b = CheckTrainingHour();
      $('.inp-trainingtime').toggle(!b);
      $('.inp-traininghour').toggle(b);
   }

   window.CheckTrainingHour = function () {
      return ProgramType.GetValue() == ");

            
            #line 22 "..\..\Views\Program\EditTemplate.cshtml"
                                   Write((int)ProgramTypeEnum.Coaching);

            
            #line default
            #line hidden
WriteLiteral(@";
   }

   window.CheckTrainingTime = function () {
      return !window.CheckTrainingHour();
   }

   $(function () {
      ProgramName.Focus();
      DateStart.ValueChanged.AddHandler(function (s) {
         var d = s.GetValue();
         if (d && DateEnd.GetValue() < d) DateEnd.SetValue(null);
         DateEnd.SetMinDate(d);
      });
      UpdateUITraining();
   });
</script>

");

            
            #line 40 "..\..\Views\Program\EditTemplate.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 41 "..\..\Views\Program\EditTemplate.cshtml"
Write(Html.HiddenFor(m => m.DoctorsId));

            
            #line default
            #line hidden
WriteLiteral("\r\n<input");

WriteLiteral(" id=\"ReasonCancelUpdated\"");

WriteLiteral(" name=\"ReasonCancelUpdated\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" />\r\n<input");

WriteLiteral(" id=\"AttachmentsUpdated\"");

WriteLiteral(" name=\"AttachmentsUpdated\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" />\r\n<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 46 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.EditorFor(m => m.ProgramName));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 49 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.EditorFor(m => m.NameEnglish));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-3_sm-6_xs-12\"");

WriteLiteral(">");

            
            #line 51 "..\..\Views\Program\EditTemplate.cshtml"
                            Write(Html.EditorFor(m => m.Project));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_sm-6_xs-12\"");

WriteLiteral(">");

            
            #line 52 "..\..\Views\Program\EditTemplate.cshtml"
                            Write(Html.EditorFor(m => m.ProgramSpecialize));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_sm-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 54 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().ComboBox(st =>
 {
    st.Name = "ProgramType";
    st.Width = Unit.Percentage(100);
    st.Properties.Caption = T("Loại khóa học");
    st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
    st.Properties.Items.Add(T("Huấn luyện"), (int)ProgramTypeEnum.Coaching);
    st.Properties.Items.Add(T("Tập huấn"), (int)ProgramTypeEnum.ShortTime);
    st.Properties.Items.Add(T("Đào tạo dài hạn"), (int)ProgramTypeEnum.LongTimeLongTerm);
    st.Properties.Items.Add(T("Đào tạo trung hạn"), (int)ProgramTypeEnum.LongTimeMidTerm);
    st.Properties.Items.Add(T("Đào tạo liên tục"), (int)ProgramTypeEnum.LongTimeContinuous);
    st.Properties.Items.Add(T("Đào tạo chuyên sâu"), (int)ProgramTypeEnum.LongTimeIntensive);
    st.Properties.ValueType = typeof(int);
    st.Properties.ClientSideEvents.ValueChanged = "function() {UpdateUITraining();}";
 }).Bind((int)Model.ProgramType).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-3_sm-6_xs-12 inp-trainingtime\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 71 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.EditorFor(m => m.TrainingTime));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-3_sm-6_xs-12 inp-traininghour\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 74 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().SpinEditFor(m => m.TrainingHour, st =>
 {
    st.Width = Unit.Percentage(100);
    st.Properties.DisplayFormatString = "#,##0 'giờ'";
    st.Properties.Caption = T("Thời lượng");
    st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
    st.Properties.CaptionSettings.RequiredMarkDisplayMode = EditorRequiredMarkMode.Required;
    st.ShowModelErrors = true;
    st.Properties.ValidationSettings.Display = Display.Dynamic;
    st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
 }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 87 "..\..\Views\Program\EditTemplate.cshtml"
                  Write(Html.EditorFor(m => m.Institution));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 89 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().DateEditFor(m => m.DateStart, st =>
 {
    st.Width = Unit.Percentage(100);
    st.Properties.Caption = T("Ngày khai giảng");
    st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
    st.ShowModelErrors = true;
    st.Properties.ValidationSettings.Display = Display.Dynamic;
    st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
 }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n\r\n   <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 101 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().DateEditFor(m => m.DateEnd, st =>
 {
    st.Width = Unit.Percentage(100);
    st.Properties.Caption = T("Ngày kết thúc");
    st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
    st.ShowModelErrors = true;
    st.Properties.ValidationSettings.Display = Display.Dynamic;
    st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
    if (Model.DateStart.HasValue)
       st.Properties.MinDate = Model.DateStart.Value;
 }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 113 "..\..\Views\Program\EditTemplate.cshtml"
                  Write(Html.EditorFor(m => m.Note));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n</div>\r\n<div");

WriteLiteral(" class=\"grid-middle\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 117 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().Label(st => st.Text = T("Danh sách cán bộ tham gia đào tạo")).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-auto\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 120 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().Button(st =>
 {
    st.Name = "ProgramDoctors";
    st.Text = T("Cập nhật");
    st.Images.Image.IconID = IconID.PeopleUsergroup16x16;
    st.ClientSideEvents.Click = "onProgramDoctorsClick";
 }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 129 "..\..\Views\Program\EditTemplate.cshtml"
 Write(Html.DevExpress().TextBox(st =>
 {
    st.Name = "SearchDoctor";
    st.Width = Unit.Percentage(100);
    st.Properties.NullText = "Tìm kiếm cán bộ";
 }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n</div>\r\n<div");

WriteLiteral(" class=\"mb\"");

WriteLiteral(">\r\n");

WriteLiteral("   ");

            
            #line 138 "..\..\Views\Program\EditTemplate.cshtml"
Write(Html.Partial("ProgramDoctors"));

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n<div");

WriteLiteral(" class=\"\"");

WriteLiteral(">\r\n");

WriteLiteral("   ");

            
            #line 141 "..\..\Views\Program\EditTemplate.cshtml"
Write(Html.EditorFor(m => m.Attachments));

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>");

        }
    }
}
#pragma warning restore 1591
