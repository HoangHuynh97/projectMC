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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/CompanyInfo/Index.cshtml")]
    public partial class _Views_CompanyInfo_Index_cshtml : WebApp.Core.BaseWebViewPage<CompanyInfo>
    {
        public _Views_CompanyInfo_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr img building\"");

WriteLiteral("></div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 6 "..\..\Views\CompanyInfo\Index.cshtml"
    Write(T("Thông tin doanh nghiệp"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n         <div");

WriteLiteral(" class=\"col-8_md-12\"");

WriteLiteral(">\r\n            <form");

WriteLiteral(" id=\"frmCompanyInfo\"");

WriteAttribute("action", Tuple.Create(" action=\"", 356), Tuple.Create("\"", 386)
            
            #line 12 "..\..\Views\CompanyInfo\Index.cshtml"
, Tuple.Create(Tuple.Create("", 365), Tuple.Create<System.Object, System.Int32>(Url.Action("Update")
            
            #line default
            #line hidden
, 365), false)
);

WriteLiteral(" method=\"post\"");

WriteLiteral(" class=\"box\"");

WriteLiteral(">\r\n               <div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n                  <div");

WriteLiteral(" class=\"col-8_xs-12 grid\"");

WriteLiteral(">\r\n                     <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 15 "..\..\Views\CompanyInfo\Index.cshtml"
                                    Write(Html.EditorFor(m => m.Name));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                     <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 16 "..\..\Views\CompanyInfo\Index.cshtml"
                                    Write(Html.EditorFor(m => m.FullName));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                     <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">");

            
            #line 17 "..\..\Views\CompanyInfo\Index.cshtml"
                                    Write(Html.EditorFor(m => m.Address));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                     <div");

WriteLiteral(" class=\"col-6\"");

WriteLiteral(">");

            
            #line 18 "..\..\Views\CompanyInfo\Index.cshtml"
                                   Write(Html.EditorFor(m => m.Phone));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                     <div");

WriteLiteral(" class=\"col-6\"");

WriteLiteral(">");

            
            #line 19 "..\..\Views\CompanyInfo\Index.cshtml"
                                   Write(Html.EditorFor(m => m.Fax));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                     <div");

WriteLiteral(" class=\"col-6\"");

WriteLiteral(">");

            
            #line 20 "..\..\Views\CompanyInfo\Index.cshtml"
                                   Write(Html.EditorFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                     <div");

WriteLiteral(" class=\"col-6\"");

WriteLiteral(">");

            
            #line 21 "..\..\Views\CompanyInfo\Index.cshtml"
                                   Write(Html.EditorFor(m => m.Website));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                  </div>\r\n                  <div");

WriteLiteral(" class=\"col-4_xs-12\"");

WriteLiteral(" style=\"min-height: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 24 "..\..\Views\CompanyInfo\Index.cshtml"
                Write(Html.DevExpress().BinaryImageFor(m => m.Logo, st =>
                     {
                        st.Width = Unit.Percentage(100);
                        st.Height = Unit.Percentage(100);
                        st.CallbackRouteValues = new { Action = "UploadLogo" };
                        st.Properties.ImageWidth = 300;
                        st.Properties.ImageHeight = 300;
                        st.Properties.ImageSizeMode = ImageSizeMode.FitProportional;
                        st.Properties.EditingSettings.Enabled = true;
                        st.Properties.EditingSettings.AllowDropOnPreview = true;
                        st.Properties.EditingSettings.UploadSettings.UploadValidationSettings.MaxFileSize = 1000000;
                     }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n                  <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 38 "..\..\Views\CompanyInfo\Index.cshtml"
                Write(Html.DevExpress().Button(b =>
                     {
                        b.Name = "btnUpdateCompanyInfo";
                        b.Text = T("Lưu");
                        b.Images.Image.IconID = IconID.SaveSave16x16;
                        b.ClientSideEvents.Click = "WebApp.function.save";
                     }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n               </div>\r\n            </form>\r\n         " +
"</div>\r\n         <div");

WriteLiteral(" class=\"col-4_md-12\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"box\"");

WriteLiteral(">\r\n               <div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n                  <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 53 "..\..\Views\CompanyInfo\Index.cshtml"
                Write(Html.DevExpress().Label(st =>
                     {
                        st.Text = T("Ngày đăng ký: {0}", Model.RegiserDate);
                     }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n                  <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 59 "..\..\Views\CompanyInfo\Index.cshtml"
                Write(Html.DevExpress().Label(st =>
                     {
                        st.Text = T("Ngày hết hạn: {0}", Model.ExpireDate);
                     }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n                  <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 65 "..\..\Views\CompanyInfo\Index.cshtml"
                Write(Html.DevExpress().Label(st =>
                     {
                        st.Text = T("Số người dùng tối đa: {0}", Model.MaxUser);
                     }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n               </div>\r\n            </div>\r\n         <" +
"/div>\r\n      </div>\r\n   </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n   WebApp.function = {\r\n      title: \'");

            
            #line 79 "..\..\Views\CompanyInfo\Index.cshtml"
         Write(H("Thông tin doanh nghiệp"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n      save: function (s, e) {\r\n         WebApp.validForm(\'#frmCompanyInfo\') &" +
"& WebApp.submit(\'#frmCompanyInfo\').done(function () {\r\n            WebApp.notify" +
"(\'");

            
            #line 82 "..\..\Views\CompanyInfo\Index.cshtml"
                      Write(H("Đã cập nhật thông tin doanh nghiệp"));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n         });\r\n      }\r\n   };\r\n\r\n   $(function () {\r\n      WebApp.validateFor" +
"m(\'#frmCompanyInfo\');\r\n   });\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
