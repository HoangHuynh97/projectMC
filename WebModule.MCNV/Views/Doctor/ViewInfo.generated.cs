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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Doctor/ViewInfo.cshtml")]
    public partial class _Views_Doctor_ViewInfo_cshtml : WebApp.Core.BaseWebViewPage<Doctor.ViewInfo>
    {
        public _Views_Doctor_ViewInfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 4 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Name));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Gender));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 6 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.DateBirth));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 7 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Facility));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 8 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Phone));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 9 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 10 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Title));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 11 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Position));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 12 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Qualification));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-3_xs-12\"");

WriteLiteral(">");

            
            #line 13 "..\..\Views\Doctor\ViewInfo.cshtml"
                       Write(Html.LockedText(m => m.Specialize));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 14 "..\..\Views\Doctor\ViewInfo.cshtml"
                  Write(Html.LockedText(m => m.Note, true));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591