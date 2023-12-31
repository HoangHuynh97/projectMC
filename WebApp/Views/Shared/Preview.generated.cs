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
    
    #line 1 "..\..\Views\Shared\Preview.cshtml"
    using DevExpress.XtraReports.Web;
    
    #line default
    #line hidden
    using WebApp.Models;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/Preview.cshtml")]
    public partial class _Views_Shared_Preview_cshtml : WebApp.Core.BaseWebViewPage<dynamic>
    {
        public _Views_Shared_Preview_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Shared\Preview.cshtml"
Write(Html.DevExpress().DocumentViewer(st =>
{
   st.Name = "reportViewer";
   st.Width = Unit.Percentage(100);
   st.Height = 300;
   var itRefresh = new ReportToolbarButton();
   itRefresh.Name = "refresh";
   itRefresh.Text = T("Làm mới");
   itRefresh.IconID = IconID.ActionsRefresh16x16;
   itRefresh.ToolTip = T("Cập nhật lại báo cáo với dữ liệu mới nhất (F5)");
   st.ToolbarItems.Add(itRefresh);
   var itFilter = new ReportToolbarButton();
   itFilter.Name = "filter";
   itFilter.Text = T("Lọc dữ liệu");
   itFilter.IconID = IconID.ActionsFilter16x16devav;
   itFilter.ToolTip = T("Cập nhật lại báo cáo với điều kiện lọc khác (F5)");
   st.ToolbarItems.Add(itFilter);
   if (Context.CheckPermission("WebApp.dll", "Mẫu báo cáo", "Thiết kế mẫu"))
   {
      var itDesigner = new ReportToolbarButton();
      itDesigner.Name = "designer";
      itDesigner.Text = T("Thiết kế");
      itDesigner.IconID = IconID.ReportsDesignreport16x16;
      itDesigner.ToolTip = T("Mở chức năng chỉnh sửa báo cáo");
      st.ToolbarItems.Add(itDesigner);
   }
   var itClose = new ReportToolbarButton();
   itClose.Name = "close";
   itClose.Text = T("Đóng");
   itClose.IconID = IconID.ActionsClose16x16;
   itClose.ToolTip = T("Đóng chức năng này (ESC)");
   st.ToolbarItems.Add(itClose);
   st.CallbackRouteValues = new { Action = "Preview" };
   st.ExportRouteValues = new { Action = "Export" };
   st.Report = Session[WebApp.Controllers.ReportViewerController.ReportKey] as DevExpress.XtraReports.UI.XtraReport;
   st.SettingsReportViewer.EnableReportMargins = true;
   st.ClientSideEvents.ToolbarItemClick = "WebApp.function.toolClick";
   st.ClientSideEvents.Init = "function(s) {WebApp.function.viewerInit(s);}";
}).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
