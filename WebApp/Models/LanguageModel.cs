using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class LanguageModel
   {
      public string Keyword { get; set; }
      public string Value { get; set; }

      public static List<LanguageModel> Load()
      {
         return Core.Language.Current.Dictionary.Select(p => new LanguageModel() { Keyword = p.Key, Value = p.Value }).ToList();
      }

      public static void Update(List<LanguageModel> changes)
      {
         var dict = Core.Language.Current.Dictionary;
         foreach(var c in changes)
            dict[c.Keyword] = c.Value;
         Core.Language.Current.Save();
      }
   }
}