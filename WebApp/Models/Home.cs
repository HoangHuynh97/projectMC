using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
   public class Home
   {
      public List<Core.Menu.TabItem> MainMenu { get; private set; }
      public List<Core.Language> Languages { get; private set; }
      public string LanguageCurrent { get; private set; }
      public Dictionary<string, List<string>> Skins { get; private set; }
      public string SkinCurrent { get; private set; }
      public bool SelectCompany { get; private set; }

      public bool HasHelp { get; private set; }

      public static Home Create()
      {
         var result = new Home()
         {
            MainMenu = Core.Menu.GetUserMenu(),
            Languages = Core.Language.List,
            LanguageCurrent = Core.Language.Current.Code,
            Skins = Core.Skin.List,
            SkinCurrent = Core.Skin.Current,
            SelectCompany = Core.ApplicationDbContext.Current.UserModel.GetAccessCompanies().Count > 1,
            HasHelp = System.IO.File.Exists(HostingEnvironment.MapPath("~/Content/help.pdf"))
         };

         return result;
      }
   }
}