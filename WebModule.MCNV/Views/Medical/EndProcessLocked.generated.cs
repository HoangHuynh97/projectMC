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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Medical/EndProcessLocked.cshtml")]
    public partial class _Views_Medical_EndProcessLocked_cshtml : WebApp.Core.BaseWebViewPage<EndProcess>
    {
        public _Views_Medical_EndProcessLocked_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 4 "..\..\Views\Medical\EndProcessLocked.cshtml"
                            Write(Html.LockedText(m => m.CompletedDate));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Medical\EndProcessLocked.cshtml"
                            Write(Html.LockedText(m => m.EndEvaluationToolName));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 6 "..\..\Views\Medical\EndProcessLocked.cshtml"
                            Write(Html.LockedText(m => m.EndEvaluationPoint));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-6_md-8_xs-12\"");

WriteLiteral(">");

            
            #line 7 "..\..\Views\Medical\EndProcessLocked.cshtml"
                            Write(Html.LockedText(T("Kết quả về cải thiện chức năng sinh hoạt hàng ngày (ADL)"), Model.EndSuccess.Value ? T("Có") : T("Không")));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 8 "..\..\Views\Medical\EndProcessLocked.cshtml"
                  Write(Html.EditorFor(m => m.Doctors, "DoctorsLocked"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(" style=\"padding-bottom: 0;\"");

WriteLiteral(">");

            
            #line 9 "..\..\Views\Medical\EndProcessLocked.cshtml"
                                             Write(Html.EditorFor(m => m.Attachments, "AttachmentsLocked"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591