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

        //PublicDisplayMenu objMenu = new PublicDisplayMenu();


        //
        // GET: /Menu/

       // public ActionResult _MenuList()
       // {

         //   var allMenuItems = objMenu.getMenuItems();
         //   return View(allMenuItems);

       // }

        
        MenuLinqClass menuObj = new MenuLinqClass();

        public PageController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

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


        public ActionResult Index(int menu_id = 0)
        {
            if (menu_id != 0)
            {
                var AllPages = objPage.getVisiblePages();

                AllPages = objPage.getPagesByMenu(menu_id);
                return View(AllPages);

            }
            else
            {
                var AllPages = objPage.getVisiblePages();

                return View(AllPages);
            }
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
            if(selectPage == null)
            {
                // Redirect the user to the main page
                return RedirectToAction("Index", "Home");
            }
            int user_id = selectPage.user_id;
            //Display Details of the Page

            //ViewData["author"] = user_id;

            
            return View(selectPage);

        }

    }
}
