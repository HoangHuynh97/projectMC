using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Duyệt hồ sơ chuyển vùng")]
   public class SharedDataController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.SharedData.ListInfo());
      }

      public ActionResult IndexPartial(Models.SharedData.ListInfo vm)
      {
         return PartialView(vm);
      }

      [Logic("Xóa"), HttpPost]
      public ActionResult Delete(int id)
      {
         Models.SharedData.Delete(id);
         return Nothing();
      }

      [HttpPost]
      public ActionResult UpdateAccept(int id, bool accept)
      {
         Models.SharedData.UpdateAccept(id, accept);
         return Nothing();
      }
   }
}