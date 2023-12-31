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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Layout.cshtml")]
    public partial class _Views_Shared__Layout_cshtml : WebApp.Core.BaseWebViewPage<dynamic>
    {
        public _Views_Shared__Layout_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n\r\n<html>\r\n<head>\r\n   <meta");

WriteLiteral(" charset=\"UTF-8\"");

WriteLiteral(" />\r\n   <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1, user-scalable=no\"");

WriteLiteral(">\r\n   <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge,chrome=1\"");

WriteLiteral(" />\r\n   <meta");

WriteLiteral(" name=\"apple-mobile-web-app-capable\"");

WriteLiteral(" content=\"yes\"");

WriteLiteral(" />\r\n   <meta");

WriteLiteral(" name=\"apple-mobile-web-app-title\"");

WriteLiteral(" content=\"Dashboard\"");

WriteLiteral(">\r\n   <meta");

WriteLiteral(" name=\"apple-mobile-web-app-status-bar-style\"");

WriteLiteral(" content=\"black\"");

WriteLiteral(">\r\n   <meta");

WriteLiteral(" name=\"mobile-web-app-capable\"");

WriteLiteral(" content=\"yes\"");

WriteLiteral(">\r\n   <meta");

WriteLiteral(" name=\"msapplication-tap-highlight\"");

WriteLiteral(" content=\"no\"");

WriteLiteral(" />\r\n   <meta");

WriteLiteral(" name=\"msapplication-TitleImage\"");

WriteAttribute("content", Tuple.Create(" content=\"", 580), Tuple.Create("\"", 703)
            
            #line 13 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 590), Tuple.Create<System.Object, System.Int32>(string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("/styles/favicons/48.png"))
            
            #line default
            #line hidden
, 590), false)
);

WriteLiteral(">\r\n   <meta");

WriteLiteral(" name=\"msapplication-TitleColor\"");

WriteLiteral(" content=\"#fff\"");

WriteLiteral(">\r\n   <meta");

WriteLiteral(" name=\"theme-color\"");

WriteLiteral(" content=\"blue\"");

WriteLiteral(">\r\n   <link");

WriteLiteral(" rel=\"manifest\"");

WriteLiteral(" href=\"/manifest.json\"");

WriteLiteral(" />\r\n\r\n   <title>");

            
            #line 18 "..\..\Views\Shared\_Layout.cshtml"
     Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" | ");

            
            #line 18 "..\..\Views\Shared\_Layout.cshtml"
                      Write(H("Quản lý trực tuyến"));

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n   <link");

WriteLiteral(" rel=\"shortcut icon\"");

WriteLiteral(" href=\"/favicon.ico\"");

WriteLiteral(" type=\"image/x-icon\"");

WriteLiteral(">\r\n   <link");

WriteLiteral(" rel=\"icon\"");

WriteLiteral(" href=\"/favicon.ico\"");

WriteLiteral(" type=\"image/x-icon\"");

WriteLiteral(">\r\n");

            
            #line 21 "..\..\Views\Shared\_Layout.cshtml"
   
            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\Shared\_Layout.cshtml"
     
      var app = System.Web.Configuration.WebConfigurationManager.AppSettings;
      var css = new List<StyleSheet>();
      var js = new List<Script>();
      if (!string.IsNullOrEmpty(app["ExtensionSuite"]))
      {
         foreach (var ext in app["ExtensionSuite"].Split(','))
         {
            css.Add(new StyleSheet() { ExtensionSuite = (ExtensionSuite)Enum.Parse(typeof(ExtensionSuite), ext) });
            js.Add(new Script() { ExtensionSuite = (ExtensionSuite)Enum.Parse(typeof(ExtensionSuite), ext) });
         }
      }
      if (!string.IsNullOrEmpty(app["ExtensionType"]))
      {
         foreach (var ext in app["ExtensionType"].Split(','))
         {
            css.Add(new StyleSheet() { ExtensionType = (ExtensionType)Enum.Parse(typeof(ExtensionType), ext) });
            js.Add(new Script() { ExtensionType = (ExtensionType)Enum.Parse(typeof(ExtensionType), ext) });
         }
      }
   
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("   ");

            
            #line 43 "..\..\Views\Shared\_Layout.cshtml"
Write(Html.DevExpress().GetStyleSheets(css.ToArray()));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("   ");

            
            #line 44 "..\..\Views\Shared\_Layout.cshtml"
Write(Html.DevExpress().GetScripts(js.ToArray()));

            
            #line default
            #line hidden
WriteLiteral("\r\n   <link");

WriteAttribute("href", Tuple.Create(" href=\"", 2112), Tuple.Create("\"", 2142)
, Tuple.Create(Tuple.Create("", 2119), Tuple.Create<System.Object, System.Int32>(Href("~/Styles/img.sprite.css")
, 2119), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n");

WriteLiteral("   ");

            
            #line 46 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("style", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n   <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
      if ('serviceWorker' in navigator) {
         navigator.serviceWorker
            .register('/sw.js')
            .then(function () {
               //console.log(""Service Worker registered successfully"");
            })
            .catch(function () {
               //console.log(""Service worker registration failed"")
            });
      }
   </script>
</head>

<body");

WriteLiteral(" style=\"user-select: none;\"");

WriteLiteral(">\r\n");

WriteLiteral("   ");

            
            #line 62 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("   ");

            
            #line 63 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("script", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n</body>\r\n</html>");

        }
    }
}
#pragma warning restore 1591
