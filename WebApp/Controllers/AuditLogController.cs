using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
   [Core.Function("Nhật ký dữ liệu")]
   public class AuditLogController : Controller
   {
      public ActionResult Index()
      {
         return PartialView(new Models.AuditLog());
      }

      public ActionResult IndexPartial(Models.AuditLog vm)
      {
         return PartialView(vm);
      }
   }
}