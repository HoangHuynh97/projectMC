using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Function("Danh sách học viên")]
   public class TrainingController : BaseController
   {

      public ActionResult Index()
      {
         return PartialView(new Models.Training.ListInfo());
      }

      public ActionResult IndexPartial(Models.Training.ListInfo vm)
      {
         return PartialView(vm);
      }
   }
}