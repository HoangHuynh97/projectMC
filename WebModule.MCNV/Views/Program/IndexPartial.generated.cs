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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Program/IndexPartial.cshtml")]
    public partial class _Views_Program_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<Program.ListInfo>
    {
        public _Views_Program_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Program\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   Html.DefaultGridSheet(st, Model.ShowFilter);
   if (WindowWidthType >= WebApp.Constant.WindowWidth.WidthType.Small && WindowWidthType <= WebApp.Constant.WindowWidth.WidthType.Medium)
   {
      st.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
      st.Settings.ColumnMinWidth = 100;
   }

   st.Columns.Add(col =>
   {
      col.FieldName = "Name";
      col.Caption = T("Tên khóa học");
      col.MinWidth = 200;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "NameEnglish";
      col.Caption = T("Tên tiếng Anh");
      col.MinWidth = 200;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Project.Name";
      col.Caption = T("Dự án");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Specialize.Name";
      col.Caption = T("Chuyên môn");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "ProgramType";
      col.Caption = T("Loại khóa học");
      col.ColumnType = MVCxGridViewColumnType.ComboBox;
      col.EditorProperties().ComboBox(cmb =>
      {
         cmb.Items.Add(T("Huấn luyện"), (int)ProgramTypeEnum.Coaching);
         cmb.Items.Add(T("Tập huấn"), (int)ProgramTypeEnum.ShortTime);
         cmb.Items.Add(T("Đào tạo dài hạn"), (int)ProgramTypeEnum.LongTimeLongTerm);
         cmb.Items.Add(T("Đào tạo trung hạn"), (int)ProgramTypeEnum.LongTimeMidTerm);
         cmb.Items.Add(T("Đào tạo liên tục"), (int)ProgramTypeEnum.LongTimeContinuous);
         cmb.Items.Add(T("Đào tạo chuyên sâu"), (int)ProgramTypeEnum.LongTimeIntensive);
         cmb.ValueType = typeof(int);
      });
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "TrainingTime";
      col.Caption = T("Thời lượng");
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Institution.Name";
      col.Caption = T("Đơn vị đào tạo");
      col.MinWidth = 200;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "DateStart";
      col.Caption = T("Ngày khai giảng");
      col.Width = 150;
      col.ColumnType = MVCxGridViewColumnType.DateEdit;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "DateEnd";
      col.Caption = T("Ngày kết thúc");
      col.Width = 120;
      col.ColumnType = MVCxGridViewColumnType.DateEdit;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "DoctorsCount";
      col.Caption = T("Số học viên");
      col.Width = 100;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Note";
      col.Caption = T("Ghi chú");
   });

}).BindToLINQ("", "", (s, e) =>
{
   e.QueryableSource = Model.GetData();
   e.DefaultSorting = "DateCreate desc";
   e.KeyExpression = "Oid";
}).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
