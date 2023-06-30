using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Data;

namespace WebApp.Core
{
   public class DtoProvider : MSSqlConnectionProvider
   {
      public DtoProvider(IDbConnection connection, AutoCreateOption autoCreateOption)
        : base(connection, autoCreateOption)
      {
      }

      protected override object ReformatReadValue(object value, ReformatReadValueArgs args)
      {
         // This implementation deactivates the default behavior of the base 
         // class logic, because the conversion step is not necessary for the type
         // DateTimeOffset, and because the attempt at conversion
         // results in exceptions since there is no automatic conversion mechanism.
         if (value != null)
         {
            Type valueType = value.GetType();
            if (valueType == typeof(DateTimeOffset))
               return value;
         }
         return base.ReformatReadValue(value, args);
      }
   }

   public class DtoConverter : ValueConverter
   {
      public override object ConvertFromStorageType(object value)
      {
         // We're ignoring the request to convert here, knowing that the loaded
         // object is already the correct type because SqlClient returns it 
         // that way.
         return value;
      }

      public override object ConvertToStorageType(object value)
      {
         if (value is DateTimeOffset)
         {
            var dto = (DateTimeOffset)value;
            return dto.ToString("yyyy-MM-dd HH:mm:ss.fffffff zzz");
         }
         else
            return value;
      }

      public override Type StorageType
      {
         get { return typeof(string); }
      }
   }
}