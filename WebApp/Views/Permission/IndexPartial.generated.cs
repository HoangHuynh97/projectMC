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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Permission/IndexPartial.cshtml")]
    public partial class _Views_Permission_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<PermissionModel.ListInfo>
    {
        public _Views_Permission_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Permission\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   Html.DefaultGridSheet(st, Model.ShowFilter);

   st.Columns.Add(col =>
   {
      col.FieldName = "Name";
      col.Caption = T("Tên quyền sử dụng");
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "System";
      col.Caption = T("Hệ thống");
      col.ColumnType = MVCxGridViewColumnType.CheckBox;
      col.Width = 80;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Note";
      col.Caption = "Ghi chú";
      col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
   });

}).BindToLINQ("", "", (s, e) =>
{
   e.QueryableSource = Model.GetData();
   e.DefaultSorting = "System DESC, Name";
   e.KeyExpression = "Oid";
}).GetHtml());

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591