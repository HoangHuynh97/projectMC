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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Phone.cshtml")]
    public partial class _Views_Shared_EditorTemplates_Phone_cshtml_ : WebApp.Core.BaseWebViewPage<string>
    {
        public _Views_Shared_EditorTemplates_Phone_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
  
   var caption = T(ViewData.ModelMetadata.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
Write(Html.DevExpress().TextBoxFor(m => m, txt =>
{
   txt.DefaultSettings();
   txt.Properties.Caption = caption;
   if (ViewData.ModelMetadata.IsRequired)
      txt.Properties.CaptionSettings.RequiredMarkDisplayMode = EditorRequiredMarkMode.Required;
   txt.Properties.NullText = "0xxx.xxx.xxx/0xxx.xxx.xxx";
}).GetHtml());

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
