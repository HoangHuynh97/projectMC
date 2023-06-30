using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
   [Core.Function("Từ điển ngôn ngữ", Roles = Constant.Role.System)]
   public class LanguageController : Core.BaseController
   {
      public ActionResult Index()
      {
         return PartialView(Models.LanguageModel.Load());
      }

      public ActionResult IndexPartial()
      {
         return PartialView(Models.LanguageModel.Load());
      }

      public ActionResult Update(MVCxGridViewBatchUpdateValues<Models.LanguageModel, string> vm)
      {
         Models.LanguageModel.Update(vm.Update);
         return PartialView("IndexPartial", Models.LanguageModel.Load());
      }
   }
}