using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public enum AttachmentType
   {
      Image,
      Video,
      Audio,
      PDF,
      Other
   }
   public class Attachment
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Url { get; set; }
      public string UniqueId { get; set; }
      /// <summary>
      /// Used to receive posted data to delete
      /// </summary>
      public bool Deleted { get; set; }
      public string UserCreate { get; }
      public string DateCreate { get; }
      /// <summary>
      /// Used to custom file name save as while download
      /// </summary>
      public string SaveAs { get; set; }
      public AttachmentType Type
      {
         get
         {
            var ext = System.IO.Path.GetExtension(Url);
            switch (ext)
            {
               case ".aac":
               case ".mp3":
               case ".oga":
               case ".wav":
               case ".weba":
               case ".mid":
               case ".midi":
               case ".opus":
                  return AttachmentType.Audio;
               case ".avi":
               case ".mp4":
               case ".mpeg":
               case ".ogv":
               case ".webm":
               case ".3gp":
                  return AttachmentType.Video;
               case ".jpg":
               case ".bmp":
               case ".gif":
               case ".jpeg":
               case ".png":
               case ".svg":
                  return AttachmentType.Image;
               case ".pdf":
                  return AttachmentType.PDF;
               default:
                  return AttachmentType.Other;
            }
         }
      }
      public string ContentType
      {
         get
         {
            if (ContentTypes.TryGetValue(System.IO.Path.GetExtension(Url), out var type))
               return type;
            return "application/octet-stream";
         }
      }

      public string FileName => string.IsNullOrEmpty(SaveAs) ? Name : SaveAs;

      public Attachment() { }
      public Attachment(DataModel.Attachment model)
      {
         Id = model.Oid;
         Name = model.Name;
         Url = model.FileUrl;
         UniqueId = model.UniqueId;
         UserCreate = ApplicationDbContext.Current.Session.GetObjectByKey<WebApp.DataModel.User>(model.UserCreate)?.UserName ?? "";
         DateCreate = model.DateCreate.Date.ToShortDateString();
      }

      public static string[] AllowedFileExtensions = new string[] {
         ".aac" , ".mp3", ".oga", ".wav", ".weba", ".mid", ".midi", ".opus",
         ".avi", ".mp4", ".mpeg", ".ogv", ".webm", ".3gp",
         ".jpg",".bmp",".gif",".jpeg",".png",".svg",
         ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
      public static int MaxFileSize = 60000000;
      public static Dictionary<string, string> ContentTypes = new Dictionary<string, string>()
      {
         {".aac", "audio/aac" },
         {".mp3","audio/mpeg" },
         {".oga","audio/ogg" },
         {".wav","audio/wav" },
         {".weba","audio/webm" },
         {".mid","audio/midi" },
         {".midi","audio/x-midi" },

         {".avi","video/x-msvideo" },
         {".mp4","video/mp4" },
         {".mpeg","video/mpeg" },
         {".ogv","video/ogg" },
         {".webm","video/webm" },
         {".3gp","video/3gpp" },

         {".jpg","image/jpeg" },
         {".bmp","image/bmp" },
         {".gif","image/gif" },
         {".jpeg","image/jpeg" },
         {".png","image/png" },
         {".svg","image/svg+xml" },
         {".pdf","application/pdf" },
         {".doc", "application/msword" },
         {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
         {".xls", ".application/vnd.ms-excel" },
         {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
         {".ppt", "application/vnd.ms-powerpoint" },
         {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" }
      };

      public static Attachment GetByUniqueId(string id)
      {
         var db = ApplicationDbContext.Current;
         var model = db.Session.FindObject<DataModel.Attachment>(CriteriaOperator.Parse("UniqueId==?", id));
         if (model == null) return null;
         return new Attachment(model);
      }

      public static DevExpress.Web.UploadControlValidationSettings UploadSettings = new DevExpress.Web.UploadControlValidationSettings()
      {
         AllowedFileExtensions = Attachment.AllowedFileExtensions,
         MaxFileSize = Attachment.MaxFileSize,
      };
   }

   public class Attachments
   {
      public List<Attachment> Items { get; set; }
      /// <summary>
      /// Unique name to create controls
      /// </summary>
      public string Name { get; set; }

      public Attachments() { }
      public Attachments(string name)
      {
         Name = name;
         Items = new List<Attachment>();
      }
      public Attachments(string name, IEnumerable<DataModel.Attachment> attachments, Func<DataModel.Attachment, string> customFileName = null)
      {
         Name = name;
         Items = attachments.OrderBy(it => it.Name)
            .Select(it =>
            {
               var r = new Attachment(it);
               if (customFileName != null)
                  r.SaveAs = customFileName(it);
               return r;
            }).ToList();
      }

      public void Update(XPCollection<DataModel.Attachment> collection, string folder, string filePrefix)
      {
         //this items list contains changed only
         if (Items == null || Items.Count == 0) return;
         foreach (var it in Items)
         {
            //add
            if (it.Id < 0)
            {
               var att = new DataModel.Attachment(collection.Session)
               {
                  Name = it.Name
               };
               collection.Add(att);
               //move out tempory folder
               var fTemp = System.Web.Hosting.HostingEnvironment.MapPath("/Content/Tempory");
               var fDest = System.Web.Hosting.HostingEnvironment.MapPath("/Content/" + folder);
               if (!System.IO.Directory.Exists(fDest)) System.IO.Directory.CreateDirectory(fDest);
               var newName = $"{filePrefix}_{it.Url}";
               System.IO.File.Move(System.IO.Path.Combine(fTemp, it.Url), System.IO.Path.Combine(fDest, newName));
               att.FileUrl = $"/Content/{folder}/" + newName;
            }
            else if (it.Deleted)//deleted
            {
               var att = collection.FirstOrDefault(el => el.Oid == it.Id);
               if (att != null)
               {
                  collection.Remove(att);
                  att.Delete();
               }
            }
            else //update
            {
               var att = collection.FirstOrDefault(el => el.Oid == it.Id);
               if (att != null)
                  att.Name = it.Name;
            }
         }
      }
   }
}