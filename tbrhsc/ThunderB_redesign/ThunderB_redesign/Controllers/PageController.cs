using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class PageController : Controller
    {
        PageLinqClass objPage = new PageLinqClass();

        

        
        MenuLinqClass menuObj = new MenuLinqClass();

        public PageController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

            ViewBag.breadCrumbs = menuObj.getBreadcrumbList();

        }

        public ActionResult _PageList(int menu_id = 0)
        {
            var AllPages = objPage.getVisiblePages();
            if (menu_id != 0)
            {
                AllPages = objPage.getPagesByMenu(menu_id);
            }
            return View(AllPages);
        }


        public ActionResult Index(int menu_id)
        {
            

                var AllPages = objPage.getPagesByMenu(menu_id);
                
                if (AllPages.Any())
                {
                    var LandPage = AllPages.First();
                    ViewBag.ListOfPages = AllPages.ToList();
                    ViewBag.subMenuItems = menuObj.getSubMenuItemsByParentId(menu_id).ToList();

                    return View(LandPage);
                }

                return View("PageNotFound");
                            
        }

        //[SlugToId]
        //public ActionResult Content(int id)
        //{
        //    var slug = RouteData.Values["slug"] as string;
        //    ViewData["slug"] = slug;
        //    ViewData["id"] = id;

        //    var selectPage = objPage.getPageByID(id);


        //    //Display Details of the Page
        //    return View(selectPage);

        //}
        public ActionResult Detail(string page_slug)
        {

            var selectPage = objPage.getPageBySlug(page_slug);
            var relatedPages = objPage.getPagesByMenu(selectPage.menu_id);
            ViewBag.relatedPages = relatedPages.ToList();
            if(selectPage == null)
            {
                // Redirect the user to the main page
                return RedirectToAction("Index", "Home");
            }
            int user_id = selectPage.user_id;
            int page_id = selectPage.page_id;
            var pageById = objPage.getPageByID(page_id);
            //Display Details of the Page

            //ViewData["author"] = user_id;

            return View(pageById);
            //return View(selectPage);

        }

        public ActionResult PageNotFound()
        {
            return View();
        }

    }
}
