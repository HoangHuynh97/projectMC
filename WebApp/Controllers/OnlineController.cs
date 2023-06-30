using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
   [Core.Function("Người dùng trực tuyến", Roles = Constant.Role.System)]
   public class OnlineController : Core.BaseController
   {
      public ActionResult Index()
      {
         return PartialView(Models.Online.Load());
      }

      public ActionResult IndexPartial()
      {
         return PartialView(Models.Online.Load());
      }

      public ActionResult Notify(string message)
      {
         SendNotify(message);
         return Nothing();
      }
   }
}