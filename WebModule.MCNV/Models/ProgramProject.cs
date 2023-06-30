using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class ProgramProject
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.ProgramProject> GetData()
         {
            var db = ApplicationDbContext.Current;
            return db.Session.QueryByCompany<DataModel.ProgramProject>();
         }
      }

      public int Id { get; set; }
      [Caption("Tên dự án"), Required]
      public string Name { get; set; }
      [Caption("Ghi chú"), Template("MultilineText")]
      public string Note { get; set; }

      public ProgramProject() { }

      public ProgramProject(DataModel.ProgramProject model)
      {
         Id = model.Oid;
         Name = model.Name;
         Note = model.Note;
      }

      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.ProgramProject model;
         if (Id == 0)
            model = new DataModel.ProgramProject(db.Session) { };
         else if (!TryGetModel(Id, out model)) return;
         model.Name = Name;
         model.Note = Note;

         db.Session.CommitChanges();
         Id = model.Oid;
      }

      public static ProgramProject Create() => new ProgramProject();

      public static ProgramProject Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new ProgramProject(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.ProgramProject model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.ProgramProject>(id);
         if (model == null || model.Company != db.CompanyId) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.QueryByCompany<DataModel.ProgramProject>()
         select new { el.Oid, el.Name };

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.QueryByCompany<DataModel.Program>()
                  where el.Project.Oid == id
                  select el).Count();
         return c == 0;
      }
   }
}