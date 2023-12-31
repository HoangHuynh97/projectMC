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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Specification/IndexPartial.cshtml")]
    public partial class _Views_Specification_IndexPartial_cshtml : WebApp.Core.BaseWebViewPage<Specification.ListInfo>
    {
        public _Views_Specification_IndexPartial_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Specification\IndexPartial.cshtml"
Write(Html.DevExpress().GridView(st =>
{
   Html.DefaultGridSheet(st, Model.ShowFilter);
   st.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
   st.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.Off;
   st.Settings.ShowColumnHeaders = true;
   st.Columns.Add(col =>
   {
      col.FieldName = "Service";
      col.Caption = "Hoạt động PHCN";
      col.GroupIndex = 0;
      col.Settings.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
   });
   st.Columns.Add(col =>
   {
      col.FieldName = "Name";
      col.Caption = T("Tên kỹ thuật");
   });
   st.CustomColumnSort = (s, e) =>
   {
      if (e.Column != null && e.Column.FieldName == "Service")
      {
         var ind1 = Convert.ToInt32(e.GetRow1Value("ServiceIndex"));
         var ind2 = Convert.ToInt32(e.GetRow2Value("ServiceIndex"));
         var res = ind1 - ind2;
         if (res == 0)
         {
            res = e.Value1.ToString().CompareTo(e.Value2.ToString());
         }
         e.Result = res;
         e.Handled = true;
      }
   };
}).Bind(Model.GetData()).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
