using System;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Core
{
   public class BaseWebViewPage<TModel> : WebViewPage<TModel>
   {
      public const int WidthDate = 100;
      public const int WidthDateTime = 150;
      public const int WidthNumber = 120;
      public const int WidthAdaptivity = Constant.WindowWidth.ExtraSmall;

      public string T(string key, params object[] values)
      {
         return Language.T(key, values);
      }

      public HtmlString H(string key, params object[] values)
      {
         return new HtmlString(T(key, values));
      }

      public bool CheckPermission(string function, string logic)
      {
         var type = this.ViewContext.Controller.GetType();
         var module = type.Assembly.ManifestModule.Name;
         return this.Context.CheckPermission(module, function, logic);
      }

      public bool CheckPermission(string logic)
      {
         var type = this.ViewContext.Controller.GetType();
         var f = type.GetCustomAttributes(typeof(Core.FunctionAttribute), false)[0] as Core.FunctionAttribute;
         return CheckPermission(f.Name, logic);
      }

      public int WindowWidth => Constant.WindowWidth.Current;
      public Constant.WindowWidth.WidthType WindowWidthType => Constant.WindowWidth.CurrentType;
         

      public override void Execute()
      {
      }
   }
}