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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Medical/BeginProcessLocked.cshtml")]
    public partial class _Views_Medical_BeginProcessLocked_cshtml : WebApp.Core.BaseWebViewPage<BeginProcess>
    {
        public _Views_Medical_BeginProcessLocked_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-auto_md-6_xs-12\"");

WriteLiteral(">");

            
            #line 4 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                               Write(Html.LockedText(m => m.EvaluationDate));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-auto_md-6_xs-12\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                               Write(Html.LockedText(m => m.EvaluationToolName));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-auto_md-6_xs-12\"");

WriteLiteral(">");

            
            #line 6 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                               Write(Html.LockedText(m => m.EvaluationPoint));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col_md-6_xs-12\"");

WriteLiteral(">");

            
            #line 7 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                          Write(Html.LockedText(m => m.Evaluation));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 8 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                  Write(Html.LockedText(m => m.Diagnostics));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 9 "..\..\Views\Medical\BeginProcessLocked.cshtml"
   
            
            #line default
            #line hidden
            
            #line 9 "..\..\Views\Medical\BeginProcessLocked.cshtml"
    if (!Model.NoProcess.Value)
   {

            
            #line default
            #line hidden
WriteLiteral("      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 11 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                     Write(Html.LockedText(m => m.Treatments));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

WriteLiteral("      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 12 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                     Write(Html.LockedText(m => m.Services));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 13 "..\..\Views\Medical\BeginProcessLocked.cshtml"
   }
   else
   {

            
            #line default
            #line hidden
WriteLiteral("      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 16 "..\..\Views\Medical\BeginProcessLocked.cshtml"
                     Write(Html.LockedText(m => m.ReasonCancel));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 17 "..\..\Views\Medical\BeginProcessLocked.cshtml"
   }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div>");

            
            #line 19 "..\..\Views\Medical\BeginProcessLocked.cshtml"
Write(Html.EditorFor(m => m.Doctors, "DoctorsLocked"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div>");

            
            #line 20 "..\..\Views\Medical\BeginProcessLocked.cshtml"
Write(Html.EditorFor(m => m.Attachments, "AttachmentsLocked"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591
