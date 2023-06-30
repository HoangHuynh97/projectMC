using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Core
{
   public class ApplicationUser : IUser<int>
   {
      public ApplicationUser()
      {
         Roles = new List<string>();
         Logins = new Dictionary<string, string>();
      }

      internal ApplicationUser(DataModel.User model)
      {
         Id = model.Oid;
         UserName = model.UserName;
         FullName = model.FullName;
         Email = model.Email;
         EmailConfirmed = model.EmailConfirmed;
         PasswordHash = model.PasswordHash;
         SecurityStamp = model.SecurityStamp;
         Active = model.Active;
         Roles = model.Roles.Select(p => p.Role).ToList();
         Logins = model.Logins.ToDictionary(p => p.LoginProvider, p => p.ProviderKey);
         var data = model.GetAccessCompanies();
         if (data.Count == 1)
            Company = data[0].Oid;
         else if (model.CurrentCompany.HasValue && data.Any(c => c.Oid == model.CurrentCompany.Value))
            Company = model.CurrentCompany;
         //roles
         if (!Roles.Contains(Constant.Role.Admin) && model.Permission != null)
         {
            Permissions = model.Permission.Details.ToDictionary(p => p.Module + "/" + p.Function, p => p.Logics);
            Reports = model.Permission.Reports.Select(p => p.Oid).ToList();
         }
      }

      public int Id { get; private set; }
      public string UserName { get; set; }
      public string FullName { get; set; }
      public string Email { get; set; }
      public bool EmailConfirmed { get; set; }
      public string PasswordHash { get; set; }
      public string SecurityStamp { get; set; }
      public bool Active { get; private set; }
      public List<string> Roles { get; private set; }
      public Dictionary<string, string> Logins { get; private set; }
      public int? Company { get; private set; }
      private Dictionary<string, string> Permissions { get; set; }
      private List<int> Reports { get; set; }

      internal void Update(DataModel.User model)
      {
         model.UserName = UserName;
         model.FullName = FullName;
         model.Email = Email;
         model.EmailConfirmed = EmailConfirmed;
         model.PasswordHash = PasswordHash;
         model.SecurityStamp = SecurityStamp;
         //update roles
         model.Session.Delete(model.Roles.Where(p => !Roles.Contains(p.Role)).ToArray());
         foreach (var r in Roles)
         {
            var t = model.Roles.FirstOrDefault(p => p.Role == r);
            if (t == null)
            {
               t = new DataModel.UserRole(model.Session)
               {
                  User = model,
                  Role = r
               };
               model.Roles.Add(t);
            }
         }

         //update logins
         model.Session.Delete(model.Logins.Where(p => Logins.All(l => l.Key != p.LoginProvider)).ToArray());
         foreach (var l in Logins)
         {
            var t = model.Logins.FirstOrDefault(p => p.LoginProvider == l.Key);
            if (t == null)
            {
               t = new DataModel.UserLogin(model.Session)
               {
                  User = model,
                  LoginProvider = l.Key
               };
               model.Logins.Add(t);
            }
            t.ProviderKey = l.Value;
         }
      }

      public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
      {
         // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
         var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
         // Add custom user claims here
         userIdentity.AddClaim(new Claim(Constant.Claim.Company, Company.ToString()));
         if (Permissions != null)
            userIdentity.AddClaims(Permissions.Select(p => new Claim(Constant.Claim.Permission + p.Key, p.Value)));
         if (Reports != null)
            userIdentity.AddClaim(new Claim(Constant.Claim.OpenReport, string.Join(",", Reports)));
         return userIdentity;
      }

      public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
      {
         return Task.FromResult(GenerateUserIdentity(manager));
      }
   }

   public class UserStore :
      IUserStore<ApplicationUser, int>,
      IUserPasswordStore<ApplicationUser, int>,
      IUserEmailStore<ApplicationUser, int>,
      IUserSecurityStampStore<ApplicationUser, int>,
      IUserLockoutStore<ApplicationUser, int>,
      IUserTwoFactorStore<ApplicationUser, int>,
      IUserRoleStore<ApplicationUser, int>,
      IUserLoginStore<ApplicationUser, int>
   {
      public UserStore() { }

      private ApplicationDbContext _db;
      public UserStore(ApplicationDbContext db)
      {
         _db = db;
      }

      public Task CreateAsync(ApplicationUser user)
      {
         var model = new DataModel.User(_db.Session)
         {
            Active = true
         };
         user.Update(model);
         _db.Session.CommitChanges();
         return Task.FromResult(0);
      }

      public Task UpdateAsync(ApplicationUser user)
      {
         var model = _db.Session.GetObjectByKey<DataModel.User>(user.Id);
         user.Update(model);
         _db.Session.CommitChanges();
         return Task.FromResult(0);
      }

      public Task DeleteAsync(ApplicationUser user)
      {
         var model = _db.Session.GetObjectByKey<DataModel.User>(user.Id);
         model.Delete();
         _db.Session.CommitChanges();
         return Task.FromResult(0);
      }

      public Task<ApplicationUser> FindByIdAsync(int userId)
      {
         var model = _db.Session.GetObjectByKey<DataModel.User>(userId);
         return Task.FromResult(model != null ? new ApplicationUser(model) : null);
      }

      public Task<ApplicationUser> FindByNameAsync(string userName)
      {
         var model = _db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("UserName=?", userName));
         return Task.FromResult(model != null ? new ApplicationUser(model) : null);
      }

      public void Dispose()
      {
      }

      public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
      {
         user.PasswordHash = passwordHash;
         return Task.FromResult(0);
      }

      public Task<string> GetPasswordHashAsync(ApplicationUser user)
      {
         return Task.FromResult(user.PasswordHash);
      }

      public Task<bool> HasPasswordAsync(ApplicationUser user)
      {
         return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
      }

      public Task SetEmailAsync(ApplicationUser user, string email)
      {
         user.Email = email;
         return Task.FromResult(0);
      }

      public Task<string> GetEmailAsync(ApplicationUser user)
      {
         return Task.FromResult(user.Email);
      }

      public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
      {
         return Task.FromResult(user.EmailConfirmed);
      }

      public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
      {
         user.EmailConfirmed = confirmed;
         return Task.FromResult(0);
      }

      public Task<ApplicationUser> FindByEmailAsync(string email)
      {
         var model = _db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("Email=?", email));
         return Task.FromResult(model != null ? new ApplicationUser(model) : null);
      }

      public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
      {
         user.SecurityStamp = stamp;
         return Task.FromResult(0);
      }

      public Task<string> GetSecurityStampAsync(ApplicationUser user)
      {
         return Task.FromResult(user.SecurityStamp);
      }

      public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
      {
         throw new NotImplementedException();
      }

      public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
      {
         throw new NotImplementedException();
      }

      public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
      {
         throw new NotImplementedException();
      }

      public Task ResetAccessFailedCountAsync(ApplicationUser user)
      {
         throw new NotImplementedException();
      }

      public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
      {
         return Task.FromResult(0);
      }

      public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
      {
         return Task.FromResult(false);
      }

      public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
      {
         throw new NotImplementedException();
      }

      public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
      {
         throw new NotImplementedException();
      }

      public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
      {
         return Task.FromResult(false);
      }

      public Task AddToRoleAsync(ApplicationUser user, string roleName)
      {
         user.Roles.Add(roleName);
         return Task.FromResult(0);
      }

      public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
      {
         user.Roles.Remove(roleName);
         return Task.FromResult(0);
      }

      public Task<IList<string>> GetRolesAsync(ApplicationUser user)
      {
         return Task.FromResult((IList<string>)user.Roles);
      }

      public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
      {
         return Task.FromResult(user.Roles.Contains(roleName));
      }

      public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
      {
         user.Logins[login.LoginProvider] = login.ProviderKey;
         return Task.FromResult(0);
      }

      public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
      {
         user.Logins.Remove(login.LoginProvider);
         return Task.FromResult(0);
      }

      public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
      {
         return Task.FromResult((IList<UserLoginInfo>)user.Logins.Select(p => new UserLoginInfo(p.Key, p.Value)).ToList());
      }

      public Task<ApplicationUser> FindAsync(UserLoginInfo login)
      {
         var user = _db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("Logins[LoginProvider=? && ProviderKey=?]", login.LoginProvider, login.ProviderKey));
         return Task.FromResult(user != null ? new ApplicationUser(user) : null);
      }
   }


   #region config service
   public class EmailService : IIdentityMessageService
   {
      public Task SendAsync(IdentityMessage message)
      {
         // Plug in your email service here to send an email.
         EmailProvider.Instance.Send(message.Destination, message.Subject, message.Body);
         return Task.FromResult(0);
      }
   }

   public class SmsService : IIdentityMessageService
   {
      public Task SendAsync(IdentityMessage message)
      {
         // Plug in your SMS service here to send a text message.
         return Task.FromResult(0);
      }
   }

   public class ApplicationDbContext : IDisposable
   {
      public static ApplicationDbContext Create()
      {
         return new ApplicationDbContext();
      }

      public ApplicationDbContext()
      {
         Session = new UnitOfWork();
      }

      public UnitOfWork Session { get; private set; }

      public int? UserId
      {
         get
         {
            return HttpContext.Current?.User?.Identity.GetUserId<int>();
         }
      }

      private DataModel.User userModel;
      public DataModel.User UserModel
      {
         get
         {
            if (userModel == null || userModel.Oid != UserId)
            {
               var id = UserId;
               if (id.HasValue)
                  userModel = Session.GetObjectByKey<DataModel.User>(id.Value);
            }
            return userModel;
         }
      }

      public int? CompanyId
      {
         get
         {
            var c = (HttpContext.Current?.User?.Identity as ClaimsIdentity)?.FindFirstValue(Constant.Claim.Company);
            if (!string.IsNullOrEmpty(c))
               return Convert.ToInt32(c);
            return null;
         }
      }

      private DataModel.Company companyModel;
      public DataModel.Company CompanyModel
      {
         get
         {
            if (companyModel == null || companyModel.Oid != CompanyId)
            {
               var id = CompanyId;
               if (id.HasValue)
                  companyModel = Session.GetObjectByKey<DataModel.Company>(id.Value);
            }
            return companyModel;
         }
      }

      public bool IsInRole(string role)
      {
         var b = HttpContext.Current?.User?.IsInRole(role);
         if (b.HasValue)
            return b.Value;
         return false;
      }

      public bool CheckPermission(string function, string logic = "")
      {
         var context = HttpContext.Current.Request.RequestContext.HttpContext;
         var module = System.Reflection.Assembly.GetCallingAssembly().ManifestModule.Name;
         return System.Web.Mvc.Helpers.CheckPermission(context, module, function, logic);
      }

      public static ApplicationDbContext Current
      {
         get
         {
            var cur = HttpContext.Current;
            if (cur == null)
               throw new Exception("There are no HttpContext");
            return cur.GetOwinContext().Get<ApplicationDbContext>();
         }
      }

      public void Dispose()
      {
         Session.Dispose();
      }
   }

   // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
   public class ApplicationUserManager : UserManager<ApplicationUser, int>
   {
      public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
          : base(store)
      {
      }

      public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
      {
         var manager = new ApplicationUserManager(new UserStore(context.Get<ApplicationDbContext>()));
         // Configure validation logic for usernames
         manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
         {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
         };
         // Configure validation logic for passwords
         manager.PasswordValidator = new PasswordValidator
         {
            RequiredLength = 6,
            RequireNonLetterOrDigit = false,
            RequireDigit = false,
            RequireLowercase = false,
            RequireUppercase = false,
         };

         // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
         // You can write your own provider and plug it in here.

         manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, int>
         {
            MessageFormat = "Your security code is {0}"
         });
         manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, int>
         {
            Subject = "Security Code",
            BodyFormat = "Your security code is {0}"
         });


         // Configure user lockout defaults
         manager.UserLockoutEnabledByDefault = false;
         manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
         manager.MaxFailedAccessAttemptsBeforeLockout = 5;

         manager.EmailService = new EmailService();
         manager.SmsService = new SmsService();
         var dataProtectionProvider = options.DataProtectionProvider;
         if (dataProtectionProvider != null)
         {
            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("WebApp Identity"));
         }

         manager.GenerateSystemUser();

         return manager;
      }

      private static bool _checkedSystemUser = false;
      private void GenerateSystemUser()
      {
         if (_checkedSystemUser) return;
         _checkedSystemUser = true;
         var user = FindByNameAsync("System");
         if (user.Result != null) return;
         Logger.Info("Create system user");
         var system = new ApplicationUser()
         {
            UserName = "System",
            FullName = "System",
            Email = "hientrung@hotmail.com",
            EmailConfirmed = true
         };
         system.Roles.AddRange(new string[] { Constant.Role.System, Constant.Role.Admin });
         this.Create(system, "system1/?");
      }
   }

   public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
   {
      public ApplicationSignInManager(UserManager<ApplicationUser, int> userManager, IAuthenticationManager authenticationManager)
         : base(userManager, authenticationManager)
      {
      }

      public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
      {
         return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
      }

      public override Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser)
      {
         //check user can login
         if (!user.Active)
            throw new Exception("Tài khoản đã bị khóa");
         return base.SignInAsync(user, isPersistent, rememberBrowser);
      }

      public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
      {
         return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
      }
   }
   #endregion
}