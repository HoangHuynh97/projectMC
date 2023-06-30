using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace WebApp.Models
{
   public class CompanyUser
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public object GetData()
         {
            var db = Core.ApplicationDbContext.Current;
            var users = QueryUser != null ? QueryUser() : db.Session.Query<DataModel.User>();
            var company = db.CompanyId.Value;
            var query = from u in users
                        where u.Companies.Any(p => p.Oid == company) && !u.Roles.Any(p => p.Role == Constant.Role.System)
                        orderby u.UserName
                        select new
                        {
                           u.Oid,
                           u.UserName,
                           u.FullName,
                           u.Address,
                           u.Email,
                           u.Phone,
                           u.Active,
                           u.Note,
                           Admin = u.Roles.Any(p => p.Role == Constant.Role.Admin),
                           u.Permission,
                           u.GroupId,
                           u.DataId,
                        };
            if (!db.IsInRole(Constant.Role.Admin))
               query = from u in query
                       where !u.Admin
                       select u;
            Dictionary<int, string> groups = UserGroups?.Invoke();
            Dictionary<int, string> datas = UserDatas?.Invoke();
            var lst = query.ToList().Select(u => new
            {
               u.Oid,
               u.UserName,
               u.FullName,
               u.Address,
               u.Email,
               u.Phone,
               u.Active,
               u.Note,
               u.Admin,
               u.Permission,
               Group = groups != null && u.GroupId.HasValue && groups.TryGetValue(u.GroupId.Value, out var v) ? v : "",
               Data = datas != null && u.DataId.HasValue && datas.TryGetValue(u.DataId.Value, out var d) ? d : "",
            });
            return lst;
         }
      }

      public int Id { get; set; }
      [Core.Caption("Tên đăng nhập")]
      [Core.Required]
      [Core.Remote("CheckUserName", "CompanyUser", "Tên đăng nhập đã có đăng ký", AdditionalFields = "Id")]
      public string UserName { get; set; }
      [Core.Caption("Họ và tên")]
      [Core.Required]
      public string FullName { get; set; }
      [Core.Caption("Địa chỉ")]
      public string Address { get; set; }
      [Core.Caption("Email")]
      [Core.Email]
      [Core.Required]
      [Core.Remote("CheckEmail", "CompanyUser", "Email đã có đăng ký", AdditionalFields = "Id")]
      public string Email { get; set; }
      [Core.Caption("Điện thoại")]
      public string Phone { get; set; }
      [Core.Caption("Hình ảnh")]
      public byte[] Image { get; set; }
      [Core.Caption("Đang sử dụng")]
      public bool Active { get; set; }
      [Core.Caption("Ghi chú")]
      [Core.Template("MultilineText")]
      public string Note { get; set; }
      [Core.Required(OnlyIf = "IsNew")]
      [Core.Caption("Mật khẩu"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string Password { get; set; }
      [Core.Required(OnlyIf = "IsNew"), Core.Equal("Password", "Không khớp với mật khẩu")]
      [Core.Caption("Nhập lại mật khẩu"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string Verify { get; set; }
      [Core.Required]
      public int? Permission { get; set; }
      [Core.Required(OnlyIf = "HasGroupId")]
      public int? GroupId { get; set; }
      [Core.Required(OnlyIf = "HasDataId")]
      public int? DataId { get; set; }
      internal static bool CheckUserName(int id, string userName)
      {
         var model = Core.ApplicationDbContext.Current.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("Oid!=? && UserName=?", id, userName));
         return model == null;
      }

      internal static bool CheckEmail(int id, string email)
      {
         var model = Core.ApplicationDbContext.Current.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("Oid!=? && Email=?", id, email));
         return model == null;
      }

      public CompanyUser()
      {
      }

      public CompanyUser(DataModel.User model)
      {
         Id = model.Oid;
         UserName = model.UserName;
         FullName = model.FullName;
         Address = model.Address;
         Email = model.Email;
         Phone = model.Phone;
         Image = model.Image;
         Active = model.Active;
         Note = model.Note;
         if (model.Roles.Any(p => p.Role == Constant.Role.Admin))
            Permission = 0;
         else
            Permission = model.Permission?.Oid;
         GroupId = model.GroupId;
         DataId = model.DataId;
      }

      public bool IsNew()
      {
         return Id == 0;
      }

      public void Save()
      {
         DataModel.User model;
         if (Id == 0)
         {
            var mn = HttpContext.Current.GetOwinContext().Get<Core.ApplicationUserManager>();
            var idr = mn.Create(new Core.ApplicationUser()
            {
               UserName = UserName,
               FullName = FullName,
               Email = Email,
               EmailConfirmed = true
            }, Password);
            if (!idr.Succeeded)
               throw new Exception(string.Join("<br/>", idr.Errors));
            model = Core.ApplicationDbContext.Current.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("UserName=?", UserName));
            model.Companies.Add(Core.ApplicationDbContext.Current.CompanyModel);
         }
         else
            model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(Id);
         var companyIds = Core.ApplicationDbContext.Current.UserModel.GetAccessCompanies().Select(p => p.Oid).ToList();
         if (model == null || !model.Companies.Any(p => companyIds.Contains(p.Oid))) return;
         model.UserName = UserName;
         model.FullName = FullName;
         model.Address = Address;
         model.Phone = Phone;
         model.Email = Email;
         model.Image = Image;
         model.Active = Active;
         model.Note = Note;
         if (Permission == 0)
            model.Permission = null;
         if (Permission == 0 && !model.Roles.Any(p => p.Role == Constant.Role.Admin))
            model.Roles.Add(new DataModel.UserRole(model.Session) { User = model, Role = Constant.Role.Admin });
         else if (Permission > 0)
         {
            model.Roles.FirstOrDefault(p => p.Role == Constant.Role.Admin)?.Delete();
            model.Permission = model.Session.GetObjectByKey<DataModel.Permission>(Permission);
         }
         model.GroupId = GroupId;
         model.DataId = DataId;
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public bool HasGroupId() => UserGroups != null;
      public bool HasDataId() => UserDatas != null && Permission != 0;

      public static CompanyUser Create()
      {
         var vm = new CompanyUser()
         {
            Active = true
         };
         return vm;
      }

      public static CompanyUser Load(int id)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(id);
         var companyIds = Core.ApplicationDbContext.Current.UserModel.GetAccessCompanies().Select(p => p.Oid).ToList();
         if (model == null || !model.Companies.Any(p => companyIds.Contains(p.Oid))) return null;
         return new CompanyUser(model);
      }

      public static void Delete(int id)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(id);
         var companyIds = Core.ApplicationDbContext.Current.UserModel.GetAccessCompanies().Select(p => p.Oid).ToList();
         if (model == null || !model.Companies.Any(p => companyIds.Contains(p.Oid))) return;
         model.Delete();
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public class ChangePassword
      {
         [Core.Required]
         public int Id { get; set; }
         [Core.Required]
         [Core.Caption("Mật khẩu mới"), Core.Template("Password")]
         [Core.Length(6, 20)]
         public string Password { get; set; }
         [Core.Required, Core.Equal("Password", "Không khớp với mật khẩu mới")]
         [Core.Caption("Nhập lại mật khẩu mới"), Core.Template("Password")]
         [Core.Length(6, 20)]
         public string Verify { get; set; }
      }

      public static void SetPassword(int id, string passwordHash)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(id);
         var companyIds = Core.ApplicationDbContext.Current.UserModel.GetAccessCompanies().Select(p => p.Oid).ToList();
         if (model == null || !model.Companies.Any(p => companyIds.Contains(p.Oid))) return;
         model.PasswordHash = passwordHash;
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public List<KeyValuePair<int, string>> GetPermissions()
      {
         var result = new List<KeyValuePair<int, string>>();
         var db = Core.ApplicationDbContext.Current;
         if (db.IsInRole(Constant.Role.Admin))
            result.Add(new KeyValuePair<int, string>(0, Core.Language.T("Toàn quyền sử dụng")));
         var pms = (from r in db.Session.Query<DataModel.Permission>()
                    where !r.System && r.Company == db.CompanyId.Value
                    orderby r.Name
                    select r).ToList();
         //check can use permission role
         var context = HttpContext.Current?.Request.RequestContext.HttpContext;
         if (context == null)
            throw new Exception("No httpcontext to check permission");
         if (!db.IsInRole(Constant.Role.Admin))
         {
            pms = pms.Where(pm =>
            {
               var f = pm.Details.SelectMany(it => it.Logics.Split(',').Select(l => new { it.Module, it.Function, Logic = l }))
               .All(it => context.CheckPermission(it.Module, it.Function, it.Logic));
               if (!f) return false;
               var r = pm.Reports.All(it => context.CheckReport(it.Oid));
               if (!r) return false;
               return true;
            }).ToList();
         }

         result.AddRange(pms.ToList().Select(p => new KeyValuePair<int, string>(p.Oid, p.Name)));
         return result;
      }

      public static bool CheckMaxUser()
      {
         var db = Core.ApplicationDbContext.Current;
         var max = db.CompanyModel.MaxUser;
         var cur = Convert.ToInt32(db.Session.Evaluate<DataModel.User>(CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("Companies[Oid=?] && !Roles[Role=?]", db.CompanyId.Value, Constant.Role.System)));
         return cur < max;
      }

      public static bool CanUpdateAdmin(int id)
      {
         var db = Core.ApplicationDbContext.Current;
         if (db.IsInRole(Constant.Role.Admin))
            return true;
         var model = db.Session.GetObjectByKey<DataModel.User>(id);
         var isAdmin = model != null && model.Roles.Any(p => p.Role == Constant.Role.Admin);
         return !isAdmin;
      }

      public static bool CanUpdateUser(int id)
      {
         var db = Core.ApplicationDbContext.Current;
         var model = db.Session.GetObjectByKey<DataModel.User>(id);
         if (model == null) return false;
         if (db.IsInRole(Constant.Role.Admin) || QueryUser == null)
            return true;
         return QueryUser().Select(el => el.Oid).Contains(model.Oid);
      }


      /// <summary>
      /// Integrate filter users outside system
      /// </summary>
      public static Func<IQueryable<DataModel.User>> QueryUser { get; set; }
      /// <summary>
      /// Integrate user groups outside system
      /// </summary>
      public static Func<Dictionary<int, string>> UserGroups { get; set; }
      /// <summary>
      /// Integrate user's permission data outside system
      /// </summary>
      public static Func<Dictionary<int, string>> UserDatas { get; set; }

      /// <summary>
      /// Implement extend editors for user group
      /// </summary>
      public static Func<System.Web.Mvc.HtmlHelper<CompanyUser>, System.Web.Mvc.MvcHtmlString> UserGroupsView { get; set; }

      /// <summary>
      /// Implement extend editors for user permission data
      /// </summary>
      public static Func<System.Web.Mvc.HtmlHelper<CompanyUser>, System.Web.Mvc.MvcHtmlString> UserDatasView { get; set; }

   }
}