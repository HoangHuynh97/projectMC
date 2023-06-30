using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace WebModule.MCNV.Models
{
   public class Diagnostic
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public IQueryable<DataModel.Diagnostic> GetData()
         {
            var db = ApplicationDbContext.Current;
            var query = from el in db.Session.Query<DataModel.Diagnostic>()
                        where el.Company == db.CompanyId.Value
                        select el;
            return query;
         }
      }

      public int Id { get; set; }
      [Caption("Mã")]
      public string Code { get; set; }
      [Caption("Tên"), Required, Length(300)]
      public string Name { get; set; }
      [Caption("Nhóm bệnh"), Required, Template("DiagnosticType")]
      public int? DiagnosticType { get; set; }

      public Diagnostic() { }
      public Diagnostic(DataModel.Diagnostic model)
      {
         Id = model.Oid;
         Code = model.Code;
         Name = model.Name;
         DiagnosticType = model.DiagnosticType?.Oid;
      }
      public void Save()
      {
         var db = ApplicationDbContext.Current;
         DataModel.Diagnostic model;
         if (Id == 0)
            model = new DataModel.Diagnostic(db.Session);
         else if (!TryGetModel(Id, out model)) return;
         var b = Id != 0 && model.Name != Name;
         model.Code = Code;
         model.Name = Name;
         if (DiagnosticType == null)
            model.DiagnosticType = null;
         else if (Models.DiagnosticType.TryGetModel(DiagnosticType.Value,out var type))
            model.DiagnosticType = type;
         db.Session.CommitChanges();
         Id = model.Oid;
         if (b) UpdateDiagnosticsStr(Id);
      }

      public static Diagnostic Create() => new Diagnostic();

      public static Diagnostic Load(int id)
      {
         if (!TryGetModel(id, out var model)) return null;
         return new Diagnostic(model);
      }

      public static void Delete(int id)
      {
         if (!TryGetModel(id, out var model)) return;
         model.Delete();
         ApplicationDbContext.Current.Session.CommitChanges();
      }

      public static bool TryGetModel(int id, out DataModel.Diagnostic model)
      {
         var db = ApplicationDbContext.Current;
         model = db.Session.GetObjectByKey<DataModel.Diagnostic>(id);
         if (model == null || model.Company != db.CompanyId.Value) return false;
         return true;
      }

      public static IQueryable ComboboxData() =>
         from el in ApplicationDbContext.Current.Session.Query<DataModel.Diagnostic>()
         where el.Company == ApplicationDbContext.Current.CompanyId.Value
         orderby el.Name ascending
         select new { el.Oid, el.Code, el.Name };

      public static bool CanDelete(int id)
      {
         var db = ApplicationDbContext.Current;
         var c = (from el in db.Session.Query<DataModel.Medical>()
                  from d in el.Diagnostics
                  where d.Oid == id
                  select d).Count();
         return c == 0;
      }

      /// <summary>
      /// Reupdate DiagnosticsStr in Medical due to change diagnostic's name
      /// </summary>
      public static void UpdateDiagnosticsStr(int diagnosticId)
      {
         var db = ApplicationDbContext.Current;
         var lst = new XPCollection<DataModel.Medical>(db.Session, CriteriaOperator.Parse("Diagnostics[Oid=?]", diagnosticId));
         foreach (var it in lst)
         {
            it.IsAudit = false;
            var names = it.Diagnostics.Select(el => el.Name).ToList();
            it.DiagnosticsStr = String.Join("; ", names);
         }
         db.Session.CommitChanges();
      }
   }
}