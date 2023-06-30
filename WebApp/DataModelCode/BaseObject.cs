using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using WebApp.Core;
using System.Collections.Generic;

namespace WebApp.DataModel
{

   public partial class BaseObject
   {
      private struct Change
      {
         public string PropertyName;
         public string OldValue;
         public string NewValue;
      }

      public BaseObject(Session session) : base(session)
      {
         var t = GetType();
         IsAudit = t.IsDefined(typeof(AuditAttribute), false);
         if (IsAudit)
         {
            var attr = (AuditAttribute)t.GetCustomAttributes(typeof(AuditAttribute), false)[0];
            AuditName = attr.Name;
            AuditKey = attr.PropertyKey;
            AuditObjectType = t.FullName;
         }
      }

      [NonPersistent]
      public bool IsAudit { get; set; }
      [NonPersistent]
      public string AuditName { get; set; }
      [NonPersistent]
      public string AuditKey { get; set; }
      [NonPersistent]
      public string AuditObjectType { get; set; }
      private List<Change> changes = new List<Change>();

      public override void AfterConstruction()
      {
         fDateCreate = DateTimeOffset.Now;
         UserCreate = ApplicationDbContext.Current.UserId.Value;
         base.AfterConstruction();
      }

      private DateTimeOffset fDateCreate;
      [ValueConverter(typeof(DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset DateCreate
      {
         get { return fDateCreate; }
         set { SetPropertyValue("DateCreate", ref fDateCreate, value); }
      }

      private DateTimeOffset fDateModify;
      [ValueConverter(typeof(DtoConverter))]
      [DbType("datetimeoffset")]
      public DateTimeOffset DateModify
      {
         get { return fDateModify; }
         set { SetPropertyValue("DateModify", ref fDateModify, value); }
      }

      protected override void OnChanged(string propertyName, object oldValue, object newValue)
      {
         base.OnChanged(propertyName, oldValue, newValue);
         if (IsLoading || Session.IsNewObject(this) || IsDeleted || !IsAudit
            || propertyName == "DateCreate" || propertyName == "DateModify" || propertyName == "UserCreate" || propertyName == "UserModify"
            ) return;
         var c = new Change
         {
            PropertyName = propertyName,
            OldValue = AuditValueToString(propertyName, oldValue),
            NewValue = AuditValueToString(propertyName, newValue)
         };
         changes.Add(c);
      }

      protected virtual string AuditValueToString(string property, object value)
      {
         if (value == null) return "";
         //get value of audit object
         var t = value.GetType();
         if (t.IsDefined(typeof(AuditAttribute), false) && value is BaseObject)
         {
            var attr = (AuditAttribute)t.GetCustomAttributes(typeof(AuditAttribute), false)[0];
            var obj = (BaseObject)value;
            return string.Format("{0}", obj.Evaluate(attr.PropertyKey));
         }

         if (value is DateTime || value is DateTime?)
         {
            DateTime d;
            if (value is DateTime) d = (DateTime)value;
            else d = (value as DateTime?).Value;
            if (d.Second > 0) return d.ToString("dd/MM/yyyy HH:mm:ss");
            else return d.ToString("dd/MM/yyyy");
         }

         if (value is DateTimeOffset)
         {
            return ((DateTimeOffset)value).ToString("yyyy - MM - dd HH: mm:ss.fffffff zzz");
         }

         if (value is decimal)
         {
            return ((decimal)value).ToString("#,###.##");
         }

         return value.ToString();
      }

      private bool _isNew = false;
      protected override void OnSaving()
      {
         fDateModify = DateTimeOffset.Now;
         UserModify = ApplicationDbContext.Current.UserId.Value;
         _isNew = Session.IsNewObject(this);
         base.OnSaving();
      }

      protected override void OnSaved()
      {
         base.OnSaved();
         //update audit for new/modify
         if (IsAudit && (changes.Count > 0 || _isNew) && !IsDeleted)
         {
            var audit = new AuditData(Session)
            {
               AuditType = _isNew ? "Thêm" : "Sửa",
               ObjectType = AuditObjectType,
               ObjectId = Convert.ToInt32(Session.GetKeyValue(this)),
               User = ApplicationDbContext.Current.UserId.Value
            };
            audit.Description = string.Format("{0} {1}: {2}", audit.AuditType, AuditName, Evaluate(AuditKey));
            if (!ApplicationDbContext.Current.IsInRole(Constant.Role.System))
               audit.Company = ApplicationDbContext.Current.CompanyId;
            foreach (var c in changes)
            {
               var data = new DataModel.AuditChange(audit.Session)
               {
                  Audit = audit,
                  PropertyName = c.PropertyName,
                  OldValue = c.OldValue,
                  NewValue = c.NewValue
               };
               audit.Changes.Add(data);
            }
            changes.Clear();
            ApplicationDbContext.Current.Session.CommitChanges();
         }
      }

      protected override void OnDeleting()
      {
         base.OnDeleting();
         if (IsAudit)
         {
            var audit = new AuditData(Session)
            {
               AuditType = "Xóa",
               ObjectType = AuditObjectType,
               ObjectId = Convert.ToInt32(Session.GetKeyValue(this)),
               User = ApplicationDbContext.Current.UserId.Value
            };
            audit.Description = string.Format("{0} {1}: {2}", audit.AuditType, AuditName, Evaluate(AuditKey));
            if (!ApplicationDbContext.Current.IsInRole(Constant.Role.System))
               audit.Company = ApplicationDbContext.Current.CompanyId;
         }
      }
   }

}
