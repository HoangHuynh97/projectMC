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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/User/EditTemplate.cshtml")]
    public partial class _Views_User_EditTemplate_cshtml : WebApp.Core.BaseWebViewPage<UserModel>
    {
        public _Views_User_EditTemplate_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\User\EditTemplate.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"col-5_sm-12 grid\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 6 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.UserName));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 9 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.FullName));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 12 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.Address));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 15 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 18 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n");

            
            #line 20 "..\..\Views\User\EditTemplate.cshtml"
      
            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\User\EditTemplate.cshtml"
       if (Model.IsNew())
      {

            
            #line default
            #line hidden
WriteLiteral("         <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 23 "..\..\Views\User\EditTemplate.cshtml"
       Write(Html.EditorFor(m => m.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n");

WriteLiteral("         <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 26 "..\..\Views\User\EditTemplate.cshtml"
       Write(Html.EditorFor(m => m.Verify));

            
            #line default
            #line hidden
WriteLiteral("\r\n         </div>\r\n");

            
            #line 28 "..\..\Views\User\EditTemplate.cshtml"
      }

            
            #line default
            #line hidden
WriteLiteral("      <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 30 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.DevExpress().ComboBoxFor(m => m.Permission, st =>
    {
       st.Width = Unit.Percentage(100);
       st.Properties.Caption = T("Quyền sử dụng");
       st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
       st.Properties.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
       st.Properties.IncrementalFilteringDelay = 1000;
       st.Properties.DropDownStyle = DropDownStyle.DropDownList;
       st.Properties.ValueField = "Key";
       st.Properties.ValueType = typeof(int);
       st.Properties.TextField = "Value";
    }).BindList(Model.GetPermissions()).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 44 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.System));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"col-6_xs-12\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 47 "..\..\Views\User\EditTemplate.cshtml"
    Write(Html.EditorFor(m => m.Active));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-3_sm-12\"");

WriteLiteral(" style=\"min-height: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 51 "..\..\Views\User\EditTemplate.cshtml"
 Write(Html.DevExpress().BinaryImageFor(m => m.Image, st =>
      {
         st.Width = Unit.Percentage(100);
         st.Height = Unit.Percentage(100);
         st.CallbackRouteValues = new { Action = "UpdateUserImage" };
         st.Properties.ImageWidth = 300;
         st.Properties.ImageHeight = 300;
         st.Properties.ImageSizeMode = ImageSizeMode.FitProportional;
         st.Properties.EditingSettings.Enabled = true;
         st.Properties.EditingSettings.AllowDropOnPreview = true;
         st.Properties.EditingSettings.UploadSettings.UploadValidationSettings.MaxFileSize = 1000000;
      }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-4_sm-12\"");

WriteLiteral(" style=\"min-height: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 65 "..\..\Views\User\EditTemplate.cshtml"
 Write(Html.DevExpress().ListBox(st =>
 {
    st.Name = "CompanyIds";
    st.Width = Unit.Percentage(100);
    st.Height = Unit.Percentage(100);
    st.Properties.ValueField = "Key";
    st.Properties.ValueType = typeof(int);
    st.Properties.TextField = "Value";
    st.Properties.SelectionMode = ListEditSelectionMode.CheckColumn;
    st.PreRender = (s, e) =>
    {
       var me = (MVCxListBox)s;
       foreach (ListEditItem item in me.Items)
       {
          item.Selected = Model.CompanyIds.Contains((int)item.Value);
       }
    };
 }).BindList(Model.GetListCompany()).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("      ");

            
            #line 85 "..\..\Views\User\EditTemplate.cshtml"
 Write(Html.EditorFor(m => m.Note));

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
