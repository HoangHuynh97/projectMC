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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Facility/Index.cshtml")]
    public partial class _Views_Facility_Index_cshtml : WebApp.Core.BaseWebViewPage<dynamic>
    {
        public _Views_Facility_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr img hospital\"");

WriteLiteral("></div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 5 "..\..\Views\Facility\Index.cshtml"
    Write(H("Đơn vị cung cấp dịch vụ"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n      <form>\r\n");

WriteLiteral("         ");

            
            #line 10 "..\..\Views\Facility\Index.cshtml"
    Write(Html.Partial("IndexPartial"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </form>\r\n   </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n   window.IsNew = function () {\r\n      return parseInt($(\'#Id\').val()) == 0;\r\n" +
"   };\r\n\r\n   WebApp.function = {\r\n      title: \'");

            
            #line 21 "..\..\Views\Facility\Index.cshtml"
         Write(H("Đơn vị cung cấp dịch vụ"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n      permission: {\r\n         add: ");

            
            #line 23 "..\..\Views\Facility\Index.cshtml"
         Write(CheckPermission("Thêm").ToString().ToLower());

            
            #line default
            #line hidden
WriteLiteral(",\r\n         edit: ");

            
            #line 24 "..\..\Views\Facility\Index.cshtml"
          Write(CheckPermission("Sửa").ToString().ToLower());

            
            #line default
            #line hidden
WriteLiteral(",\r\n         del: ");

            
            #line 25 "..\..\Views\Facility\Index.cshtml"
         Write(CheckPermission("Xóa").ToString().ToLower());

            
            #line default
            #line hidden
WriteLiteral(",\r\n      },\r\n      add: function () {\r\n         if (WebApp.loading() || !WebApp.f" +
"unction.permission.add) return;\r\n         WebApp.showDialog({\r\n            title" +
": \'");

            
            #line 30 "..\..\Views\Facility\Index.cshtml"
               Write(H("Thêm đơn vị"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            width: 1000,\r\n            url: \'");

            
            #line 32 "..\..\Views\Facility\Index.cshtml"
             Write(Url.Action("Add"));

            
            #line default
            #line hidden
WriteLiteral(@"',
         }).done(function(v) {
            grdSheet.PerformCallback();
         });
      },
      edit: function () {
         if (WebApp.loading() || !WebApp.function.permission.edit) return;
         var idx = grdSheet.GetFocusedRowIndex();
         var id = grdSheet.GetRowKey(idx);
         if (!id) return;
         WebApp.showDialog({
            title: '");

            
            #line 43 "..\..\Views\Facility\Index.cshtml"
               Write(H("Cập nhật đơn vị"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            width: 1000,\r\n            icon: \'");

            
            #line 45 "..\..\Views\Facility\Index.cshtml"
              Write(IconID.EditEdit32x32);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            url: \'");

            
            #line 46 "..\..\Views\Facility\Index.cshtml"
             Write(Url.Action("Edit"));

            
            #line default
            #line hidden
WriteLiteral(@"',
            data: { id: id },
         }).done(function (v) {
            grdSheet.PerformCallback();
         });
      },
      view: function () {
         var idx = grdSheet.GetFocusedRowIndex();
         var id = grdSheet.GetRowKey(idx);
         if (!id) return;
         WebApp.showDialog({
            title: '");

            
            #line 57 "..\..\Views\Facility\Index.cshtml"
               Write(H("Thông tin đơn vị"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            width: 1000,\r\n            icon: \'");

            
            #line 59 "..\..\Views\Facility\Index.cshtml"
              Write(IconID.ActionsNew32x32);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            url: \'");

            
            #line 60 "..\..\Views\Facility\Index.cshtml"
             Write(Url.Action("ViewInfo"));

            
            #line default
            #line hidden
WriteLiteral(@"',
            data: { id: id },
            buttons: [],
         });
      },
      del: function () {
         if (WebApp.loading() || !WebApp.function.permission.del) return;
         var idx = grdSheet.GetFocusedRowIndex();
         var id = grdSheet.GetRowKey(idx);
         if (!id) return;
         WebApp.showConfirm(WebApp.message.delete, WebApp.message.deleteConfirm).done(function (r) {
            WebApp.ajax('");

            
            #line 71 "..\..\Views\Facility\Index.cshtml"
                    Write(Url.Action("Delete"));

            
            #line default
            #line hidden
WriteLiteral(@"', {id: id}).done(function() {
               grdSheet.PerformCallback();
            });
         });
      },
      export: function () {
         grdSheet.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
      },
      filter: function () {
         grdSheet._showFilter = !(grdSheet._showFilter || false);
         grdSheet.PerformCallback();
      },
      hotkey: {
         f2: function() { WebApp.function.add(); },
         f3: function() { WebApp.function.edit(); },
         f4: function() { WebApp.function.del(); },
         f5: function () { grdSheet.PerformCallback(); },
         esc: function () { WebApp.function.close(); },
      },
      resize: function () {
         $('.page-content').height($('.page-container').height() - $('.page-header').height());
         grdSheet.SetHeight($('.page-content').innerHeight());
      },
      close: WebApp.closeFunction.bind(WebApp),
      update: function() {
         var idx = grdSheet.GetFocusedRowIndex();
         var tool = grdSheet.GetToolbar(0);
         tool.GetItemByName(""edit"").SetEnabled(idx !== -1);
         tool.GetItemByName(""del"").SetEnabled(idx !== -1);
      }
   };
</script>");

        }
    }
}
#pragma warning restore 1591
