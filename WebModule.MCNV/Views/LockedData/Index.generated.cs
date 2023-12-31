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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/LockedData/Index.cshtml")]
    public partial class _Views_LockedData_Index_cshtml : WebApp.Core.BaseWebViewPage<LockedData>
    {
        public _Views_LockedData_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr img keys\"");

WriteLiteral("></div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 7 "..\..\Views\LockedData\Index.cshtml"
    Write(H("Khóa dữ liệu"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n      <form");

WriteLiteral(" id=\"frmLockedData\"");

WriteAttribute("action", Tuple.Create(" action=\"", 274), Tuple.Create("\"", 303)
            
            #line 11 "..\..\Views\LockedData\Index.cshtml"
, Tuple.Create(Tuple.Create("", 283), Tuple.Create<System.Object, System.Int32>(Url.Action("Index")
            
            #line default
            #line hidden
, 283), false)
);

WriteLiteral(">\r\n         <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"LockedOption\"");

WriteLiteral(" name=\"Option\"");

WriteAttribute("value", Tuple.Create(" value=\"", 368), Tuple.Create("\"", 389)
            
            #line 12 "..\..\Views\LockedData\Index.cshtml"
, Tuple.Create(Tuple.Create("", 376), Tuple.Create<System.Object, System.Int32>(Model.Option
            
            #line default
            #line hidden
, 376), false)
);

WriteLiteral(" />\r\n         <div");

WriteLiteral(" class=\"box\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"mb\"");

WriteLiteral(">\r\n");

WriteLiteral("               ");

            
            #line 15 "..\..\Views\LockedData\Index.cshtml"
          Write(Html.DevExpress().RadioButton(st =>
          {
             st.Name = "radLockNone";
             st.GroupName = "LockedOption";
             st.Text = T("Không sử dụng tính năng khóa dữ liệu");
             st.Checked = Model.Option == LockedOption.None;
             st.Properties.ClientSideEvents.CheckedChanged = "function() {$('#LockedOption').val(" + (int)LockedOption.None + ");}";
          }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"mb grid-middle-noGutter-noBottom\"");

WriteLiteral(">\r\n               <div");

WriteLiteral(" class=\"col-auto\"");

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 26 "..\..\Views\LockedData\Index.cshtml"
             Write(Html.DevExpress().RadioButton(st =>
             {
                st.Name = "radLockDate";
                st.GroupName = "LockedOption";
                st.Text = T("Khóa dữ liệu được tạo từ ngày này");
                st.Checked = Model.Option == LockedOption.ToDate;
                st.Properties.ClientSideEvents.CheckedChanged = "function() {$('#LockedOption').val(" + (int)LockedOption.ToDate + ");}";
             }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n               </div>\r\n               <div");

WriteLiteral(" class=\"col-auto ml\"");

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 36 "..\..\Views\LockedData\Index.cshtml"
             Write(Html.DevExpress().DateEditFor(m => m.OptionDate, st =>
             {
                st.Width = 120;
                st.Properties.MinDate = DateTime.Today.AddDays(-30);
                st.Properties.MaxDate = DateTime.Today;
                st.ShowModelErrors = true;
                st.Properties.ValidationSettings.Display = Display.Dynamic;
                st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                st.Properties.ShowOutOfRangeWarning = false;
             }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n               </div>\r\n               <div");

WriteLiteral(" class=\"col-auto ml\"");

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 48 "..\..\Views\LockedData\Index.cshtml"
             Write(Html.DevExpress().Label(st => st.Text = T("trở về trước")).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n               </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"mb grid-middle-noGutter-noBottom\"");

WriteLiteral(">\r\n               <div");

WriteLiteral(" class=\"col-auto\"");

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 53 "..\..\Views\LockedData\Index.cshtml"
             Write(Html.DevExpress().RadioButton(st =>
             {
                st.Name = "radLockSpan";
                st.GroupName = "LockedOption";
                st.Text = T("Dữ liệu tự động khóa sau khi tạo");
                st.Checked = Model.Option == LockedOption.DaySpan;
                st.Properties.ClientSideEvents.CheckedChanged = "function() {$('#LockedOption').val(" + (int)LockedOption.DaySpan + ");}";
             }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n               </div>\r\n               <div");

WriteLiteral(" class=\"col-auto ml\"");

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 63 "..\..\Views\LockedData\Index.cshtml"
             Write(Html.DevExpress().SpinEditFor(m => m.OptionSpan, st =>
             {
                st.Width = 60;
                st.Properties.MinValue = 1;
                st.Properties.MaxValue = 30;
                st.ShowModelErrors = true;
                st.Properties.ValidationSettings.Display = Display.Dynamic;
                st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                st.Properties.ShowOutOfRangeWarning = false;
             }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n               </div>\r\n               <div");

WriteLiteral(" class=\"col-auto ml\"");

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 75 "..\..\Views\LockedData\Index.cshtml"
             Write(Html.DevExpress().Label(st => st.Text = T("ngày")).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n               </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"\"");

WriteLiteral(" style=\"margin-top: 40px;\"");

WriteLiteral(">\r\n");

WriteLiteral("               ");

            
            #line 79 "..\..\Views\LockedData\Index.cshtml"
          Write(Html.DevExpress().Button(st =>
          {
             st.Name = "btnSaveLockedData";
             st.Text = T("Lưu thay đổi");
             st.Images.Image.IconID = IconID.SaveSave16x16;
             st.ClientSideEvents.Click = "function() {onSaveLockedData();}";
          }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n         </div>\r\n      </form>\r\n   </div>\r\n</div>\r\n\r\n\r\n<scr" +
"ipt>\r\n   $(function () {\r\n      WebApp.validateForm(\'#frmLockedData\');\r\n   });\r\n" +
"   function CheckOptionDate() {\r\n      return +$(\'#LockedOption\').val() == ");

            
            #line 98 "..\..\Views\LockedData\Index.cshtml"
                                      Write((int)LockedOption.ToDate);

            
            #line default
            #line hidden
WriteLiteral(";\r\n   }\r\n   function CheckOptionSpan() {\r\n      return +$(\'#LockedOption\').val() " +
"== ");

            
            #line 101 "..\..\Views\LockedData\Index.cshtml"
                                      Write((int)LockedOption.DaySpan);

            
            #line default
            #line hidden
WriteLiteral(";\r\n   }\r\n   function onSaveLockedData() {\r\n      if (!WebApp.validForm(\'#frmLocke" +
"dData\')) return;\r\n      WebApp.submit(\'#frmLockedData\').done(function () {\r\n    " +
"     WebApp.notify(\'");

            
            #line 106 "..\..\Views\LockedData\Index.cshtml"
                   Write(H("Đã cập nhật thành công"));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n      });\r\n   }\r\n   WebApp.function = {\r\n      title: \'");

            
            #line 110 "..\..\Views\LockedData\Index.cshtml"
         Write(H("Khóa dữ liệu"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n   };\r\n</script>");

        }
    }
}
#pragma warning restore 1591
