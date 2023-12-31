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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/UpdateProfile.cshtml")]
    public partial class _Views_Home_UpdateProfile_cshtml : WebApp.Core.BaseWebViewPage<UserProfile>
    {
        public _Views_Home_UpdateProfile_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral(@"<style>
   .btnlink {
      background: transparent;
      border: none;
      padding: 5px;
      margin-right: 5px;
      cursor: pointer;
      width: 70px;
   }

      .btnlink.disable {
         opacity: 0.3;
      }

      .btnlink:hover {
         background: #f0f0f0;
         opacity: 1;
      }
</style>
<div");

WriteLiteral(" class=\"page-container\"");

WriteLiteral(">\r\n   <div");

WriteLiteral(" class=\"page-header grid-middle\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"mr\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 24 "..\..\Views\Home\UpdateProfile.cshtml"
    Write(Html.Icon(IconID.ViewCard32x32devav));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n      <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">\r\n");

WriteLiteral("         ");

            
            #line 27 "..\..\Views\Home\UpdateProfile.cshtml"
    Write(T("Thông tin cá nhân"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   </div>\r\n   <div");

WriteLiteral(" class=\"page-content\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"grid\"");

WriteLiteral(">\r\n\r\n         <div");

WriteLiteral(" class=\"col-6_xs-12_sm-12_md-8\"");

WriteLiteral(">\r\n            <form");

WriteLiteral(" class=\"mb\"");

WriteLiteral(" id=\"frmUpdateProfile\"");

WriteAttribute("action", Tuple.Create(" action=\"", 753), Tuple.Create("\"", 790)
            
            #line 34 "..\..\Views\Home\UpdateProfile.cshtml"
, Tuple.Create(Tuple.Create("", 762), Tuple.Create<System.Object, System.Int32>(Url.Action("UpdateProfile")
            
            #line default
            #line hidden
, 762), false)
);

WriteLiteral(">\r\n               <div");

WriteLiteral(" class=\"box grid\"");

WriteLiteral(">\r\n                  <div");

WriteLiteral(" class=\"col-8_xs-12\"");

WriteLiteral(">\r\n                     <div");

WriteLiteral(" class=\"mb\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 38 "..\..\Views\Home\UpdateProfile.cshtml"
                   Write(Html.EditorFor(m => m.FullName));

            
            #line default
            #line hidden
WriteLiteral("\r\n                     </div>\r\n                     <div");

WriteLiteral(" class=\"mb\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 41 "..\..\Views\Home\UpdateProfile.cshtml"
                   Write(Html.EditorFor(m => m.Address));

            
            #line default
            #line hidden
WriteLiteral("\r\n                     </div>\r\n                     <div");

WriteLiteral(" class=\"mb\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 44 "..\..\Views\Home\UpdateProfile.cshtml"
                   Write(Html.EditorFor(m => m.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n                     </div>\r\n                     <div>\r\n");

WriteLiteral("                        ");

            
            #line 47 "..\..\Views\Home\UpdateProfile.cshtml"
                   Write(Html.EditorFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n                     </div>\r\n                  </div>\r\n                  <div");

WriteLiteral(" class=\"col-4_xs-12\"");

WriteLiteral(" style=\"min-height: 200px;\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 51 "..\..\Views\Home\UpdateProfile.cshtml"
                Write(Html.DevExpress().BinaryImageFor(m => m.Image, st =>
                {
                   st.Width = Unit.Percentage(100);
                   st.Height = Unit.Percentage(100);
                   st.CallbackRouteValues = new { Controller = "Home", Action = "UpdateUserImage" };
                   st.Properties.ImageWidth = 300;
                   st.Properties.ImageHeight = 300;
                   st.Properties.ImageSizeMode = ImageSizeMode.FitProportional;
                   st.Properties.EditingSettings.Enabled = true;
                   st.Properties.EditingSettings.AllowDropOnPreview = true;
                   st.Properties.EditingSettings.UploadSettings.UploadValidationSettings.MaxFileSize = 1000000;
                }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n                  <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                     ");

            
            #line 65 "..\..\Views\Home\UpdateProfile.cshtml"
                Write(Html.DevExpress().Button(b =>
                   {
                      b.Name = "btnSaveProfile";
                      b.Text = T("Lưu");
                      b.Images.Image.IconID = IconID.SaveSave16x16;
                      b.ClientSideEvents.Click = "WebApp.function.save";
                   }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  </div>\r\n               </div>\r\n            </form>\r\n");

            
            #line 75 "..\..\Views\Home\UpdateProfile.cshtml"
            
            
            #line default
            #line hidden
            
            #line 75 "..\..\Views\Home\UpdateProfile.cshtml"
             if (Model.Logins.Count > 0)
            {

            
            #line default
            #line hidden
WriteLiteral("               <form");

WriteLiteral(" id=\"frmLinkLogin\"");

WriteLiteral(" method=\"post\"");

WriteAttribute("action", Tuple.Create(" action=\"", 2826), Tuple.Create("\"", 2859)
            
            #line 77 "..\..\Views\Home\UpdateProfile.cshtml"
, Tuple.Create(Tuple.Create("", 2835), Tuple.Create<System.Object, System.Int32>(Url.Action("LinkLogin")
            
            #line default
            #line hidden
, 2835), false)
);

WriteLiteral(">\r\n");

WriteLiteral("                  ");

            
            #line 78 "..\..\Views\Home\UpdateProfile.cshtml"
             Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteral("\r\n                  <div");

WriteLiteral(" class=\"box grid\"");

WriteLiteral(">\r\n                     <div");

WriteLiteral(" class=\"col-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 81 "..\..\Views\Home\UpdateProfile.cshtml"
                   Write(Html.DevExpress().Label(st =>
                   {
                      st.Text = T("Click để thêm hoặc hủy đăng nhập từ các tài khoản");
                   }).GetHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                     </div>\r\n                     <div>\r\n");

            
            #line 87 "..\..\Views\Home\UpdateProfile.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 87 "..\..\Views\Home\UpdateProfile.cshtml"
                         if (Model.Logins.ContainsKey("Facebook"))
                        {

            
            #line default
            #line hidden
WriteLiteral("                           <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" name=\"provider\"");

WriteLiteral(" value=\"Facebook\"");

WriteAttribute("class", Tuple.Create(" class=\"", 3425), Tuple.Create("\"", 3491)
, Tuple.Create(Tuple.Create("", 3433), Tuple.Create("btnlink", 3433), true)
            
            #line 89 "..\..\Views\Home\UpdateProfile.cshtml"
                 , Tuple.Create(Tuple.Create(" ", 3440), Tuple.Create<System.Object, System.Int32>(Model.Logins["Facebook"] ? "enable" : "disable"
            
            #line default
            #line hidden
, 3441), false)
);

WriteLiteral(">\r\n                              <div");

WriteLiteral(" class=\"img facebook\"");

WriteLiteral("></div>\r\n                              <div>Facebook</div>\r\n                     " +
"      </button>\r\n");

            
            #line 93 "..\..\Views\Home\UpdateProfile.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        ");

            
            #line 94 "..\..\Views\Home\UpdateProfile.cshtml"
                         if (Model.Logins.ContainsKey("Google"))
                        {

            
            #line default
            #line hidden
WriteLiteral("                           <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" name=\"provider\"");

WriteLiteral(" value=\"Google\"");

WriteAttribute("class", Tuple.Create(" class=\"", 3847), Tuple.Create("\"", 3911)
, Tuple.Create(Tuple.Create("", 3855), Tuple.Create("btnlink", 3855), true)
            
            #line 96 "..\..\Views\Home\UpdateProfile.cshtml"
               , Tuple.Create(Tuple.Create(" ", 3862), Tuple.Create<System.Object, System.Int32>(Model.Logins["Google"] ? "enable" : "disable"
            
            #line default
            #line hidden
, 3863), false)
);

WriteLiteral(">\r\n                              <div");

WriteLiteral(" class=\"img google\"");

WriteLiteral("></div>\r\n                              <div>Google</div>\r\n                       " +
"    </button>\r\n");

            
            #line 100 "..\..\Views\Home\UpdateProfile.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        ");

            
            #line 101 "..\..\Views\Home\UpdateProfile.cshtml"
                         if (Model.Logins.ContainsKey("Microsoft"))
                        {

            
            #line default
            #line hidden
WriteLiteral("                           <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" name=\"provider\"");

WriteLiteral(" value=\"Microsoft\"");

WriteAttribute("class", Tuple.Create(" class=\"", 4269), Tuple.Create("\"", 4336)
, Tuple.Create(Tuple.Create("", 4277), Tuple.Create("btnlink", 4277), true)
            
            #line 103 "..\..\Views\Home\UpdateProfile.cshtml"
                  , Tuple.Create(Tuple.Create(" ", 4284), Tuple.Create<System.Object, System.Int32>(Model.Logins["Microsoft"] ? "enable" : "disable"
            
            #line default
            #line hidden
, 4285), false)
);

WriteLiteral(">\r\n                              <div");

WriteLiteral(" class=\"img microsoft\"");

WriteLiteral("></div>\r\n                              <div>Microsoft</div>\r\n                    " +
"       </button>\r\n");

            
            #line 107 "..\..\Views\Home\UpdateProfile.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                     </div>\r\n                  </div>\r\n               </form>\r\n");

            
            #line 111 "..\..\Views\Home\UpdateProfile.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("         </div>\r\n         <div");

WriteLiteral(" class=\"col-6_xs-0_sm-0_md-4\"");

WriteLiteral("></div>\r\n      </div>\r\n   </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
   $(function () {
      WebApp.validateForm('#frmUpdateProfile');

      $('#frmLinkLogin .btnlink').click(function () {
         var btn = $(this);
         var v = btn.val();
         if (btn.hasClass('enable')) {
            WebApp.showConfirm('");

            
            #line 126 "..\..\Views\Home\UpdateProfile.cshtml"
                           Write(H("Hủy đăng nhập"));

            
            #line default
            #line hidden
WriteLiteral("\', \'");

            
            #line 126 "..\..\Views\Home\UpdateProfile.cshtml"
                                                  Write(H("Bạn có muốn hủy bỏ đăng nhập từ {0} không?"));

            
            #line default
            #line hidden
WriteLiteral("\'.format(v)).done(function () {\r\n               WebApp.ajax(\'");

            
            #line 127 "..\..\Views\Home\UpdateProfile.cshtml"
                       Write(Url.Action("RemoveLogin"));

            
            #line default
            #line hidden
WriteLiteral("\', { provider: v }).done(function () {\r\n                  btn.toggleClass(\'enable" +
" disable\');\r\n                  WebApp.notify(\'");

            
            #line 129 "..\..\Views\Home\UpdateProfile.cshtml"
                            Write(H("Đã hủy bỏ đăng nhập {0}"));

            
            #line default
            #line hidden
WriteLiteral("\'.format(v));\r\n               });\r\n            });\r\n            return false;\r\n  " +
"       }\r\n      });\r\n   });\r\n\r\n   WebApp.function = {\r\n      title: \'");

            
            #line 138 "..\..\Views\Home\UpdateProfile.cshtml"
         Write(H("Thông tin cá nhân"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n      save: function (s, e) {\r\n         WebApp.validForm(\'#frmUpdateProfile\')" +
" && WebApp.submit(\'#frmUpdateProfile\').done(function () {\r\n            WebApp.no" +
"tify(\'");

            
            #line 141 "..\..\Views\Home\UpdateProfile.cshtml"
                      Write(H("Đã cập nhật thông tin cá nhân"));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n         });\r\n      }\r\n   };\r\n</script>");

        }
    }
}
#pragma warning restore 1591
