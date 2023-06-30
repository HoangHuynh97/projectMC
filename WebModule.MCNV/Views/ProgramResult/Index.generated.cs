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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ProgramResult/Index.cshtml")]
    public partial class _Views_ProgramResult_Index_cshtml : WebApp.Core.BaseWebViewPage<ProgramResult>
    {
        public _Views_ProgramResult_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 5 "..\..\Views\ProgramResult\Index.cshtml"
    Write(Html.Icon("actions_newitem_32x32devav"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 8 "..\..\Views\ProgramResult\Index.cshtml"
    Write(H("Kết quả đào tạo"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" style=\"margin-left: 20px; max-width: 400px; flex: 1;\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 11 "..\..\Views\ProgramResult\Index.cshtml"
    Write(Html.DevExpress().ComboBox(st =>
    {
       st.Name = "cmbProgram";
       st.Width = Unit.Percentage(100);
       st.Properties.NullText = T("Chọn khóa đào tạo");
       st.Properties.ValueField = "Oid";
       st.Properties.ValueType = typeof(int);
       st.Properties.TextField = "Name";
       st.Properties.ClientSideEvents.ValueChanged = "function() {WebApp.function.reload();}";
       if (ViewBag.ProgramId != null)
       {
          st.Properties.ClientSideEvents.Init = "function(s) {setTimeout(function() {s.SetValue(" + ViewBag.ProgramId + ");});s.RaiseValueChanged();}";
       }
    }).BindList(Program.ComboboxData()).Bind(ViewBag.ProgramId).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" style=\"margin-left: 20px; width: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 27 "..\..\Views\ProgramResult\Index.cshtml"
    Write(Html.DevExpress().Button(st =>
    {
       st.Name = "btnProgramResultSave";
       st.Text = T("Lưu thay đổi");
       st.Images.Image.IconID = IconID.SaveSave16x16;
       st.EnableClientSideAPI = true;
       st.ClientVisible = false;
       st.ClientSideEvents.Click = "function() {WebApp.function.save();}";
    }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 39 "..\..\Views\ProgramResult\Index.cshtml"
 Write(Html.Partial("IndexPartial"));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n</div>\r\n\r\n\r\n<script>\r\n   WebApp.function = {\r\n      id: 0,\r\n      ti" +
"tle: \'");

            
            #line 47 "..\..\Views\ProgramResult\Index.cshtml"
         Write(H("Kết quả đào tạo"));

            
            #line default
            #line hidden
WriteLiteral(@"',
      resize: function () {
         $('.page-content').height($('.page-container').height() - $('.page-header').height());
      },
      errors: [],
      hasChange: false,
      reload: function () {
         const old = this.id;
         var id = cmbProgram.GetValue();
         if (!id) return;
         this.id = id;
         if (this.hasChange) {
            WebApp.showConfirm('");

            
            #line 59 "..\..\Views\ProgramResult\Index.cshtml"
                           Write(H("Lưu dữ liệu"));

            
            #line default
            #line hidden
WriteLiteral("\', \'");

            
            #line 59 "..\..\Views\ProgramResult\Index.cshtml"
                                                Write(H("Bạn có muốn lưu các thay đổi của kết quả đào tạo đang xem không?"));

            
            #line default
            #line hidden
WriteLiteral(@"')
               .done(function () {
                  WebApp.function.hasChange = false;
                  WebApp.function.save(old);
               })
               .fail(function () {
                  panelProgramResult.PerformCallback();
               });
         }
         else
            panelProgramResult.PerformCallback();
      },
      afterReload: function () {
         this.errors = [];
         this.setChanged(false);
         const fChange = function () {
            const b = grdResult.GetBatchEditHelper().CanUpdate() ||
               AttachmentListAttachments.GetBatchEditHelper().CanUpdate();
            WebApp.function.setChanged(b);
         };
         AttachmentListAttachments.onDataChanged = function () {
            fChange();
         };
         grdResult.BatchEditEndEditing.AddHandler(function (_, e) {
            setTimeout(fChange);
         });
      },
      hotkey: {
         esc: function () { WebApp.function.close(); },
      },
      close: WebApp.closeFunction.bind(WebApp),
      setChanged: function (b) {
         this.hasChange = b;
         btnProgramResultSave.SetVisible(b);
      },
      save: function (id) {
         if (WebApp.function.errors.length) {
            WebApp.showInfo('");

            
            #line 96 "..\..\Views\ProgramResult\Index.cshtml"
                        Write(H("Lỗi"));

            
            #line default
            #line hidden
WriteLiteral("\', \'");

            
            #line 96 "..\..\Views\ProgramResult\Index.cshtml"
                                     Write(H("Dữ liệu không hợp lệ"));

            
            #line default
            #line hidden
WriteLiteral(@"');
            return;
         }
         var data = {
            Id: id || cmbProgram.GetValue(),
            Attachments: { Items: AttachmentListAttachments.GetChanged(), },
            Doctors: grdResult.GetChanged(),
         }
         if (!data.Attachments.Items.length && !data.Doctors.length) return;
         //convert format decimal so text can parse to float in server-side
         data.Doctors.forEach(function (d) {
            const keys = Object.keys(d);
            keys.forEach(function (k) {
               if (k.startsWith('Score')) {
                  d[k] = ASPx.NumberFormatter.Format('0.#', parseFloat(d[k]) || 0) || '0';
               }
            });
         });

         WebApp.ajax('");

            
            #line 115 "..\..\Views\ProgramResult\Index.cshtml"
                 Write(Url.Action("Save"));

            
            #line default
            #line hidden
WriteLiteral("\', data).done(function () {\r\n            WebApp.function.hasChange = false;\r\n    " +
"        WebApp.function.reload();\r\n         });\r\n      },\r\n      updatePercent: " +
"function (s, e) {\r\n         WebApp.function.resultEndEditing(s, e);\r\n         se" +
"tTimeout(function () {\r\n            if ([\'Score1\', \'Score2\'].indexOf(e.focusedCo" +
"lumn.fieldName) == -1) return;\r\n            var a = parseFloat(s.batchEditApi.Ge" +
"tCellValue(e.visibleIndex, \"Score1\")) || 0;\r\n            var b = parseFloat(s.ba" +
"tchEditApi.GetCellValue(e.visibleIndex, \"Score2\")) || 0;\r\n            var c = 0;" +
"\r\n            if (a != 0)\r\n               c = Math.round(b * 100 / a);\r\n        " +
"    s.batchEditApi.SetCellValue(e.visibleIndex, \"Score3\", c);\r\n         });\r\n   " +
"   },\r\n      updateHours: function (s, e) {\r\n         WebApp.function.resultEndE" +
"diting(s, e);\r\n         setTimeout(function () {\r\n            const lst = [\'Scor" +
"e1\', \'Score2\', \'Score3\', \'Score4\', \'Score5\', \'Score6\', \'Score7\', \'Score8\', \'Scor" +
"e9\', \'Score10\', \'Score11\', \'Score12\'];\r\n            if (lst.indexOf(e.focusedCol" +
"umn.fieldName) == -1) return;\r\n            const all = (parseFloat(s.batchEditAp" +
"i.GetCellValue(e.visibleIndex, \'HoursTotal\')) || 0)\r\n               + (parseFloa" +
"t(s.batchEditApi.GetCellValue(e.visibleIndex, \'HoursLeft\')) || 0);\r\n            " +
"var total = 0;\r\n            lst.forEach(function (f) {\r\n               total += " +
"parseFloat(s.batchEditApi.GetCellValue(e.visibleIndex, f)) || 0;\r\n            })" +
";\r\n            s.batchEditApi.SetCellValue(e.visibleIndex, \"HoursTotal\", total);" +
"\r\n            s.batchEditApi.SetCellValue(e.visibleIndex, \"HoursLeft\", all - tot" +
"al);\r\n         });\r\n      },\r\n      resultStartEditing: function (s, e) {\r\n     " +
"    if (e.focusedColumn.fieldName == \'ReasonCancel\') {\r\n            var v = s.ba" +
"tchEditApi.GetCellValue(e.visibleIndex, \'Status\');\r\n            e.cancel = v != " +
"3;\r\n         }\r\n      },\r\n      resultEndEditing: function (s, e) {\r\n         if" +
" (e.focusedColumn.fieldName == \'Status\' && !e.cancel) {\r\n            const v = e" +
".rowValues[e.focusedColumn.index].value;\r\n            if (v != 3 && s.batchEditA" +
"pi.GetCellValue(e.visibleIndex, \'ReasonCancel\')) {\r\n               s.batchEditAp" +
"i.SetCellValue(e.visibleIndex, \'ReasonCancel\', \'\');\r\n            }\r\n         }\r\n" +
"      },\r\n      resultValidating: function (grid, e) {\r\n         var status = e." +
"validationInfo[grid.GetColumnByField(\"Status\").index];\r\n         var reason = e." +
"validationInfo[grid.GetColumnByField(\"ReasonCancel\").index];\r\n         if (statu" +
"s.value === 3 && !reason.value) {\r\n            //there are unknow issue layout t" +
"hat it goto mode Updated values, so ignore it for now\r\n            //reason.isVa" +
"lid = false;\r\n            //reason.errorText = \'");

            
            #line 167 "..\..\Views\ProgramResult\Index.cshtml"
                             Write(H("Nhập lý do nghỉ học"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n            WebApp.function.errors.push(e.key);\r\n         }\r\n         else\r\n " +
"           WebApp.function.errors = WebApp.function.errors.filter(function (id) " +
"{ return id != e.key; });\r\n      },\r\n      export: function () {\r\n         windo" +
"w.open(\'");

            
            #line 174 "..\..\Views\ProgramResult\Index.cshtml"
                 Write(Url.Action("Export"));

            
            #line default
            #line hidden
WriteLiteral("\' + \'/?program=\' + WebApp.function.id);\r\n      }\r\n   };\r\n</script>");

        }
    }
}
#pragma warning restore 1591