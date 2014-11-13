using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GiftsSystem.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //  name: "Add products to category",
            //  url: "Category/Details/{id}/Product/Create",
            //  defaults: new { controller = "Product", action = "Create", id = UrlParameter.Optional }
            //  );
            routes.MapRoute(
               name: "Add products to category",
               url: "Product/Create/{name}",
               defaults: new { controller = "Product", action = "Create", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "Category details",
               url: "Category/Details/{id}",
               defaults: new { controller = "Category", action = "Details", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "Edit category",
                url: "Category/Edit/{name}",
                defaults: new { controller = "Category", action = "Edit", name = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
