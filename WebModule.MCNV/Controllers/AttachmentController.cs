using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;
using System.IO;

namespace WebModule.MCNV.Controllers
{
   [Authorize]
   public class AttachmentController : BaseController
   {
      public ActionResult Download(string id, string saveas)
      {
         try
         {
            var vm = Models.Attachment.GetByUniqueId(id);
            vm.SaveAs = saveas;
            var f = Server.MapPath(vm.Url);
            if (vm.ContentType=="application/pdf")
               return File(f, vm.ContentType); //can not custom file download name for PDF viewer
            return File(f, vm.ContentType, vm.FileName + Path.GetExtension(f));
         }
         catch
         {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, T("Dữ liệu không hợp lệ"));
         }
      }

      public ActionResult Viewer(string id, string saveas)
      {
         var notFound = Content(T("Không tìm thấy tài liệu"));
         if (string.IsNullOrEmpty(id)) return notFound;
         var vm = Models.Attachment.GetByUniqueId(id);
         if (vm == null) return notFound;
         vm.SaveAs = saveas;
         var f = Server.MapPath(vm.Url);
         if (!System.IO.File.Exists(f)) return notFound;
         if (vm.Type == Models.AttachmentType.Other)
            return File(f, vm.ContentType, vm.FileName + Path.GetExtension(f));
         return View(vm);
      }
      public ActionResult Upload(string uploaderName)
      {
         DevExpress.Web.Mvc.UploadControlExtension.GetUploadedFiles(uploaderName, Models.Attachment.UploadSettings, (s, e) =>
         {
            //save tempory file
            string folder = Server.MapPath("/Content/Tempory");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            string newName = Guid.NewGuid().ToString() + Path.GetExtension(e.UploadedFile.FileName);
            e.UploadedFile.SaveAs(Path.Combine(folder, newName));
            //return value
            var name = Path.GetFileNameWithoutExtension(e.UploadedFile.FileName);
            if (name.Length > 100)
               name = name.Substring(0, 99);
            e.CallbackData = System.Web.Helpers.Json.Encode(new Models.Attachment()
            {
               Name = name,
               Url = newName
            });
         });
         return null;
      }
   }
}