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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Language/IndexPartial.cshtml")]
    public partial class _Views_Language_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<List<LanguageModel>>
    {
        public _Views_Language_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Language\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   st.Name = "grdSheet";
   st.Width = Unit.Percentage(100);
   st.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
   st.SettingsPager.PageSize = 50;
   st.CallbackRouteValues = new { Action = "IndexPartial" };
   st.KeyFieldName = "Keyword";
   st.SettingsEditing.BatchUpdateRouteValues = new { Action = "Update" };
   st.SettingsEditing.Mode = GridViewEditingMode.Batch;
   st.SettingsEditing.BatchEditSettings.StartEditAction = GridViewBatchStartEditAction.Click;
   st.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;

   st.Toolbars.Add(toolbar =>
   {
      toolbar.Name = "toolbarMain";
      toolbar.Enabled = true;
      toolbar.Position = GridToolbarPosition.Top;
      toolbar.ItemAlign = GridToolbarItemAlign.Left;
      toolbar.EnableAdaptivity = true;
      toolbar.Items.Add(tool =>
      {
         tool.Text = T("Lưu");
         tool.ToolTip = T("Lưu dữ liệu (F8)");
         tool.Image.IconID = IconID.SaveSave16x16;
         tool.Command = GridViewToolbarCommand.Update;
      });
      toolbar.Items.Add(tool =>
      {
         tool.Text = T("Làm mới");
         tool.ToolTip = T("Làm mới dữ liệu (F5)");
         tool.Image.IconID = IconID.ActionsRefresh16x16;
         tool.Command = GridViewToolbarCommand.Refresh;
      });
      toolbar.Items.Add(tool =>
      {
         tool.BeginGroup = true;
         tool.Command = GridViewToolbarCommand.Custom;
         tool.SetTemplateContent(c =>
         {
            Html.DevExpress().ButtonEdit(txt =>
            {
               txt.Name = "search";
               txt.Properties.NullText = T("Tìm kiếm");
               txt.Properties.NullTextDisplayMode = NullTextDisplayMode.UnfocusedAndFocused;
               var btn = txt.Properties.Buttons.Add();
               btn.Image.IconID = DevExpress.Web.ASPxThemes.IconID.FindFind16x16;
               btn.Position = ButtonsPosition.Left;

            }).Render();
         });
      });
      toolbar.Items.Add(tool =>
      {
         tool.Name = "close";
         tool.Text = T("Đóng");
         tool.Image.IconID = IconID.ActionsClose16x16;
         tool.ToolTip = T("Đóng chức năng này (ESC)");
         tool.Command = GridViewToolbarCommand.Custom;
         tool.BeginGroup = true;
      });
   });

   st.Columns.Add(col =>
   {
      col.FieldName = "Keyword";
      col.Caption = T("Keyword");
      col.SortAscending();
      col.ReadOnly = true;
      col.EditFormSettings.Visible = DefaultBoolean.False;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Value";
      col.Caption = WebApp.Core.Language.Current.Name;
      col.EditorProperties().TextBox(txt =>
      {
         txt.ValidationSettings.Display = Display.Dynamic;
      });
   });

   st.SettingsSearchPanel.CustomEditorName = "search";
   st.ClientSideEvents.BatchEditConfirmShowing = "function(s,e) {e.cancel=true;}";
   st.ClientSideEvents.ToolbarItemClick = "function(s,e) {e.item.name && WebApp.function[e.item.name] && WebApp.function[e.item.name].apply(WebApp);}";
   st.ClientSideEvents.BeginCallback = "function(s,e) {s._command = e.command;}";
   st.ClientSideEvents.EndCallback = "function(s,e) {if (s._command == 'UPDATEEDIT') WebApp.notify(WebApp.message.saved); s._command = ''; }";
}).Bind(Model).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591