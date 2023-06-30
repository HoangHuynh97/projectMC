using DevExpress.Web;
using DevExpress.Web.ASPxThemes;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.Core;

namespace System.Web.Mvc
{
   public static class Helpers
   {
      //check permission base on Claims
      public static bool CheckPermission(this HttpContextBase httpContext, string module, string function, string logic = "")
      {
         if (!httpContext.User.Identity.IsAuthenticated)
            return false;
         var fns = WebApp.Core.Menu.Functions;
         if (!fns.ContainsKey(module))
            return false;
         if (!fns[module].ContainsKey(function))
            return false;
         if (fns[module][function].Roles.Count > 0)
         {
            var inRole = false;
            foreach (var r in fns[module][function].Roles)
               if (httpContext.User.IsInRole(r))
               {
                  inRole = true;
                  break;
               }
            if (!inRole)
               return false;
         }
         if (fns[module][function].Users.Count > 0)
         {
            if (!fns[module][function].Users.Contains(httpContext.User.Identity.Name))
               return false;
         }
         if (httpContext.User.IsInRole(WebApp.Constant.Role.Admin))
            return true;
         if (!(httpContext.User.Identity is ClaimsIdentity))
            return false;
         string name = $"{WebApp.Constant.Claim.Permission}{module}/{function}";
         var c = ((ClaimsIdentity)httpContext.User.Identity).Claims.FirstOrDefault(p => p.Type == name);
         if (c == null) return false;
         if (!string.IsNullOrEmpty(logic))
            return c.Value.Split(',').Contains(logic);
         return true;
      }

      //check can open report
      public static bool CheckReport(this HttpContextBase httpContext, int id)
      {
         if (!httpContext.User.Identity.IsAuthenticated)
            return false;
         if (httpContext.User.IsInRole(WebApp.Constant.Role.Admin))
            return true;
         if (!(httpContext.User.Identity is ClaimsIdentity))
            return false;
         var c = ((ClaimsIdentity)httpContext.User.Identity).Claims.FirstOrDefault(p => p.Type == WebApp.Constant.Claim.OpenReport);
         if (c == null || string.IsNullOrEmpty(c.Value)) return false;
         return c.Value.Split(',').Contains(id.ToString());
      }

      public static IHtmlString Icon(this HtmlHelper html, string IconID)
      {
         return html.Raw($"<div class='dxIcon_{IconID} dx-vam'></div>");
      }

