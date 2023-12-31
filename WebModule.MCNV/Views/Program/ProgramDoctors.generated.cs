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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Program/ProgramDoctors.cshtml")]
    public partial class _Views_Program_ProgramDoctors_cshtml : WebApp.Core.BaseWebViewPage<Program>
    {
        public _Views_Program_ProgramDoctors_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Program\ProgramDoctors.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   st.Name = "grdDoctors";
   st.CallbackRouteValues = new { Action = "ProgramDoctors", DoctorsId = Model.DoctorsId };
   st.KeyFieldName = "Id";
   st.Width = Unit.Percentage(100);
   st.Settings.VerticalScrollableHeight = 250;
   st.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
   st.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;
   st.SettingsSearchPanel.CustomEditorName = "SearchDoctor";
   st.SettingsBehavior.AutoExpandAllGroups = true;
   st.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;

   st.Columns.Add(col =>
   {
      col.FieldName = "Facility";
      col.Caption = T("Đơn vị");
      col.GroupIndex = 0;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Name";
      col.Caption = T("Họ và tên");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Specialize";
      col.Caption = T("Lĩnh vực chuyên môn");
   });
}).Bind(Model.Doctors).GetHtml());

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
