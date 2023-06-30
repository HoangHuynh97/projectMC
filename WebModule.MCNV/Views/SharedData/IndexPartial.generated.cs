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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/SharedData/IndexPartial.cshtml")]
    public partial class _Views_SharedData_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<SharedData.ListInfo>
    {
        public _Views_SharedData_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\SharedData\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   Html.DefaultGridSheet(st, Model.ShowFilter);
   st.Toolbars[0].Items.RemoveAt(0);
   st.Toolbars[0].Items.RemoveAt(0);
   if (WindowWidthType >= WebApp.Constant.WindowWidth.WidthType.Small && WindowWidthType <= WebApp.Constant.WindowWidth.WidthType.Medium)
   {
      st.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
      st.Settings.ColumnMinWidth = 150;
   }

   st.Columns.Add(col =>
   {
      col.FieldName = "LocalDateRequest";
      col.Caption = T("Ngày yêu cầu");
      col.ColumnType = MVCxGridViewColumnType.DateEdit;
      col.PropertiesEdit.DisplayFormatString = "g";
      col.MaxWidth = WidthDateTime;
      col.SettingsHeaderFilter.Mode = GridHeaderFilterMode.DateRangePicker;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "UserRequest.FullName";
      col.Caption = T("Người yêu cầu");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "FacilityRequest.Name";
      col.Caption = T("Đơn vị yêu cầu");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "FacilityReceive.Name";
      col.Caption = T("Đơn vị nhận yêu cầu");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Patient.Code";
      col.Caption = T("Mã định danh");
      col.MaxWidth = 120;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Patient.Name";
      col.Caption = T("Bệnh nhân");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Accept";
      col.Caption = T("Đồng ý");
      col.ColumnType = MVCxGridViewColumnType.CheckBox;
      col.MaxWidth = 80;
      col.SetDataItemTemplateContent(container =>
      {
         var id = DataBinder.GetPropertyValue(container.DataItem, "Oid");
         if (id == null) return;
         var accept = (bool)DataBinder.GetPropertyValue(container.DataItem, "Accept");
         Html.DevExpress().CheckBox(chk =>
         {
            chk.Name = "Accept" + id;
            chk.Checked = accept;
            chk.Properties.ClientSideEvents.CheckedChanged = String.Format("function(s) {{WebApp.function.updateAccept({0}, s.GetValue());}}", id);
         }).Render();
      });
   });

}).BindToLINQ("", "", (s, e) =>
{
   e.QueryableSource = Model.GetData();
   e.DefaultSorting = "DateRequest desc";
   e.KeyExpression = "Oid";
}).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591