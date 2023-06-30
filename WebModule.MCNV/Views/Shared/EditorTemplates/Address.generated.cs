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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Address.cshtml")]
    public partial class _Views_Shared_EditorTemplates_Address_cshtml_ : WebApp.Core.BaseWebViewPage<AddressBase>
    {
        public _Views_Shared_EditorTemplates_Address_cshtml_()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 3 "..\..\Views\Shared\EditorTemplates\Address.cshtml"
                         Write(Html.EditorFor(m => m.Province));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 4 "..\..\Views\Shared\EditorTemplates\Address.cshtml"
                         Write(Html.EditorFor(m => m.District));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div");

WriteLiteral(" class=\"col-3_md-4_xs-12\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Shared\EditorTemplates\Address.cshtml"
                         Write(Html.EditorFor(m => m.Ward));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div");

WriteLiteral(" class=\"col-3_md-12_xs-12\"");

WriteLiteral(">");

            
            #line 6 "..\..\Views\Shared\EditorTemplates\Address.cshtml"
                          Write(Html.EditorFor(m => m.Address));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
   $(function () {
      Province.ValueChanged.AddHandler(function () {
         District.SetValue(null);
         Ward.SetValue(null);
         District.PerformCallback();
         Ward.PerformCallback();
      });
      District.BeginCallback.AddHandler(function (_, e) {
         e.customArgs.province = Province.GetValue();
      });
      District.ValueChanged.AddHandler(function () {
         Ward.SetValue(null);
         Ward.PerformCallback();
      });
      Ward.BeginCallback.AddHandler(function (_, e) {
         e.customArgs.district = District.GetValue();
      });
   });

   function WardRequired() {
      return !!Ward.listBox.GetItemCount();
   }
</script>
");

        }
    }
}
#pragma warning restore 1591