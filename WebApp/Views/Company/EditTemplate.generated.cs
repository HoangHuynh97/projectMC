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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Company/EditTemplate.cshtml")]
    public partial class _Views_Company_EditTemplate_cshtml : WebApp.Core.BaseWebViewPage<Company>
    {
        public _Views_Company_EditTemplate_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Company\EditTemplate.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-6_xs-12 grid\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Company\EditTemplate.cshtml"
                     Write(Html.EditorFor(m => m.Name));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 6 "..\..\Views\Company\EditTemplate.cshtml"
                     Write(Html.EditorFor(m => m.FullName));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 7 "..\..\Views\Company\EditTemplate.cshtml"
                     Write(Html.EditorFor(m => m.Address));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 8 "..\..\Views\Company\EditTemplate.cshtml"
                          Write(Html.EditorFor(m => m.Phone));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 9 "..\..\Views\Company\EditTemplate.cshtml"
                          Write(Html.EditorFor(m => m.Fax));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 10 "..\..\Views\Company\EditTemplate.cshtml"
                          Write(Html.EditorFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 11 "..\..\Views\Company\EditTemplate.cshtml"
                          Write(Html.EditorFor(m => m.Website));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n         <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 15 "..\..\Views\Company\EditTemplate.cshtml"
                        Write(Html.EditorFor(m => m.Director));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n         <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 16 "..\..\Views\Company\EditTemplate.cshtml"
                        Write(Html.EditorFor(m => m.DirectorPhone));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n         <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 17 "..\..\Views\Company\EditTemplate.cshtml"
                             Write(Html.EditorFor(m => m.RegiserDate));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n         <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">");

            
            #line 18 "..\..\Views\Company\EditTemplate.cshtml"
                             Write(Html.EditorFor(m => m.ExpireDate));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n         <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 20 "..\..\Views\Company\EditTemplate.cshtml"
       Write(Html.DevExpress().SpinEditFor(m => m.MaxUser, st =>
       {
          st.Width = Unit.Percentage(100);
          st.Properties.Caption = T("Số người dùng tối đa");
          st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
          st.Properties.MinValue = 1;
          st.Properties.MaxValue = 99;
       }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n         <div");

WriteLiteral(" class=\"col-6_xs-12-middle\"");

WriteLiteral(" style=\"padding-bottom: 0;\"");

WriteLiteral(">");

            
            #line 29 "..\..\Views\Company\EditTemplate.cshtml"
                                                               Write(Html.EditorFor(m => m.Active));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 33 "..\..\Views\Company\EditTemplate.cshtml"
 Write(Html.EditorFor(m => m.Note));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
