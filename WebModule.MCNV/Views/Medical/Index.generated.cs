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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Medical/Index.cshtml")]
    public partial class _Views_Medical_Index_cshtml : WebApp.Core.BaseWebViewPage<dynamic>
    {
        public _Views_Medical_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr img documents2\"");

WriteLiteral("></div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 5 "..\..\Views\Medical\Index.cshtml"
    Write(H("Hồ sơ phục hồi chức năng"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n      <form>\r\n");

WriteLiteral("         ");

            
            #line 10 "..\..\Views\Medical\Index.cshtml"
    Write(Html.Partial("IndexPartial"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </form>\r\n   </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n   window.IsNew = function () {\r\n      return parseInt($(\'#Id\').val()) == 0;\r\n" +
"   };\r\n\r\n   WebApp.function = {\r\n      title: \'");

            
            #line 21 "..\..\Views\Medical\Index.cshtml"
         Write(H("Hồ sơ PHCN"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n      permission: {\r\n         add: ");

            
            #line 23 "..\..\Views\Medical\Index.cshtml"
         Write(CheckPermission("Thêm").ToString().ToLower());

            
            #line default
            #line hidden
WriteLiteral(",\r\n         edit: ");

            
            #line 24 "..\..\Views\Medical\Index.cshtml"
          Write(CheckPermission("Sửa").ToString().ToLower());

            
            #line default
            #line hidden
WriteLiteral(",\r\n         del: ");

            
            #line 25 "..\..\Views\Medical\Index.cshtml"
         Write(CheckPermission("Xóa").ToString().ToLower());

            
            #line default
            #line hidden
WriteLiteral(",\r\n      },\r\n      add: function () {\r\n         if (WebApp.loading() || !WebApp.f" +
"unction.permission.add) return;\r\n         WebApp.showDialog({\r\n            name:" +
" \'dlgProcess\',\r\n            width: 1000,\r\n            url: \'");

            
            #line 32 "..\..\Views\Medical\Index.cshtml"
             Write(Url.Action("Add"));

            
            #line default
            #line hidden
WriteLiteral(@"',
            buttons: [],
         }).done(function() {
            processModel.changed() && grdSheet.PerformCallback();
         });
      },
      edit: function () {
         if (WebApp.loading() || !WebApp.function.permission.edit) return;
         var idx = grdSheet.GetFocusedRowIndex();
         var id = grdSheet.GetRowKey(idx);
         if (!id) return;
         WebApp.showDialog({
            name: 'dlgProcess',
            width: 1000,
            icon: '");

            
            #line 46 "..\..\Views\Medical\Index.cshtml"
              Write(IconID.EditEdit32x32);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            url: \'");

            
            #line 47 "..\..\Views\Medical\Index.cshtml"
             Write(Url.Action("Edit"));

            
            #line default
            #line hidden
WriteLiteral(@"',
            data: { id: id },
            buttons: [],
         }).done(function () {
            processModel.changed() && grdSheet.PerformCallback();
         });
      },
      del: function () {
         if (WebApp.loading() || !WebApp.function.permission.del) return;
         var idx = grdSheet.GetFocusedRowIndex();
         var id = grdSheet.GetRowKey(idx);
         if (!id) return;
         WebApp.showConfirm(WebApp.message.delete, WebApp.message.deleteConfirm + '<br/>Lưu ý: Xóa hồ sơ sẽ KHÔNG xóa thông tin NKT.').done(function (r) {
            WebApp.ajax('");

            
            #line 60 "..\..\Views\Medical\Index.cshtml"
                    Write(Url.Action("Delete"));

            
            #line default
            #line hidden
WriteLiteral("\', {id: id}).done(function() {\r\n               grdSheet.PerformCallback();\r\n     " +
"       });\r\n         });\r\n      },\r\n      export: function () {\r\n         grdShe" +
"et.ExportTo(ASPxClientGridViewExportFormat.Xlsx);\r\n      },\r\n      filter: funct" +
"ion () {\r\n         grdSheet._showFilter = !(grdSheet._showFilter || false);\r\n   " +
"      grdSheet.PerformCallback();\r\n      },\r\n      hotkey: {\r\n         f2: funct" +
"ion() { WebApp.function.add(); },\r\n         f3: function() { WebApp.function.edi" +
"t(); },\r\n         f4: function() { WebApp.function.del(); },\r\n         f5: funct" +
"ion () { grdSheet.PerformCallback(); },\r\n         esc: function () { WebApp.func" +
"tion.close(); },\r\n      },\r\n      resize: function () {\r\n         $(\'.page-conte" +
"nt\').height($(\'.page-container\').height() - $(\'.page-header\').height());\r\n      " +
"   grdSheet.SetHeight($(\'.page-content\').innerHeight());\r\n      },\r\n      close:" +
" WebApp.closeFunction.bind(WebApp),\r\n      update: function() {\r\n         var id" +
"x = grdSheet.GetFocusedRowIndex();\r\n         var tool = grdSheet.GetToolbar(0);\r" +
"\n         tool.GetItemByName(\"edit\").SetEnabled(idx !== -1);\r\n         tool.GetI" +
"temByName(\"del\").SetEnabled(idx !== -1);\r\n      },\r\n      filterControl: functio" +
"n () {\r\n         grdSheet._showFilter = false;\r\n      },\r\n      toggleProcessRea" +
"son: function (suffix) {\r\n         $(\'[processreason-suffix=\"\' + suffix + \'\"]\')." +
"toggle(window[\'ProcessStatus\' + suffix].GetValue() == 4);\r\n      }\r\n   };\r\n\r\n   " +
"window.CheckProcessReason = function () {\r\n      const suffix = $(this).closest(" +
"\'[processreason-suffix]\').attr(\'processreason-suffix\');\r\n      return window[\'Pr" +
"ocessStatus\' + suffix].GetValue() == 4;\r\n   }\r\n</script>");

        }
    }
}
#pragma warning restore 1591