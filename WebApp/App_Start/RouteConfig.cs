using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
   internal class RouteConfig
   {
      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
         
         //custom route
         routes.MapRoute(
             name: "Login",
             url: "Login",
             namespaces: new[] { "WebApp.Controllers" },
             defaults: new { controller = "Home", action = "Login" }
         );

         //route of modules
         var ams = System.Web.Compilation.BuildManager.GetReferencedAssemblies().OfType<System.Reflection.Assembly>().Where(a => a.ManifestModule.Name.StartsWith("WebModule."));
         Core.Logger.Info("Router config: " + string.Join(", ", ams.Select(p => p.ManifestModule.Name)));
         foreach (var a in ams)
         {
            var n = a.ManifestModule.Name.Split('.')[1];
            routes.MapRoute(
                name: n, // Route name
                url: "{module}/{controller}/{action}/{id}", // URL with parameters
                constraints: new { module = n },
                namespaces: new[] { a.ManifestModule.Name.Replace(".dll", ".Controllers") },
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
         }

         routes.MapRoute(
             name: "Default", // Route name
             url: "{controller}/{action}/{id}", // URL with parameters
             namespaces: new[] { "WebApp.Controllers" },
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
         );
      }
   }
}