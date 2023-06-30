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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Attachment/Viewer.cshtml")]
    public partial class _Views_Attachment_Viewer_cshtml : WebApp.Core.BaseWebViewPage<Attachment>
    {
        public _Views_Attachment_Viewer_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Attachment\Viewer.cshtml"
  
   Layout = "";
   string url = Url.Action("Download", "Attachment", new { Id = Model.UniqueId, SaveAs = Model.SaveAs });

            
            #line default
            #line hidden
WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n<head>\r\n   <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n   <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1\"");

WriteLiteral(">\r\n   <title>");

            
            #line 11 "..\..\Views\Attachment\Viewer.cshtml"
     Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n</head>\r\n<body>\r\n");

            
            #line 14 "..\..\Views\Attachment\Viewer.cshtml"
   
            
            #line default
            #line hidden
            
            #line 14 "..\..\Views\Attachment\Viewer.cshtml"
    if (Model.Type == AttachmentType.Image)
   {

            
            #line default
            #line hidden
WriteLiteral("      <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"https://cdnjs.cloudflare.com/ajax/libs/viewerjs/1.10.5/viewer.min.css\"");

WriteLiteral(" integrity=\"sha512-3NbO5DhK9LuM6wu+IZvV5AYXxqSmj/lfLLmHMV9t18ij+MfmhhxeYEunHllEu+" +
"TFJ4tJM5D0DhazM2EGGGvNmQ==\"");

WriteLiteral(" crossorigin=\"anonymous\"");

WriteLiteral(" referrerpolicy=\"no-referrer\"");

WriteLiteral(" />\r\n");

WriteLiteral("      <script");

WriteLiteral(" src=\"https://cdnjs.cloudflare.com/ajax/libs/viewerjs/1.10.5/viewer.min.js\"");

WriteLiteral(" integrity=\"sha512-i5q29evO2Z4FHGCO+d5VLrwgre/l+vaud5qsVqQbPXvHmD9obORDrPIGFpP2+e" +
"p+HY+z41kAmVFRHqQAjSROmA==\"");

WriteLiteral(" crossorigin=\"anonymous\"");

WriteLiteral(" referrerpolicy=\"no-referrer\"");

WriteLiteral("></script>\r\n");

WriteLiteral("      <script>\r\n         window.addEventListener(\'DOMContentLoaded\', function () " +
"{\r\n            var image = new Image();\r\n\r\n            image.src = \'");

            
            #line 22 "..\..\Views\Attachment\Viewer.cshtml"
                    Write(url);

            
            #line default
            #line hidden
WriteLiteral(@"';

            var viewer = new Viewer(image, {
               button: false,
               navbar: false,
               title: false,
               toolbar: {
                  zoomIn: true,
                  zoomOut: true,
                  oneToOne: true,
                  reset: true,
                  rotateLeft: true,
                  rotateRight: true,
                  flipHorizontal: true,
                  flipVertical: true,
               },
               hidden: function () {
                  viewer.destroy();
               },
            });

            viewer.show();
         });

      </script>
");

            
            #line 47 "..\..\Views\Attachment\Viewer.cshtml"
   }
   else if (Model.Type == AttachmentType.PDF)
   {

            
            #line default
            #line hidden
WriteLiteral("      <script");

WriteLiteral(" src=\"https://cdnjs.cloudflare.com/ajax/libs/pdfobject/2.2.8/pdfobject.min.js\"");

WriteLiteral(" integrity=\"sha512-MoP2OErV7Mtk4VL893VYBFq8yJHWQtqJxTyIAsCVKzILrvHyKQpAwJf9noILcz" +
"N6psvXUxTr19T5h+ndywCoVw==\"");

WriteLiteral(" crossorigin=\"anonymous\"");

WriteLiteral(" referrerpolicy=\"no-referrer\"");

WriteLiteral("></script>\r\n");

WriteLiteral("      <script>\r\n         window.addEventListener(\'DOMContentLoaded\', function () " +
"{\r\n            PDFObject.embed(\"");

            
            #line 53 "..\..\Views\Attachment\Viewer.cshtml"
                        Write(url);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n         });\r\n      </script>\r\n");

            
            #line 56 "..\..\Views\Attachment\Viewer.cshtml"
   }
   else if (Model.Type == AttachmentType.Video)
   {

            
            #line default
            #line hidden
WriteLiteral(@"      <style>
         html, body {
            height: 100%;
            margin: 0;
            padding: 0;
         }

         .video-container {
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
         }

            .video-container video {
               width: 100%;
               max-width: 100%;
               max-height: 100%;
            }
      </style>
");

WriteLiteral("      <div");

WriteLiteral(" class=\"video-container\"");

WriteLiteral(">\r\n         <video controls>\r\n            <source");

WriteAttribute("src", Tuple.Create(" src=\"", 2842), Tuple.Create("\"", 2852)
            
            #line 82 "..\..\Views\Attachment\Viewer.cshtml"
, Tuple.Create(Tuple.Create("", 2848), Tuple.Create<System.Object, System.Int32>(url
            
            #line default
            #line hidden
, 2848), false)
);

WriteAttribute("type", Tuple.Create(" type=\"", 2853), Tuple.Create("\"", 2878)
            
            #line 82 "..\..\Views\Attachment\Viewer.cshtml"
, Tuple.Create(Tuple.Create("", 2860), Tuple.Create<System.Object, System.Int32>(Model.ContentType
            
            #line default
            #line hidden
, 2860), false)
);

WriteLiteral(" />\r\n            Your browser does not support the video tag.\r\n         </video>\r" +
"\n      </div>\r\n");

            
            #line 86 "..\..\Views\Attachment\Viewer.cshtml"
   }
   else if (Model.Type == AttachmentType.Audio)
   {

            
            #line default
            #line hidden
WriteLiteral(@"      <style>
         html, body {
            height: 100%;
            margin: 0;
            padding: 0;
         }

         .audio-container {
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
         }

            .audio-container audio {
               width: 80%;
               max-width: 800px;
            }
      </style>
");

WriteLiteral("      <div");

WriteLiteral(" class=\"audio-container\"");

WriteLiteral(">\r\n         <audio controls>\r\n            <source");

WriteAttribute("src", Tuple.Create(" src=\"", 3585), Tuple.Create("\"", 3595)
            
            #line 111 "..\..\Views\Attachment\Viewer.cshtml"
, Tuple.Create(Tuple.Create("", 3591), Tuple.Create<System.Object, System.Int32>(url
            
            #line default
            #line hidden
, 3591), false)
);

WriteAttribute("type", Tuple.Create(" type=\"", 3596), Tuple.Create("\"", 3621)
            
            #line 111 "..\..\Views\Attachment\Viewer.cshtml"
, Tuple.Create(Tuple.Create("", 3603), Tuple.Create<System.Object, System.Int32>(Model.ContentType
            
            #line default
            #line hidden
, 3603), false)
);

WriteLiteral(" />\r\n            Your browser does not support the audio tag.\r\n         </audio>\r" +
"\n      </div>\r\n");

            
            #line 115 "..\..\Views\Attachment\Viewer.cshtml"
   }

            
            #line default
            #line hidden
WriteLiteral("</body>\r\n</html>");

        }
    }
}
#pragma warning restore 1591
