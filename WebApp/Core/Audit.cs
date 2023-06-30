using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Core
{
   public class Audit
   {
      public static void Add(string type, string description)
      {
         var model = new DataModel.AuditData(ApplicationDbContext.Current.Session)
         {
            AuditType = type,
            Description = description,
            User = ApplicationDbContext.Current.UserId.Value
         };
         if (!ApplicationDbContext.Current.IsInRole(Constant.Role.System))
            model.Company = ApplicationDbContext.Current.CompanyId;
      }

      public class AuditInfo
      {
         public int Id { get; set; }
         public DateTime AuditDate { get; set; }
         public string AuditType { get; set; }
         public string Description { get; set; }
         public string UserName { get; set; }
      }

      public static List<AuditInfo> GetAudits(Type objType, int? objId = null, DateTime? fromDate = null, DateTime? toDate = null)
      {
         return GetAudits(objType.Name, objId, fromDate, toDate);
      }

      public static List<AuditInfo> GetAudits(string objType, int? objId = null, DateTime? fromDate = null, DateTime? toDate = null)
      {
         var users = new XPCollection<DataModel.User>(ApplicationDbContext.Current.Session);
         var audits = new XPCollection<DataModel.AuditData>(ApplicationDbContext.Current.Session);
         var c = new GroupOperator(GroupOperatorType.And, CriteriaOperator.Parse("ObjectType = ?", objType));
         if (!ApplicationDbContext.Current.IsInRole(Constant.Role.System))
            c.Operands.Add(CriteriaOperator.Parse("Company=?", ApplicationDbContext.Current.CompanyId.Value));
         if (objId.HasValue)
            c.Operands.Add(CriteriaOperator.Parse("ObjectId = ?", objId.Value));
         if (fromDate.HasValue)
            c.Operands.Add(CriteriaOperator.Parse("AuditDate >= ?", new DateTimeOffset(fromDate.Value)));
         if (toDate.HasValue)
            c.Operands.Add(CriteriaOperator.Parse("AuditDate < ?", new DateTimeOffset(toDate.Value.AddDays(1))));
         audits.Criteria = c;
         audits.Sorting.Add(new SortProperty("AuditDate", DevExpress.Xpo.DB.SortingDirection.Descending));
         var result = audits.Select(a => new AuditInfo() { Id = a.Id, AuditDate = a.AuditDate.LocalDateTime, AuditType = a.AuditType, Description = a.Description, UserName = users.FirstOrDefault(u => u.Oid == a.User)?.UserName });
         return result.ToList();
      }

      public static List<AuditInfo> GetAudits(DateTime fromDate, DateTime toDate)
      {
         var users = new XPCollection<DataModel.User>(ApplicationDbContext.Current.Session);
         var audits = new XPCollection<DataModel.AuditData>(ApplicationDbContext.Current.Session);
         var c = new GroupOperator(GroupOperatorType.And, CriteriaOperator.Parse("AuditDate >= ? && AuditDate < ?", new DateTimeOffset(fromDate), new DateTimeOffset(toDate.AddDays(1))));
         if (!ApplicationDbContext.Current.IsInRole(Constant.Role.System))
            c.Operands.Add(CriteriaOperator.Parse("Company=?", ApplicationDbContext.Current.CompanyId.Value));
         audits.Criteria = c;
         audits.Sorting.Add(new SortProperty("AuditDate", DevExpress.Xpo.DB.SortingDirection.Descending));
         var result = audits.Select(a => new AuditInfo() { Id = a.Id, AuditDate = a.AuditDate.LocalDateTime, AuditType = a.AuditType, Description = a.Description, UserName = users.FirstOrDefault(u => u.Oid == a.User)?.UserName });
         return result.ToList();
      }

      public class AuditDataInfo
      {
         public int Id { get; set; }
         public string PropertyName { get; set; }
         public string OldValue { get; set; }
         public string NewValue { get; set; }
      }

      public static List<AuditDataInfo> GetAuditData(int auditId)
      {
         var db = ApplicationDbContext.Current;
         var data = new XPCollection<DataModel.AuditChange>(db.Session);
         data.Criteria = CriteriaOperator.Parse("Audit = ?", auditId);
         var result = data.Select(p => new AuditDataInfo() { Id = p.Id, PropertyName = p.PropertyName, OldValue = p.OldValue, NewValue = p.NewValue });
         return result.ToList();
      }
   }
}