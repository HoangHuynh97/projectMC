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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Medical/FacilityProcessLocked.cshtml")]
    public partial class _Views_Medical_FacilityProcessLocked_cshtml : WebApp.Core.BaseWebViewPage<FacilityProcess>
    {
        public _Views_Medical_FacilityProcessLocked_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">");

            
            #line 4 "..\..\Views\Medical\FacilityProcessLocked.cshtml"
                          Write(Html.LockedText(T("Ngày vào viện"), Model.DateIn.ToShortDateString()));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Medical\FacilityProcessLocked.cshtml"
                          Write(Html.LockedText(T("Ngày ra viện"), Model.DateOut.HasValue ? Model.DateOut.Value.ToShortDateString() : ""));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">");

            
            #line 6 "..\..\Views\Medical\FacilityProcessLocked.cshtml"
                          Write(Html.LockedText(T("Lĩnh vực can thiệp"), Model.ServiceName));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-auto_xs-12\"");

WriteLiteral(">");

            
            #line 7 "..\..\Views\Medical\FacilityProcessLocked.cshtml"
                          Write(Html.LockedText(T("Kết quả can thiệp"), Model.StatusName));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n</div>\r\n<div>");

            
            #line 9 "..\..\Views\Medical\FacilityProcessLocked.cshtml"
Write(Html.EditorFor(m => m.Doctors, "DoctorsLocked"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div>");

            
            #line 10 "..\..\Views\Medical\FacilityProcessLocked.cshtml"
Write(Html.EditorFor(m => m.Attachments, "AttachmentsLocked"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591
