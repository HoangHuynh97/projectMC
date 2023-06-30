using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Web.Hosting;
using System.IO;
using System.Text.RegularExpressions;

namespace WebApp.Core
{
   public class EmailProvider
   {
      public string Server { get; set; }
      public int Port { get; set; }

      public string UserName { get; set; }
      public string Password { get; set; }
      public bool Ssl { get; set; }
      public string FromEmail { get; set; }
      public EmailProvider() { }

      public bool Send(string to, string subject, string content)
      {
         var msg = new MailMessage(FromEmail, to, subject, content);
         msg.BodyEncoding = Encoding.UTF8;
         msg.IsBodyHtml = true;

         var client = new SmtpClient(Server, Port)
         {
            UseDefaultCredentials = true,
            EnableSsl = Ssl,
            Credentials = new NetworkCredential(UserName, Password)
         };
         try
         {
            client.Send(msg);
            return true;
         }
         catch (Exception ex)
         {
            Logger.Error(ex);
            return false;
         }
      }

      public bool SendTemplate(string to, string subject, string templateName, Dictionary<string, string> replacer = null)
      {
         var path = HostingEnvironment.MapPath($"/App_Data/{templateName}");
         if (!File.Exists(path))
         {
            Logger.Error($"Not found email template App_Data/{templateName}");
            return false;
         }
         var content = File.ReadAllText(path);
         if (replacer != null)
            content = templateRegex.Replace(content, match => replacer[match.Groups[1].Value]);
         return Send(to, subject, content);
      }

      private static readonly Regex templateRegex = new Regex(@"{{(\w+)}}", RegexOptions.Compiled);
      private static EmailProvider instance;
      public static EmailProvider Instance
      {
         get
         {
            if (instance == null)
            {
               var stt = System.Web.Configuration.WebConfigurationManager.AppSettings;
               instance = new EmailProvider()
               {
                  Server = stt["SmtpServer"] ?? "",
                  Port = int.Parse(stt["SmtpPort"] ?? "25"),
                  UserName = stt["SmtpUserName"] ?? "",
                  Password = stt["SmtpPassword"] ?? "",
                  Ssl = bool.Parse(stt["SmtpSsl"] ?? "false"),
               };
               instance.FromEmail = stt["SmtpEmail"] ?? instance.UserName;
            }
            return instance;
         }
      }
   }
}