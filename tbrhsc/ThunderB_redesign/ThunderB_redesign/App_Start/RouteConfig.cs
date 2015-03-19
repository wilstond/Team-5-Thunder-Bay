﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThunderB_redesign
{
    public class RouteConfig
    {
           public static void RegisterRoutes(RouteCollection routes)
            {
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

                routes.MapRoute("Home","", new { controller = "Home", action = "index", id = UrlParameter.Optional }
                );


            } 
         }

}


    /*   
     public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{slug}/{id}",
                defaults: new { controller = "Home", action = "Index", slug = UrlParameter.Optional, id = UrlParameter.Optional }
            );


        }
     */
   