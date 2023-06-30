using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class ChangePassword
   {
      [Core.Required]
      [Core.Caption("Mật khẩu cũ"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string OldPassword { get; set; }
      [Core.Required]
      [Core.Caption("Mật khẩu mới"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string NewPassword { get; set; }
      [Core.Required, Core.Equal("NewPassword", "Không khớp với mật khẩu mới")]
      [Core.Caption("Nhập lại mật khẩu mới"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string Verify { get; set; }
   }
}