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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Medical/EndProcess.cshtml")]
    public partial class _Views_Medical_EndProcess_cshtml : WebApp.Core.BaseWebViewPage<EndProcess>
    {
        public _Views_Medical_EndProcess_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" id=\"dxss_endprocess\"");

WriteLiteral(">\r\n   function onEndProcessSave() {\r\n      if (!WebApp.validForm(\'#frmEndProcess\'" +
")) return;\r\n      if (!AttachmentListEndProcess.GetVisibleRowsOnPage()) {\r\n     " +
"    WebApp.showInfo(\'");

            
            #line 7 "..\..\Views\Medical\EndProcess.cshtml"
                     Write(H("Lỗi"));

            
            #line default
            #line hidden
WriteLiteral("\', \'");

            
            #line 7 "..\..\Views\Medical\EndProcess.cshtml"
                                  Write(H("Vui lòng cập nhật tài liệu, hình ảnh, video kèm theo!"));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n         return;\r\n      }\r\n      EndProcessSave.SetEnabled(false);\r\n      We" +
"bApp.ajax(\'");

            
            #line 11 "..\..\Views\Medical\EndProcess.cshtml"
              Write(Url.Action("EndProcess", "Medical"));

            
            #line default
            #line hidden
WriteLiteral("\', getEndData()).done(function (r) {\r\n         processModel.reload();\r\n      }).f" +
"ail(function () { EndProcessSave.SetEnabled(true); });\r\n   }\r\n\r\n   function onEn" +
"dProcessDelete() {\r\n      WebApp.showConfirm(\'");

            
            #line 17 "..\..\Views\Medical\EndProcess.cshtml"
                     Write(H("Xóa"));

            
            #line default
            #line hidden
WriteLiteral("\', \'");

            
            #line 17 "..\..\Views\Medical\EndProcess.cshtml"
                                  Write(H("Bạn có muốn xóa dữ liệu và tài liệu kết thúc điều trị?"));

            
            #line default
            #line hidden
WriteLiteral("\').done(function () {\r\n         WebApp.ajax(\'");

            
            #line 18 "..\..\Views\Medical\EndProcess.cshtml"
                 Write(Url.Action("DeleteEndProcess", "Medical"));

            
            #line default
            #line hidden
WriteLiteral("\', { id: ");

            
            #line 18 "..\..\Views\Medical\EndProcess.cshtml"
                                                                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@"}).done(function () {
            processModel.reload();
         });
      });
   }

   function getEndData() {
      var d = CompletedDate.GetDate();
      return {
         Id: processModel.id,
         CompletedDate: d && d.toISOString() || null,
         EndSuccess: EndSuccess.GetValue(),
         EndEvaluation: EndEvaluation.GetValue(),
         EndEvaluationTool: EndEvaluationTool.GetValue(),
         EndEvaluationPoint: EndEvaluationPoint.GetValue(),
         Attachments: { Items: AttachmentListEndProcess.GetChanged(), },
         Doctors: { Items: DoctorsEndDoctors.GetSelectedValues().map(function (id) { return { Id: id }; }), Facility: ");

            
            #line 34 "..\..\Views\Medical\EndProcess.cshtml"
                                                                                                                  Write(Model.Doctors.Facility);

            
            #line default
            #line hidden
WriteLiteral(@" },
      };
   }

   setTimeout(function () {
      WebApp.validateForm('#frmEndProcess');
      var oldData = JSON.stringify(getEndData());

      [CompletedDate, EndSuccess, EndEvaluation, EndEvaluationTool, EndEvaluationPoint].forEach(function (it) {
         it.ValueChanged.AddHandler(function () {
            var d = JSON.stringify(getEndData());
            EndProcessSave.SetVisible(d != oldData);
         });
      });
      AttachmentListEndProcess.onDataChanged = function (data) {
         var d = JSON.stringify(getEndData());
         EndProcessSave.SetVisible(d != oldData);
      };
      DoctorsEndDoctors.SelectedIndexChanged.AddHandler(function () {
         var d = JSON.stringify(getEndData());
         EndProcessSave.SetVisible(d != oldData);
      });
   });
</script>
<form");

WriteLiteral(" id=\"frmEndProcess\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 61 "..\..\Views\Medical\EndProcess.cshtml"
    Write(Html.DevExpress().DateEditFor(m => m.CompletedDate, st =>
    {
       st.Width = Unit.Percentage(100);
       st.Properties.MinDate = Model.MinDate;
       st.Properties.MaxDate = DateTime.Today;
       st.Properties.AllowNull = false;
       st.ShowModelErrors = true;
       st.Properties.ValidationSettings.Display = Display.Dynamic;
       st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
       st.Properties.CaptionSettings.RequiredMarkDisplayMode = EditorRequiredMarkMode.Required;
       st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
       st.Properties.Caption = T("Ngày dừng can thiệp");
    }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 75 "..\..\Views\Medical\EndProcess.cshtml"
                               Write(Html.EditorFor(m => m.EndEvaluationTool));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 76 "..\..\Views\Medical\EndProcess.cshtml"
                               Write(Html.EditorFor(m => m.EndEvaluationPoint));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-12 grid-middle-noBottom\"");

WriteLiteral(">\r\n         <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">");

            
            #line 78 "..\..\Views\Medical\EndProcess.cshtml"
                                Write(H("Kết quả về cải thiện chức năng sinh hoạt hàng ngày (ADL)"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n         <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 80 "..\..\Views\Medical\EndProcess.cshtml"
       Write(Html.DevExpress().RadioButtonListFor(m => m.EndSuccess, st =>
       {
          st.Properties.RepeatColumns = 2;
          st.Properties.Items.Add(T("Có"), true);
          st.Properties.Items.Add(T("Không"), false);
          st.ControlStyle.Border.BorderWidth = 0;
          st.Properties.EnableClientSideAPI = true;
          st.Properties.ValueType = typeof(bool?);
          st.ShowModelErrors = true;
          st.Properties.ValidationSettings.Display = Display.Dynamic;
          st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
       }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 94 "..\..\Views\Medical\EndProcess.cshtml"
                     Write(Html.EditorFor(m => m.EndEvaluation));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 95 "..\..\Views\Medical\EndProcess.cshtml"
                     Write(Html.EditorFor(m => m.Doctors));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 96 "..\..\Views\Medical\EndProcess.cshtml"
                     Write(Html.EditorFor(m => m.Attachments));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"grid-noBottom-noGutter\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"col-auto\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 100 "..\..\Views\Medical\EndProcess.cshtml"
    Write(Html.DevExpress().Button(btn =>
    {
       btn.Name = "EndProcessSave";
       btn.Text = T("Lưu thay đổi");
       btn.Images.Image.IconID = IconID.SaveSave16x16;
       btn.Styles.Style.Paddings.Padding = 0;
       btn.RenderMode = ButtonRenderMode.Outline;
       btn.ClientVisible = false;
       btn.ControlStyle.CssClass = "mr";
       btn.ClientSideEvents.Click = "onEndProcessSave";
    }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-auto\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 113 "..\..\Views\Medical\EndProcess.cshtml"
    Write(Html.DevExpress().Button(btn =>
    {
       btn.Name = "EndProcessDelete";
       btn.Text = T("Xóa");
       btn.Images.Image.IconID = IconID.EditDelete16x16;
       btn.Styles.Style.Paddings.Padding = 0;
       btn.RenderMode = ButtonRenderMode.Outline;
       btn.ClientSideEvents.Click = "onEndProcessDelete";
    }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n</form>");

        }
    }
}
#pragma warning restore 1591
