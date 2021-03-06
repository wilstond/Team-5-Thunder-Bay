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
                     name: "AccountRoute",
                     url: "Account",
                     defaults: new { controller = "Account", action = "Login" }
                 );
                
               routes.MapRoute(
                     name: "DonationRoute",
                     url: "Donation",
                     defaults: new { controller = "Donation", action = "Index"}
                 );

                routes.MapRoute(
                      name: "FeedbackRoute",
                      url: "Feedback",
                      defaults: new { controller = "Feedback", action = "Index"}
                  );
                routes.MapRoute(
                     name: "AboutUsRoute",
                     url: "About-Us",
                     defaults: new { controller = "Page", action = "Index", menu_id = "101" }
                 );

                routes.MapRoute(
                      name: "FaqRoute",
                      url: "Faq",
                      defaults: new { controller = "Faq", action = "Index" }
                  );

                routes.MapRoute(
                      name: "AlertRoute",
                      url: "Alert",
                      defaults: new { controller = "Alert", action = "Index" }
                  );

                routes.MapRoute(
                      name: "AppointmentsRoute",
                      url: "Appointment",
                      defaults: new { controller = "Appointment", action = "Form" }
                  );

                routes.MapRoute(
                       name: "PageAdmin_upload",
                       url: "admin/PageAdmin/uploadPartial",
                       defaults: new { controller = "PageAdmin", action = "uploadPartial" }
                   );

                routes.MapRoute(
                       name: "BackToHome",
                       url: "Page/Index",
                       defaults: new {  controller = "Page", action = "Index"}
                   );

 //Custom Route to fetch page by friendly url instead of id
               routes.MapRoute(
                      name: "GetPageBySlug",
                      url: "Page/{page_slug}",
                      defaults: new { controller = "Page", action = "Detail"}
                  );

                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

                

                routes.MapRoute("Home", "", new { controller = "Home", action = "index", id = UrlParameter.Optional }
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
   