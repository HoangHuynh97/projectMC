using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using DevExpress.Data.Filtering;

namespace WebApp.Core
{
   [Authorize]
   public class MessageHub : Hub
   {
      static readonly ConcurrentDictionary<string, HashSet<string>> online = new ConcurrentDictionary<string, HashSet<string>>();

      public static Dictionary<string, int> GetOnline()
      {
         return online.ToDictionary(v => v.Key, v => v.Value.Count);
      }

      public MessageHub() { }

      void UpdateLogin()
      {
         var name = Context.User?.Identity?.Name;
         if (string.IsNullOrEmpty(name))
            return;
         var ids = online.GetOrAdd(name, _ => new HashSet<string>());
         lock (ids)
         {
            //notify login
            if (ids.Count == 0)
            {
               var session = new UnitOfWork();
               var user = session.GetObjectByKey<DataModel.User>(Context.User.Identity.GetUserId<int>());
               user.LastLogin = DateTimeOffset.Now;
               if (user.Roles.Any(r => r.Role == Constant.Role.System))
               {
                  new DataModel.AuditData(session)
                  {
                     AuditType = "Đăng nhập",
                     Description = string.Format("{0} đăng nhập vào hệ thống", user.UserName),
                     User = user.Oid
                  };
               }
               else
               {
                  foreach (var c in user.Companies)
                  {
                     new DataModel.AuditData(session)
                     {
                        AuditType = "Đăng nhập",
                        Description = string.Format("{0} đăng nhập vào hệ thống", user.UserName),
                        User = user.Oid,
                        Company = c.Oid
                     };
                  }
               }
               session.CommitChanges();

               Clients.Users(GetAdmins()).message("loggedIn", name);
            }
            ids.Add(Context.ConnectionId);
         }
      }

      async void UpdateLogout()
      {
         var name = Context.User?.Identity?.Name;
         if (string.IsNullOrEmpty(name))
            return;
         await Task.Delay(10000); //avoid refresh page, signalr disconnect and connect again
         if (online.TryGetValue(name, out HashSet<string> ids))
         {
            lock (ids)
            {
               ids.Remove(Context.ConnectionId);
               if (ids.Count == 0)
               {
                  online.TryRemove(name, out HashSet<string> _);

                  //notify logout
                  var session = new UnitOfWork();
                  var user = session.GetObjectByKey<DataModel.User>(Context.User.Identity.GetUserId<int>());
                  if (user.Roles.Any(r => r.Role == Constant.Role.System))
                  {
                     new DataModel.AuditData(session)
                     {
                        AuditType = "Thoát",
                        Description = string.Format("{0} thoát hệ thống", user.UserName),
                        User = user.Oid
                     };
                  }
                  else
                  {
                     foreach (var c in user.Companies)
                     {
                        new DataModel.AuditData(session)
                        {
                           AuditType = "Thoát",
                           Description = string.Format("{0} thoát hệ thống", user.UserName),
                           User = user.Oid,
                           Company = c.Oid
                        };
                     }
                  }
                  session.CommitChanges();

                  Clients.Users(GetAdmins()).message("loggedOut", name);
               }
            }
         }
      }

      List<string> GetAdmins()
      {
         var result = new List<string>();
         var session = new Session();
         var user = session.GetObjectByKey<DataModel.User>(Context.User.Identity.GetUserId<int>());
         if (user == null)
            throw new Exception("Not found user");
         //access company
         var comps = string.Join(",", user.GetAccessCompanies().Select(c => c.Oid));
         if (string.IsNullOrEmpty(comps))
            comps = "0";//pass syntax error

         //user system
         var systemUsers = new XPCollection<DataModel.User>(session, CriteriaOperator.Parse("Oid!=? && Active && Roles[Role==?] && Roles[Role==?]", user.Oid, Constant.Role.System, Constant.Role.Admin));
         result.AddRange(systemUsers.Select(u => u.UserName));

         //user admin
         if (!user.Roles.Any(p => p.Role == Constant.Role.System))
         {
            var adminUsers = new XPCollection<DataModel.User>(session, CriteriaOperator.Parse("This!=? && Active && Roles[Role=?] && Companies[Oid in (" + comps + ")]", user.Oid, Constant.Role.Admin));
            result.AddRange(adminUsers.Select(u => u.UserName));
         }

         return result;
      }

      public override Task OnConnected()
      {
         UpdateLogin();
         return base.OnConnected();
      }

      public override Task OnReconnected()
      {
         UpdateLogin();
         return base.OnReconnected();
      }

      public override Task OnDisconnected(bool stopCalled)
      {
         UpdateLogout();
         return base.OnDisconnected(stopCalled);
      }
   }
}