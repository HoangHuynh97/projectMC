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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Treatments.cshtml")]
    public partial class _Views_Shared_EditorTemplates_Treatments_cshtml_ : WebApp.Core.BaseWebViewPage<string>
    {
        public _Views_Shared_EditorTemplates_Treatments_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
  
   var name = ViewData.ModelMetadata.PropertyName;
   var caption = T(ViewData.ModelMetadata.DisplayName);
   var ids = ViewData.ModelMetadata.ContainerType.GetProperty(name + "Value").GetValue(ViewData.ModelMetadata.Container) as string;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<script>\r\n   function onTreatmentsChanged(s) {\r\n      const items = s.GetSele" +
"ctedItems();\r\n      const name = \'#");

            
            #line 12 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
                 Write(name);

            
            #line default
            #line hidden
WriteLiteral("Value\';\r\n      $(name).val(items.map(it => it.value).join(\';\'));\r\n      const ctr" +
"l = ");

            
            #line 14 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
              Write(name);

            
            #line default
            #line hidden
WriteLiteral(";\r\n      ctrl.SetValue(items.map(it => it.text).join(\'; \'));\r\n      ctrl.RaiseVal" +
"ueChanged();\r\n   }\r\n   function onTreatmentsDropdown() {\r\n      const ctrl = ");

            
            #line 19 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
              Write(name);

            
            #line default
            #line hidden
WriteLiteral(";\r\n      TreatmentsList.SetHeight(TreatmentsList.GetHeightByContent() + 10);\r\n   " +
"}\r\n   function onNewTreatment() {\r\n      WebApp.showDialog({\r\n         title: \'");

            
            #line 24 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
            Write(H("Thêm gói điều trị"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n         url: \'");

            
            #line 25 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
          Write(Url.Action("Add", "Treatment"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n         width: 600,\r\n      }).done(function (r) {\r\n         const sel = Trea" +
"tmentsList.AddItem([r.Name], r.Id);\r\n         TreatmentsList.SetSelectedIndex(se" +
"l);\r\n         TreatmentsList.RaiseSelectedIndexChanged();\r\n      });\r\n   }\r\n</sc" +
"ript>\r\n<input");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("id", Tuple.Create(" id=\"", 1141), Tuple.Create("\"", 1158)
            
            #line 34 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
, Tuple.Create(Tuple.Create("", 1146), Tuple.Create<System.Object, System.Int32>(name
            
            #line default
            #line hidden
, 1146), false)
, Tuple.Create(Tuple.Create("", 1153), Tuple.Create("Value", 1153), true)
);

WriteAttribute("name", Tuple.Create(" name=\"", 1159), Tuple.Create("\"", 1178)
            
            #line 34 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
, Tuple.Create(Tuple.Create("", 1166), Tuple.Create<System.Object, System.Int32>(name
            
            #line default
            #line hidden
, 1166), false)
, Tuple.Create(Tuple.Create("", 1173), Tuple.Create("Value", 1173), true)
);

WriteAttribute("value", Tuple.Create(" value=\"", 1179), Tuple.Create("\"", 1191)
            
            #line 34 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
, Tuple.Create(Tuple.Create("", 1187), Tuple.Create<System.Object, System.Int32>(ids
            
            #line default
            #line hidden
, 1187), false)
);

WriteLiteral(" />\r\n");

            
            #line 35 "..\..\Views\Shared\EditorTemplates\Treatments.cshtml"
Write(Html.DevExpress().DropDownEditFor(m => m, dp =>
{
   dp.Width = Unit.Percentage(100);
   dp.Properties.Caption = T(caption);
   dp.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
   if (ViewData.ModelMetadata.IsRequired)
   {
      dp.Properties.CaptionSettings.RequiredMarkDisplayMode = EditorRequiredMarkMode.Required;
   }
   dp.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
   dp.Properties.ValidationSettings.Display = Display.Dynamic;
   dp.ShowModelErrors = true;
   dp.ReadOnly = true;
   dp.Properties.EnableClientSideAPI = true;
   dp.Properties.SettingsAdaptivity.Mode = DropDownEditAdaptivityMode.OnWindowInnerWidth;
   dp.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.Never;
   dp.SetDropDownWindowTemplateContent(container =>
   {
      Html.DevExpress().ListBox(st =>
      {
         st.Name = "TreatmentsList";
         st.Width = Unit.Percentage(100);
         st.Properties.SelectionMode = ListEditSelectionMode.CheckColumn;
         st.Properties.ValueField = "Oid";
         st.Properties.ValueType = typeof(int);
         st.Properties.TextField = "Name";
         st.ControlStyle.Border.BorderWidth = 0;
         st.Properties.EnableClientSideAPI = true;
         st.Properties.ClientSideEvents.SelectedIndexChanged = "onTreatmentsChanged";
         st.PreRender += (sender, _) =>
         {
            var me = sender as MVCxListBox;
            if (!string.IsNullOrEmpty(ids))
            {
               var vals = ids.Split(';').Select(el => int.Parse(el)).ToList();
               foreach (ListEditItem it in me.Items)
               {
                  it.Selected = vals.Contains((int)it.Value);
               }
            }
         };
      }).BindList(Treatment.ComboboxData()).Render();
   });
   dp.Properties.ClientSideEvents.DropDown = "onTreatmentsDropdown";
   if (CheckPermission("Gói can thiệp", "Thêm"))
   {
      var btn = dp.Properties.Buttons.Add();
      btn.Image.IconID = IconID.ActionsAdd16x16;
      btn.ToolTip = T("Thêm");
      dp.Properties.ClientSideEvents.ButtonClick = "onNewTreatment";
   }
}).GetHtml());

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
