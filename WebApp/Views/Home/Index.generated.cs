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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/Index.cshtml")]
    public partial class _Views_Home_Index_cshtml : WebApp.Core.BaseWebViewPage<Home>
    {
        public _Views_Home_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Home\Index.cshtml"
  
   ViewBag.Title = T("Trang chủ");

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("style", () => {

WriteLiteral("\r\n   <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"https://cdnjs.cloudflare.com/ajax/libs/gridlex/2.7.1/gridlex.min.css\"");

WriteLiteral(" />\r\n   <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"https://cdnjs.cloudflare.com/ajax/libs/izitoast/1.1.5/css/iziToast.min.css" +
"\"");

WriteLiteral(@" />
   <style>
      * {
         box-sizing: border-box;
      }

      html, body {
         margin: 0;
         padding: 0;
         height: 100%;
         overflow: hidden;
      }

      .main-container {
         height: 100%;
      }

      .main-content {
         background-color: #ecf0f5;
         overflow: auto;
         padding: 0 20px;
      }

      .page-container {
         height: 100%;
      }

      .page-header {
         min-height: 50px;
      }

         .page-header .title {
            color: #1b6969;
            text-shadow: rgba(0, 0, 30, 0.08) 0px 3px 2px;
            text-transform: uppercase;
            font-size: 22px;
            font-weight: bold;
         }

      ");

WriteLiteral("@media screen and (max-width: 35.5em) {\r\n         .page-header .title {\r\n        " +
"    font-size: 14px;\r\n         }\r\n      }\r\n\r\n      .box {\r\n         min-height: " +
"90px;\r\n         background: #fff;\r\n         box-shadow: 0 2px 2px rgba(0,0,0,0.1" +
");\r\n         border-radius: 2px;\r\n         padding: 1em;\r\n      }\r\n\r\n      .mess" +
"agebox .img {\r\n         float: left;\r\n         margin: 0 10px 10px 0;\r\n      }\r\n" +
"\r\n      .messagebox .msg {\r\n         margin-top: 5px;\r\n      }\r\n\r\n      .dialogf" +
"ooter {\r\n         padding: 5px;\r\n         text-align: center;\r\n      }\r\n\r\n      " +
".mt {\r\n         margin-top: 0.5rem !important;\r\n      }\r\n\r\n      .mb {\r\n        " +
" margin-bottom: 0.5rem !important;\r\n      }\r\n\r\n      .ml {\r\n         margin-left" +
": 0.5rem !important;\r\n      }\r\n\r\n      .mr {\r\n         margin-right: 0.5rem !imp" +
"ortant;\r\n      }\r\n\r\n      .pt {\r\n         padding-top: 0.5rem !important;\r\n     " +
" }\r\n\r\n      .pb {\r\n         padding-bottom: 0.5rem !important;\r\n      }\r\n\r\n     " +
" .pl {\r\n         padding-left: 0.5rem !important;\r\n      }\r\n\r\n      .pr {\r\n     " +
"    padding-right: 0.5rem !important;\r\n      }\r\n\r\n      .dialogfooter .dxbButton" +
"Sys {\r\n         margin: 0 5px;\r\n      }\r\n\r\n      .hidden {\r\n         display: no" +
"ne;\r\n      }\r\n\r\n      .companyName {\r\n         font-family: Garamond, serif;\r\n  " +
"       line-height: 1em;\r\n         color: #42d9ff;\r\n         font-weight: bold;\r" +
"\n         font-size: 60px;\r\n         text-shadow: 0px 0px 0 rgb(42,193,231),1px " +
"1px 0 rgb(29,180,218),2px 2px 0 rgb(15,166,204),3px 3px 0 rgb(1,152,190), 4px 4p" +
"x 0 rgb(-13,138,176),5px 5px 4px rgba(0,0,0,0.6),5px 5px 1px rgba(0,0,0,0.5),0px" +
" 0px 4px rgba(0,0,0,.2);\r\n         margin-bottom: 30px;\r\n      }\r\n\r\n      .compa" +
"nyAddress {\r\n         font-size: 20px;\r\n         color: #18a49a;\r\n         font-" +
"weight: bold;\r\n      }\r\n\r\n      .iziToast-img {\r\n         width: 32px !important" +
";\r\n         height: 32px !important;\r\n         margin-top: -17px !important;\r\n  " +
"       left: -5px !important;\r\n      }\r\n\r\n      .current-active-menu {\r\n        " +
" background-color: #f2ca58 !important;\r\n      }\r\n\r\n      #grdSheet .dxm-side-men" +
"u-mode {\r\n         float: left;\r\n         margin: 5px;\r\n      }\r\n\r\n         #grd" +
"Sheet .dxm-side-menu-mode ~ .dxgvSearchPanel_Moderno,\r\n         #grdSheet .dxm-s" +
"ide-menu-mode ~ .dxgvSearchPanel_Office365,\r\n         #grdSheet .dxm-side-menu-m" +
"ode ~ .dxgvSearchPanel_iOS,\r\n         #grdSheet .dxm-side-menu-mode ~ .dxgvSearc" +
"hPanel_MaterialCompact {\r\n            margin-left: 50px;\r\n            padding-to" +
"p: 5px;\r\n         }\r\n\r\n      .dxmodalSys.dxdd-root .dxpc-mainDiv.dxdd-list table" +
"[class^=\"dxeListBox\"] {\r\n         color: inherit!important;\r\n      }\r\n\r\n      .d" +
"xfc-nd img[class^=\"dxEditors_fcremove_\"] {\r\n         margin-left: 40px!important" +
";\r\n      }\r\n   </style>\r\n\r\n");

});

DefineSection("script", () => {

WriteLiteral("\r\n   <script");

WriteLiteral(" src=\"https://cdnjs.cloudflare.com/ajax/libs/izitoast/1.1.5/js/iziToast.min.js\"");

WriteLiteral("></script>\r\n   <script");

WriteLiteral(" src=\"https://cdnjs.cloudflare.com/ajax/libs/signalr.js/2.4.0/jquery.signalR.min." +
"js\"");

WriteLiteral("></script>\r\n   <script");

WriteLiteral(" src=\"/signalr/js\"");

WriteLiteral("></script>\r\n");

});

WriteLiteral("<div");

WriteLiteral(" class=\"main-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"main-header\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 171 "..\..\Views\Home\Index.cshtml"
 Write(Html.Partial("MainMenu"));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"main-content\"");

WriteLiteral(">\r\n\r\n   </div>\r\n</div>\r\n");

            
            #line 177 "..\..\Views\Home\Index.cshtml"
Write(Html.Partial("Common"));

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
