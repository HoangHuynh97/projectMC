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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/Report.cshtml")]
    public partial class _Views_Shared_Report_cshtml : WebApp.Core.BaseWebViewPage<IEnumerable<KeyValuePair<string, List<KeyValuePair<int, string>>>>>
    {
        public _Views_Shared_Report_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr img documents3\"");

WriteLiteral("></div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">");

            
            #line 5 "..\..\Views\Shared\Report.cshtml"
                    Write(H("Báo cáo"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n      <div");

WriteLiteral(" style=\"margin-left: 1rem; flex: 1; max-width: 600px; font-family: Arial,sans-ser" +
"if;\"");

WriteLiteral(">\r\n         <select");

WriteLiteral(" id=\"cmbReport\"");

WriteLiteral(" style=\"width: 100%;\"");

WriteLiteral(" data-placeholder=\"");

            
            #line 7 "..\..\Views\Shared\Report.cshtml"
                                                                  Write(H("Chọn báo cáo..."));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(">\r\n            <option></option>\r\n");

            
            #line 9 "..\..\Views\Shared\Report.cshtml"
            
            
            #line default
            #line hidden
            
            #line 9 "..\..\Views\Shared\Report.cshtml"
             foreach (var g in Model)
            {

            
            #line default
            #line hidden
WriteLiteral("               <optgroup");

WriteAttribute("label", Tuple.Create(" label=\"", 541), Tuple.Create("\"", 558)
            
            #line 11 "..\..\Views\Shared\Report.cshtml"
, Tuple.Create(Tuple.Create("", 549), Tuple.Create<System.Object, System.Int32>(H(g.Key)
            
            #line default
            #line hidden
, 549), false)
);

WriteLiteral(">\r\n");

            
            #line 12 "..\..\Views\Shared\Report.cshtml"
                  
            
            #line default
            #line hidden
            
            #line 12 "..\..\Views\Shared\Report.cshtml"
                   foreach (var r in g.Value)
                  {

            
            #line default
            #line hidden
WriteLiteral("                     <option");

WriteAttribute("value", Tuple.Create(" value=\"", 658), Tuple.Create("\"", 672)
            
            #line 14 "..\..\Views\Shared\Report.cshtml"
, Tuple.Create(Tuple.Create("", 666), Tuple.Create<System.Object, System.Int32>(r.Key
            
            #line default
            #line hidden
, 666), false)
);

WriteLiteral(">");

            
            #line 14 "..\..\Views\Shared\Report.cshtml"
                                       Write(H(r.Value));

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 15 "..\..\Views\Shared\Report.cshtml"
                  }

            
            #line default
            #line hidden
WriteLiteral("               </optgroup>\r\n");

            
            #line 17 "..\..\Views\Shared\Report.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("         </select>\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 22 "..\..\Views\Shared\Report.cshtml"
 Write(Html.Partial("Preview"));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n   WebApp.function = {\r\n      reportId: 0,\r\n      toolRefresh: null,\r\n      to" +
"olFilter: null,\r\n      viewer: null,\r\n      title: \'");

            
            #line 32 "..\..\Views\Shared\Report.cshtml"
         Write(H("Báo cáo"));

            
            #line default
            #line hidden
WriteLiteral(@"',
      hotkey: {
         f5: function () { WebApp.function.refresh(); },
         esc: function () { WebApp.function.close();}
      },
      resize: function () {
         $('.page-content').height($('.page-container').height() - $('.page-header').height());
         this.viewer && this.viewer.SetHeight($('.page-content').innerHeight());
      },
      init: function () {
         $.when(function () {
            if (!$.fn.chosen) {
               $('<link/>', {
                  rel: 'stylesheet',
                  type: 'text/css',
                  href: 'https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.2/chosen.min.css'
               }).appendTo('head');
               return $.ajax({ url: 'https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.2/chosen.jquery.min.js', dataType: ""script"" });
            }
         }()).done(function () {
            $('#cmbReport').chosen({
               width: '100%',
               no_results_text: '");

            
            #line 54 "..\..\Views\Shared\Report.cshtml"
                            Write(H("Không tìm thấy: "));

            
            #line default
            #line hidden
WriteLiteral(@"'
            }).change(function () {
               var id = parseInt($(this).val()) || 0;
               if (!id) return;
               WebApp.function.OpenReport(id);
            });
         });
      },
      viewerInit: function (viewer) {
         this.viewer = viewer;
         this.resize();
         const tools = this.viewer.GetToolbar().enabledItems;
         this.toolRefresh = tools.find(function (it) { return it.name == 'refresh' });
         this.toolRefresh.SetVisible(false);
         this.toolFilter = tools.find(function (it) { return it.name == 'filter' });
         this.toolFilter.SetVisible(false);
      },
      refresh: function () {
         if (!this.reportId) return;
         this.OpenReport(this.reportId);
      },
      filter: function () {
         if (!this.reportId) return;
         this.ShowParameter(this.reportId);
      },
      close: WebApp.closeFunction.bind(WebApp),
      designer: function () {
         if (!this.reportId) return;
         window.open('");

            
            #line 82 "..\..\Views\Shared\Report.cshtml"
                 Write(Url.Action("Designer", "Report"));

            
            #line default
            #line hidden
WriteLiteral(@"/' + this.reportId, '_blank');
      },
      toolClick: function (s, e) {
         e.item.name && WebApp.function[e.item.name] && WebApp.function[e.item.name]();
      },
      OpenReport: function (id) {
         if (!id) return;
         var self = this;
         WebApp.ajax('");

            
            #line 90 "..\..\Views\Shared\Report.cshtml"
                 Write(Url.Action("OpenReport"));

            
            #line default
            #line hidden
WriteLiteral(@"', { id: id }).done(function (r) {
            self.toolRefresh.SetVisible(true);
            self.toolFilter.SetVisible(false);
            self.reportId = id;
            self.viewer.Refresh();
         }).fail(function () {
            $('#cmbReport').val(self.reportId).trigger('chosen:updated');
         });
      },
      ShowParameter: function (id) {
         if (!id) return;
         var self = this;
         self.toolRefresh.SetVisible(false);
         self.toolFilter.SetVisible(true);
         WebApp.showDialog({
            url: '");

            
            #line 105 "..\..\Views\Shared\Report.cshtml"
             Write(Url.Action("ShowParameter"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            data: { id: id },\r\n            icon: \'");

            
            #line 107 "..\..\Views\Shared\Report.cshtml"
              Write(IconID.ReportsParameters32x32);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            title: \'");

            
            #line 108 "..\..\Views\Shared\Report.cshtml"
               Write(H("Điều kiện lập báo cáo"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            width: 400,\r\n         }).done(function () {\r\n            self.rep" +
"ortId = id;\r\n            self.viewer.Refresh();\r\n            $(\'#cmbReport\').val" +
"(self.reportId).trigger(\'chosen:updated\');\r\n         });\r\n      }\r\n   };\r\n</scri" +
"pt>");

        }
    }
}
#pragma warning restore 1591