      public static void DefaultGridSheet(this HtmlHelper html, GridViewSettings st, bool showFilter,
         bool editable = true,
         IEnumerable<MVCxGridViewToolbarItem> rootTools = null,
         IEnumerable<MVCxGridViewToolbarItem> advancedTools = null
         )
      {
         var controller = html.ViewContext.Controller.GetType();
         var module = controller.Assembly.ManifestModule.Name;
         var function = controller.GetCustomAttributes(typeof(FunctionAttribute), false)[0] as FunctionAttribute;
         var windowWidth = Convert.ToInt32(html.ViewContext.RequestContext.HttpContext.Request.Headers["WindowWidth"]);

         st.Name = "grdSheet";
         st.Width = Unit.Percentage(100);
         st.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
         st.SettingsPager.PageSize = 50;
         st.SettingsPager.AlwaysShowPager = true;
         st.CallbackRouteValues = new { Action = "IndexPartial" };
         st.KeyFieldName = "Oid";
         st.SettingsBehavior.AllowFocusedRow = true;
         st.SettingsExport.EnableClientSideExportAPI = true;
         st.SettingsExport.FileName = function.Name;
         st.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
         st.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCellsWindowLimit;
         st.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = WebApp.Constant.WindowWidth.ExtraSmall;
         st.SettingsSearchPanel.Delay = 800;
         st.SettingsSearchPanel.Visible = WebApp.Constant.WindowWidth.CurrentType <= WebApp.Constant.WindowWidth.WidthType.Small;
         st.SettingsText.SearchPanelEditorNullText = Language.T("Tìm kiếm");
         st.Settings.ShowHeaderFilterButton = showFilter;
         if (WebApp.Constant.WindowWidth.CurrentType == WebApp.Constant.WindowWidth.WidthType.ExtraSmall && !showFilter)
            st.Settings.ShowColumnHeaders = false;
         st.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

         st.BeforeGetCallbackResult = (s, e) =>
         {
            var grd = s as MVCxGridView;
            grd.FilterEnabled = showFilter;
            if (!grd.FilterEnabled)
            {
               grd.FilterExpression = String.Empty;
            }
         };

         //toolbar
         st.Toolbars.Add(toolbar =>
         {
            toolbar.Name = "toolbarMain";
            toolbar.Enabled = true;
            toolbar.Position = GridToolbarPosition.Top;
            toolbar.ItemAlign = GridToolbarItemAlign.Left;
            toolbar.SettingsAdaptivity.Enabled = true;
            toolbar.SettingsAdaptivity.EnableCollapseToSideMenu = true;
            toolbar.SettingsAdaptivity.CollapseToSideMenuAtWindowInnerWidth = WebApp.Constant.WindowWidth.ExtraSmall;

            if (editable)
            {
               toolbar.Items.Add(tool =>
               {
                  tool.Name = "add";
                  tool.Text = Language.T("Thêm");
                  tool.ToolTip = Language.T("Thêm mới (F2)");
                  tool.Image.IconID = IconID.ActionsAdditem16x16;
                  tool.Command = GridViewToolbarCommand.Custom;
                  tool.Visible = CheckPermission(html.ViewContext.HttpContext, module, function.Name, "Thêm");
               });
               toolbar.Items.Add(tool =>
               {
                  tool.Name = "edit";
                  tool.Text = Language.T("Sửa");
                  tool.ToolTip = Language.T("Sửa dữ liệu (F3)");
                  tool.Image.IconID = IconID.EditEdit16x16;
                  tool.Command = GridViewToolbarCommand.Custom;
                  tool.Visible = CheckPermission(html.ViewContext.HttpContext, module, function.Name, "Sửa");
               });
               toolbar.Items.Add(tool =>
               {
                  tool.Name = "del";
                  tool.Text = Language.T("Xóa");
                  tool.ToolTip = Language.T("Xóa dữ liệu (F4)");
                  tool.Image.IconID = IconID.EditDelete16x16;
                  tool.Command = GridViewToolbarCommand.Custom;
                  tool.Visible = CheckPermission(html.ViewContext.HttpContext, module, function.Name, "Xóa");
               });
            }

            if (rootTools != null) toolbar.Items.AddRange(rootTools);

            if (WebApp.Constant.WindowWidth.CurrentType > WebApp.Constant.WindowWidth.WidthType.Small)
            {
               toolbar.Items.Add(tool =>
               {
                  tool.BeginGroup = true;
                  tool.Command = GridViewToolbarCommand.Custom;
                  tool.SetTemplateContent(c =>
                  {
                     html.DevExpress().TextBox(txt =>
                     {
                        txt.Name = "searchSheet";
                        txt.Properties.NullText = Language.T("Tìm kiếm");
                        txt.Properties.NullTextDisplayMode = NullTextDisplayMode.UnfocusedAndFocused;
                        txt.Properties.SelectInputTextOnClick = true;
                     }).Render();
                  });
               });
               st.SettingsSearchPanel.CustomEditorName = "searchSheet";
            }

            var advItems = toolbar.Items;
            if (WebApp.Constant.WindowWidth.CurrentType > WebApp.Constant.WindowWidth.WidthType.ExtraSmall)
            {
               toolbar.Items.Add(tool =>
               {
                  tool.Image.IconID = IconID.SetupProperties16x16;
                  advItems = tool.Items;
               });
            }

            advItems.Add(it =>
            {
               it.Name = "filter";
               it.Text = Language.T("Lọc dữ liệu");
               it.Image.IconID = IconID.FilterFilter16x16;
               it.Checked = showFilter;
            });

            if (advancedTools != null) advItems.AddRange(advancedTools);

            advItems.Add(it =>
            {
               it.Command = GridViewToolbarCommand.ExportToXlsx;
               it.Text = Language.T("Xuất sang Excel");
               it.Image.IconID = IconID.ExportExport16x16;
            });
            advItems.Add(it =>
            {
               it.Text = Language.T("Làm mới (F5)");
               it.Image.IconID = IconID.ActionsRefresh16x16;
               it.Command = GridViewToolbarCommand.Refresh;
            });


            toolbar.Items.Add(tool =>
            {
               tool.Name = "close";
               tool.Text = Language.T("Đóng");
               tool.Image.IconID = IconID.ActionsClose16x16;
               tool.ToolTip = Language.T("Đóng chức năng này (ESC)");
               tool.Command = GridViewToolbarCommand.Custom;
            });
         });

         st.ClientSideEvents.ToolbarItemClick = "function(s,e) {e.item.name && WebApp.function[e.item.name] && WebApp.function[e.item.name]();}";
         st.ClientSideEvents.BeginCallback = "function(s,e) {e.customArgs.showFilter = s._showFilter;}";
         if (editable)
         {
            st.ClientSideEvents.Init = "function(s,e) {WebApp.function.update && WebApp.function.update();}";
            st.ClientSideEvents.EndCallback = "function(s,e) {WebApp.function.update && WebApp.function.update();}";
            st.ClientSideEvents.FocusedRowChanged = "function(s,e) {WebApp.function.update && WebApp.function.update();}";
            st.ClientSideEvents.RowDblClick = "function(s,e) {WebApp.function.edit && WebApp.function.edit();}";
         }

      }
   }
}