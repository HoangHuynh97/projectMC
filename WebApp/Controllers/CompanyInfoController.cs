using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebApp.Controllers
{
   [Function("Thông tin doanh nghiệp")]
   public class CompanyInfoController : BaseController
   {
      public ActionResult Index()
      {
         return PartialView(Models.CompanyInfo.Load());
      }

      [HttpPost]
      public ActionResult UploadLogo()
      {
         return BinaryImageEditExtension.GetCallbackResult();
      }

      [HttpPost]
      public ActionResult Update(Models.CompanyInfo vm)
      {
         if (!ModelState.IsValid)
            return PartialView("Index", vm);
         vm.Save();
         return Nothing();
      }
   }
}