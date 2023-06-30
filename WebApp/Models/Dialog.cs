using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class Dialog
   {
      public class Button
      {
         public string Value { get; set; }
         public string Text { get; set; }
         public string Icon { get; set; }
      }

      public string Name { get; set; }
      public string Title { get; set; }
      public string Icon { get; set; }
      public bool Maximized { get; set; }
      public bool Modal { get; set; }
      public string Url { get; set; }
      public List<Button> Buttons { get; set; }

      public Dialog()
      {
         Buttons = new List<Button>();
      }
   }
}