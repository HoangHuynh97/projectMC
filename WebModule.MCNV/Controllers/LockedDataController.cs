using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Khóa dữ liệu")]
   public class LockedDataController : BaseController
   {
      public ActionResult Index()
      {
         return PartialView(Models.LockedData.Load());
      }

      [HttpPost]
      public ActionResult Index(Models.LockedData vm)
      {
         if (!ModelState.IsValid) return ShowInfo(T("Lỗi"), T("Dữ liệu không hợp lệ"));
         vm.Save();
         return Nothing();
      }
   }
}