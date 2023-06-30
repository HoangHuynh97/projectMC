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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/User/IndexPartial.cshtml")]
    public partial class _Views_User_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<UserModel.ListInfo>
    {
        public _Views_User_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\User\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   var tool = new MVCxGridViewToolbarItem();
   tool.Name = "password";
   tool.Text = T("Đổi mật khẩu");
   tool.ToolTip = T("Đặt lại mật khẩu mới");
   tool.Image.IconID = IconID.BusinessobjectsBouser16x16;
   tool.Command = GridViewToolbarCommand.Custom;
   tool.Visible = CheckPermission("Đổi mật khẩu");

   Html.DefaultGridSheet(st, Model.ShowFilter, true, new MVCxGridViewToolbarItem[] { tool });

   st.Columns.Add(col =>
   {
      col.FieldName = "UserName";
      col.Caption = T("Tên đăng nhập");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "FullName";
      col.Caption = T("Họ và tên");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Address";
      col.Caption = T("Địa chỉ");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Email";
      col.Caption = T("Email");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Phone";
      col.Caption = T("Điện thoại");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "System";
      col.Caption = T("Hệ thống");
      col.Width = 80;
      col.ColumnType = MVCxGridViewColumnType.CheckBox;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Admin";
      col.Caption = T("Toàn quyền");
      col.Width = 80;
      col.ColumnType = MVCxGridViewColumnType.CheckBox;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Permission.Name";
      col.Caption = T("Phân quyền");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Active";
      col.Caption = T("Sử dụng");
      col.Width = 80;
      col.ColumnType = MVCxGridViewColumnType.CheckBox;
   });
   st.PreviewFieldName = "Note";
   st.Settings.ShowPreview = true;

}).BindToLINQ("", "", (s, e) =>
{
   e.QueryableSource = Model.GetData();
   e.DefaultSorting = "UserName";
   e.KeyExpression = "Oid";
}).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591