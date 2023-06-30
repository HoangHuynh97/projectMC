using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Constant
{
   public static class Role
   {
      public const string System = "System";
      public const string Admin = "Admin";
   }

   public static class Claim
   {
      public const string Company = "CurrentCompany";
      public const string Permission = "[Permission]";
      public const string OpenReport = "OpenReport";
   }

   public static class WindowWidth
   {
      //max width of devices
      public const int ExtraSmall = 576;
      public const int Small = 768;
      public const int Medium = 1024;
      public const int Large = 1280;

      public enum WidthType
      {
         ExtraSmall,
         Small,
         Medium,
         Large,
         ExtraLarge,
      }

      public static WidthType DetectWidth(int width)
      {
         if (width <= ExtraSmall) return WidthType.ExtraSmall;
         if (width <= Small) return WidthType.Small;
         if (width <= Medium) return WidthType.Medium;
         if (width <= Large) return WidthType.Large;
         return WidthType.ExtraLarge;
      }

      public static int Current => Convert.ToInt32(HttpContext.Current.Request.Headers["WindowWidth"]);
      public static WidthType CurrentType => Current == 0? WidthType.Large : DetectWidth(Current);
   }
}