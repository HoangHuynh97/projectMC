using System;
using System.Reflection;

namespace WebApp.Core
{
   public class Logger
   {
      static Logger()
      {
         log4net.Config.XmlConfigurator.Configure();
      }

      public static void Debug(object message, Exception exception = null)
      {
         var log = log4net.LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name);
         log.Debug(message, exception);
      }

      public static void Error(object message, Exception exception = null)
      {
         var log = log4net.LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name);
         log.Error(message, exception);
      }

      public static void Info(object message, Exception exception = null)
      {
         var log = log4net.LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name);
         log.Info(message, exception);
      }

      public static void Fatal(object message, Exception exception = null)
      {
         var log = log4net.LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name);
         log.Fatal(message, exception);
      }

      public static void Warn(object message, Exception exception = null)
      {
         var log = log4net.LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name);
         log.Warn(message, exception);
      }
   }
}