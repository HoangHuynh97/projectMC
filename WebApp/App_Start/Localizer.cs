using DevExpress.Utils.Localization.Internal;
using DevExpress.Web.Localization;
using DevExpress.XtraReports.Web.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApp.Localizer
{
   public class Localizer : ASPxperienceResLocalizer
   {
      public static void Activate()
      {
         Localizer localizer = new Localizer();
         DefaultActiveLocalizerProvider<ASPxperienceStringId> provider = new DefaultActiveLocalizerProvider<ASPxperienceStringId>(localizer);
         Localizer.SetActiveLocalizerProvider(provider);
      }

      public override string GetLocalizedString(ASPxperienceStringId id)
      {
         var result = base.GetLocalizedString(id);
         if (Language == "vi")
            result = Core.Language.T(result);
         return result;
      }
   }

   public class Editors : ASPxEditorsResLocalizer
   {
      public static void Activate()
      {
         Editors localizer = new Editors();
         DefaultActiveLocalizerProvider<ASPxEditorsStringId> provider = new DefaultActiveLocalizerProvider<ASPxEditorsStringId>(localizer);
         Editors.SetActiveLocalizerProvider(provider);
      }

      public override string GetLocalizedString(ASPxEditorsStringId id)
      {
         var result = base.GetLocalizedString(id);
         if (Language == "vi")
            result = Core.Language.T(result);
         return result;
      }
   }

   public class GridView : ASPxGridViewResLocalizer
   {
      public static void Activate()
      {
         GridView localizer = new GridView();
         DefaultActiveLocalizerProvider<ASPxGridViewStringId> provider = new DefaultActiveLocalizerProvider<ASPxGridViewStringId>(localizer);
         GridView.SetActiveLocalizerProvider(provider);
      }

      public override string GetLocalizedString(ASPxGridViewStringId id)
      {
         var result = base.GetLocalizedString(id);
         if (Language == "vi")
            result = Core.Language.T(result);
         return result;
      }
   }

   public class Report : ASPxReportsResLocalizer
   {
      public static void Activate()
      {
         Report localizer = new Report();
         DefaultActiveLocalizerProvider<ASPxReportsStringId> provider = new DefaultActiveLocalizerProvider<ASPxReportsStringId>(localizer);
         Report.SetActiveLocalizerProvider(provider);
      }

      public override string GetLocalizedString(ASPxReportsStringId id)
      {
         var result = base.GetLocalizedString(id);
         if (Language == "vi")
            result = Core.Language.T(result);
         return result;
      }
   }
}