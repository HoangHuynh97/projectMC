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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Patient.cshtml")]
    public partial class _Views_Shared_EditorTemplates_Patient_cshtml_ : WebApp.Core.BaseWebViewPage<int?>
    {
        public _Views_Shared_EditorTemplates_Patient_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Shared\EditorTemplates\Patient.cshtml"
  
   var name = ViewData.ModelMetadata.PropertyName;
   var caption = T(ViewData.ModelMetadata.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("\r\n<script>\r\n   function onNewPatient() {\r\n      WebApp.showDialog({\r\n         tit" +
"le: \'");

            
            #line 9 "..\..\Views\Shared\EditorTemplates\Patient.cshtml"
            Write(H("Thêm bệnh nhân"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n         url: \'");

            
            #line 10 "..\..\Views\Shared\EditorTemplates\Patient.cshtml"
          Write(Url.Action("Add", "Patient"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n         width: 1000,\r\n      }).done(function (r) {\r\n         const ctrl = ");

            
            #line 13 "..\..\Views\Shared\EditorTemplates\Patient.cshtml"
                 Write(name);

            
            #line default
            #line hidden
WriteLiteral(@";
         ctrl._newValue = r;
         ctrl.SetText(r.Name);
         ctrl.filterStrategy.Filtering();
      });
   }
   function onEndCallbackPatient(s) {
      if (s._newValue) {
         var r = s._newValue;
         delete s._newValue;
         setTimeout(function () { s.SetValue(r.Id); });
      }
   }
</script>
");

            
            #line 27 "..\..\Views\Shared\EditorTemplates\Patient.cshtml"
Write(Html.DevExpress().ComboBoxFor(m => m, st =>
{
   st.DefaultSettings();
   st.CallbackRouteValues = new { Action = "Patient", Controller = "Combobox" };
   st.Properties.ValueField = "Oid";
   st.Properties.ValueType = typeof(int);
   st.Properties.Caption = caption;
   st.Properties.CallbackPageSize = 20;
   if (ViewData.ModelMetadata.IsRequired)
      st.Properties.CaptionSettings.RequiredMarkDisplayMode = EditorRequiredMarkMode.Required;
   st.Properties.TextFormatString = "{1}";
   st.Properties.NullText = T("Tìm theo mã, tên, CMND, CCCD, BHYT, điện thoại");
   st.Properties.Columns.Add("Code", T("Mã")).Width = 50;
   st.Properties.Columns.Add("Name", T("Tên"));
   if (CheckPermission("Thông tin NKT", "Thêm"))
   {
      var btn = st.Properties.Buttons.Add();
      btn.Image.IconID = IconID.ActionsAdd16x16;
      btn.ToolTip = T("Thêm");
      st.Properties.ClientSideEvents.ButtonClick = "onNewPatient";
      st.Properties.ClientSideEvents.EndCallback = "onEndCallbackPatient";
   }
}).BindList(Patient.ComboboxData()).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591