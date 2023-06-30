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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/AuditLog/IndexPartial.cshtml")]
    public partial class _Views_AuditLog_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<AuditLog>
    {
        public _Views_AuditLog_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\AuditLog\IndexPartial.cshtml"
  
   var data = Model.GetData();
   var edit = data.Where(p => p.AuditType == "Sửa").Select(p => p.Id).ToList();

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 6 "..\..\Views\AuditLog\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   Html.DefaultGridSheet(st, Model.ShowFilter, false);
   st.KeyFieldName = "Id";

   st.SettingsDetail.ShowDetailRow = true;
   st.SetDetailRowTemplateContent(c =>
   {
      var id = Convert.ToInt32(DataBinder.Eval(c.DataItem, "Id"));
      Html.DevExpress().GridView(grd =>
      {
         grd.Name = "grdAudit" + id;
         grd.Width = Unit.Percentage(100);
         grd.SettingsBehavior.AllowSort = false;
         grd.SettingsBehavior.AllowDragDrop = false;
         grd.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
         grd.SettingsDetail.MasterGridName = "grdSheet";
         grd.KeyFieldName = "Id";
         grd.Columns.Add("PropertyName", T("Thuộc tính"));
         grd.Columns.Add("OldValue", T("Giá trị cũ"));
         grd.Columns.Add("NewValue", T("Giá trị mới"));
      }).Bind(AuditLog.GetDetails(id)).Render();
   });
   st.Init = (sender, e) =>
   {
      MVCxGridView grid = (MVCxGridView)sender;
      grid.DetailRowGetButtonVisibility += new ASPxGridViewDetailRowButtonEventHandler((s, evargs) =>
      {
         if (!edit.Contains(Convert.ToInt32(evargs.KeyValue)))
         {
            evargs.ButtonState = GridViewDetailRowButtonState.Hidden;
         }
      });
   };


   st.Columns.Add(col =>
   {
      col.FieldName = "AuditDate";
      col.Caption = T("Ngày cập nhật");
      col.Width = 200;
      col.ColumnType = MVCxGridViewColumnType.DateEdit;
      col.PropertiesEdit.DisplayFormatString = "g";
      col.Settings.AllowHeaderFilter = DefaultBoolean.False;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "UserName";
      col.Caption = T("Người dùng");
      col.Width = 100;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "AuditType";
      col.Caption = T("Thao tác");
      col.Width = 100;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Description";
      col.Caption = T("Cập nhật dữ liệu");
   });

   st.ClientSideEvents.ToolbarItemClick = "WebApp.function.toolClick";
   st.ClientSideEvents.BeginCallback = "function(s,e) {e.customArgs.showFilter = s._showFilter; e.customArgs.TuNgay = TuNgay.GetText(); e.customArgs.DenNgay = DenNgay.GetText();}";
}).Bind(data).GetHtml());

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591