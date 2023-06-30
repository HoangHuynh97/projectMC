using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace WebApp.Models
{
   public class UserModel
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable GetData()
         {
            var query = from u in Core.ApplicationDbContext.Current.Session.Query<DataModel.User>()
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
                           System = u.Roles.Any(p => p.Role == Constant.Role.System),
                           Admin = u.Roles.Any(p => p.Role == Constant.Role.Admin),
                           u.Permission
                        };
            return query;
         }
      }

      public int Id { get; set; }
      [Core.Caption("Tên đăng nhập")]
      [Core.Required]
      [Core.Remote("CheckUserName", "User", "Tên đăng nhập đã có đăng ký", AdditionalFields = "Id")]
      public string UserName { get; set; }
      [Core.Caption("Họ và tên")]
      [Core.Required]
      public string FullName { get; set; }
      [Core.Caption("Địa chỉ")]
      public string Address { get; set; }
      [Core.Caption("Email")]
      [Core.Email]
      [Core.Required]
      [Core.Remote("CheckEmail", "User", "Email đã có đăng ký", AdditionalFields = "Id")]
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
      [Core.Caption("Người dùng hệ thống")]
      public bool System { get; set; }
      [Core.Required(OnlyIf = "IsNew")]
      [Core.Caption("Mật khẩu"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string Password { get; set; }
      [Core.Required(OnlyIf = "IsNew"), Core.Equal("Password", "Không khớp với mật khẩu")]
      [Core.Caption("Nhập lại mật khẩu"), Core.Template("Password")]
      [Core.Length(6, 20)]
      public string Verify { get; set; }
      public List<int> CompanyIds { get; set; }
      public int Permission { get; set; }

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

      public UserModel()
      {
         CompanyIds = new List<int>();
      }

      public UserModel(DataModel.User model)
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
         System = model.Roles.Any(p => p.Role == Constant.Role.System);
         CompanyIds = model.Companies.Select(p => p.Oid).ToList();
         if (!model.Roles.Any(p => p.Role == Constant.Role.Admin) && model.Permission != null)
            Permission = model.Permission.Oid;
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
         }
         else
            model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(Id);
         if (model == null) return;
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
         if (System && !model.Roles.Any(p => p.Role == Constant.Role.System))
            model.Roles.Add(new DataModel.UserRole(model.Session) { User = model, Role = Constant.Role.System });
         else if (!System)
            model.Roles.FirstOrDefault(p => p.Role == Constant.Role.System)?.Delete();
         //add/remove company
         var del = model.Companies.Where(p => !CompanyIds.Contains(p.Oid)).ToList();
         foreach (var d in del)
            model.Companies.Remove(d);
         var add = CompanyIds.Where(p => !model.Companies.Any(c => c.Oid == p)).ToList();
         foreach (var a in add)
            model.Companies.Add(model.Session.GetObjectByKey<DataModel.Company>(a));

         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public List<KeyValuePair<int, string>> GetListCompany()
      {
         var db = Core.ApplicationDbContext.Current;
         var xpc = new XPCollection<DataModel.Company>(db.Session);
         xpc.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
         return xpc.Select(p => new KeyValuePair<int, string>(p.Oid, p.Name)).ToList();
      }

      public static UserModel Create()
      {
         var vm = new UserModel()
         {
            Active = true
         };
         return vm;
      }

      public static UserModel Load(int id)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(id);
         if (model == null) return null;
         return new UserModel(model);
      }

      public static void Delete(int id)
      {
         var model = Core.ApplicationDbContext.Current.Session.GetObjectByKey<DataModel.User>(id);
         if (model == null) return;
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
         if (model == null) return;
         model.PasswordHash = passwordHash;
         Core.ApplicationDbContext.Current.Session.CommitChanges();
      }

      public List<KeyValuePair<int, string>> GetPermissions()
      {
         var result = new List<KeyValuePair<int, string>>
         {
            new KeyValuePair<int, string>(0, Core.Language.T("Toàn quyền sử dụng"))
         };
         var xpc = new XPCollection<DataModel.Permission>(Core.ApplicationDbContext.Current.Session);
         xpc.Sorting.Add(new SortProperty("System", DevExpress.Xpo.DB.SortingDirection.Descending));
         xpc.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
         result.AddRange(xpc.Select(p => new KeyValuePair<int, string>(p.Oid, p.Name)));
         return result;
      }
      
      public static bool CreateResetPasswordLink(string email, out string id, out string userName)
      {
         id = "";
         userName = "";
         var db = Core.ApplicationDbContext.Current;
         var user = db.Session.FindObject<DataModel.User>(CriteriaOperator.Parse("Email==?", email));
         if (user == null) return false;
         id = Guid.NewGuid().ToString();
         userName = user.UserName;
         user.ResetPasswordId = id;
         user.ResetPasswordExpire = DateTime.Now.AddHours(2);
         db.Session.CommitChanges();
         return true;
      }
   }
}