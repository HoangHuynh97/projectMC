using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevExpress.Xpo;
using DevExpress.Web.Mvc.UI;
using WebModule.MCNV.Models;

namespace WebModule.MCNV
{
   public class MvcApplication : WebApp.MvcApplication
   {
      public override string Name => "WebModule.MCNV";

      public override void AfterStart()
      {
         base.AfterStart();

         WebApp.Models.CompanyUser.QueryUser = () =>
         {
            var db = WebApp.Core.ApplicationDbContext.Current;
            var query = from el in db.Session.Query<WebApp.DataModel.User>()
                        select el;
            if (!db.IsInRole(WebApp.Constant.Role.Admin))
            {
               var groups = from el in db.Session.QueryFacility() select el.Oid;
               query = from el in query
                       where groups.Contains(el.GroupId.Value)
                       select el;
            }
            return query;
         };

         WebApp.Models.CompanyUser.UserGroups = () =>
         {
            var db = WebApp.Core.ApplicationDbContext.Current;
            return db.Session.QueryFacility().ToDictionary(x => x.Oid, x => x.Name);
         };

         WebApp.Models.CompanyUser.UserGroupsView = (html) =>
         {
            var db = WebApp.Core.ApplicationDbContext.Current;
            var query = from el in db.Session.QueryFacility()
                        orderby el.Name
                        select new { el.Oid, el.Name, el.FullAddress };
            var lst = query.ToList();
            html.DevExpress().ComboBoxFor(m => m.GroupId, st =>
            {
               st.Width = System.Web.UI.WebControls.Unit.Percentage(100);
               st.Properties.Caption = WebApp.Core.Language.T("Đơn vị công tác");
               st.Properties.CaptionSettings.Position = DevExpress.Web.EditorCaptionPosition.Top;
               st.Properties.CaptionSettings.RequiredMarkDisplayMode = DevExpress.Web.EditorRequiredMarkMode.Required;
               st.Properties.ValueField = "Oid";
               st.Properties.ValueType = typeof(int);
               st.Properties.AllowNull = false;
               st.Properties.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
               st.Properties.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
               st.ShowModelErrors = true;
               st.Properties.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
               st.Properties.IncrementalFilteringDelay = 800;
               st.Properties.TextFormatString = "{0}";
               st.Properties.Columns.Add("Name", WebApp.Core.Language.T("Tên đơn vị")).Width = 300;
               st.Properties.Columns.Add("FullAddress", WebApp.Core.Language.T("Địa chỉ")).Width = 400;
               st.Properties.ClientSideEvents.Init = "function(s) {s.SetCustomFilter('Name', 'FullAddress');}";
            }).BindList(lst).Render();
            return new MvcHtmlString("");
         };

         WebApp.Models.CompanyUser.UserDatas = () =>
         {
            var db = WebApp.Core.ApplicationDbContext.Current;
            var query = db.Session.QueryPermissionData();
            var lst = query.ToDictionary(x => x.Oid, x => x.Name);
            return lst;
         };

         WebApp.Models.CompanyUser.UserDatasView = (html) =>
         {
            var lst = WebApp.Models.CompanyUser.UserDatas().Select(it => it).OrderBy(it => it.Value).ToList();
            html.DevExpress().ComboBoxFor(m => m.DataId, st =>
            {
               st.Width = System.Web.UI.WebControls.Unit.Percentage(100);
               st.Properties.Caption = WebApp.Core.Language.T("Quyền dữ liệu");
               st.Properties.CaptionSettings.Position = DevExpress.Web.EditorCaptionPosition.Top;
               st.Properties.CaptionSettings.RequiredMarkDisplayMode = DevExpress.Web.EditorRequiredMarkMode.Required;
               st.Properties.ValueField = "Key";
               st.Properties.ValueType = typeof(int);
               st.Properties.TextField = "Value";
               st.Properties.AllowNull = false;
               st.Properties.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
               st.Properties.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
               st.ShowModelErrors = true;
               st.Properties.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
               st.Properties.IncrementalFilteringDelay = 800;
               st.Properties.ClientSideEvents.Init = "function(s) {s.SetCustomFilter('Value');}";
            }).BindList(lst).Render();
            return new MvcHtmlString("");
         };
      }
   }
}
