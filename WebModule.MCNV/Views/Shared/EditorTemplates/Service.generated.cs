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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Service.cshtml")]
    public partial class _Views_Shared_EditorTemplates_Service_cshtml_ : WebApp.Core.BaseWebViewPage<int?>
    {
        public _Views_Shared_EditorTemplates_Service_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Shared\EditorTemplates\Service.cshtml"
  
   var caption = T(ViewData.ModelMetadata.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 6 "..\..\Views\Shared\EditorTemplates\Service.cshtml"
Write(Html.DevExpress().ComboBoxFor(m => m, st =>
{
   st.DefaultSettings();
   st.Properties.ValueField = "Oid";
   st.Properties.ValueType = typeof(int);
   st.Properties.Caption = caption;
   if (ViewData.ModelMetadata.IsRequired)
      st.Properties.CaptionSettings.RequiredMarkDisplayMode = EditorRequiredMarkMode.Required;
   st.Properties.TextField = "Name";
}).BindList(Service.ComboboxData()).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
