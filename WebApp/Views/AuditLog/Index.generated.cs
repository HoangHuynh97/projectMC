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
    using WebApp.Models;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/AuditLog/Index.cshtml")]
    public partial class _Views_AuditLog_Index_cshtml : WebApp.Core.BaseWebViewPage<AuditLog>
    {
        public _Views_AuditLog_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"col_xs-12 grid-middle\"");

WriteLiteral(" style=\"max-width: 300px;\"");

WriteLiteral(">\r\n         <div");

WriteLiteral(" class=\"mr img data_transfer\"");

WriteLiteral("></div>\r\n         <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 7 "..\..\Views\AuditLog\Index.cshtml"
       Write(H("Nhật ký dữ liệu"));

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col_xs-12 grid-noBottom\"");

WriteLiteral(">\r\n         <div");

WriteLiteral(" class=\"col_xs-12\"");

WriteLiteral(" style=\"max-width: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 12 "..\..\Views\AuditLog\Index.cshtml"
       Write(Html.DevExpress().DateEditFor(m => m.TuNgay, st =>
       {
          st.Width = Unit.Percentage(100);
          st.Properties.Caption = T("Từ ngày");
          st.Properties.AllowNull = false;
          st.Properties.MaxDate = DateTime.Today;
          st.Properties.ClientSideEvents.ValueChanged = "function(s,e) {grdSheet.PerformCallback();}";
       }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n         <div");

WriteLiteral(" class=\"col_xs-12\"");

WriteLiteral(" style=\"max-width: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 22 "..\..\Views\AuditLog\Index.cshtml"
       Write(Html.DevExpress().DateEditFor(m => m.DenNgay, st =>
       {
          st.Width = Unit.Percentage(100);
          st.Properties.Caption = T("Đến ngày");
          st.Properties.AllowNull = false;
          st.Properties.MaxDate = DateTime.Today;
          st.Properties.DateRangeSettings.StartDateEditID = "TuNgay";
          st.Properties.ClientSideEvents.ValueChanged = "function(s,e) {grdSheet.PerformCallback();}";
       }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n      </div>\r\n\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n      <form>\r\n");

WriteLiteral("         ");

            
            #line 37 "..\..\Views\AuditLog\Index.cshtml"
    Write(Html.Partial("IndexPartial"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </form>\r\n   </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n   WebApp.function = {\r\n      title: \'");

            
            #line 44 "..\..\Views\AuditLog\Index.cshtml"
         Write(H("Nhật ký dữ liệu"));

            
            #line default
            #line hidden
WriteLiteral(@"',
      filter: function () {
         grdSheet._showFilter = !(grdSheet._showFilter || false);
         grdSheet.PerformCallback();
      },
      hotkey: {
         f5: function () { grdSheet.PerformCallback(); },
         esc: function () { WebApp.function.close(); },
      },
      resize: function () {
         $('.page-content').height($('.page-container').height() - $('.page-header').height());
         grdSheet.SetHeight($('.page-content').innerHeight());
      },
      close: WebApp.closeFunction.bind(WebApp),
      toolClick: function (s, e) {
         e.item.name && WebApp.function[e.item.name] && WebApp.function[e.item.name]();
      },
      filter: function () {
         grdSheet._showFilter = !(grdSheet._showFilter || false);
         grdSheet.PerformCallback();
      },
   };
</script>");

        }
    }
}
#pragma warning restore 1591
