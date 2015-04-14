using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class NewsPostPublicController : Controller
    {

        
        NewsPostPublic objNews = new NewsPostPublic();
        MenuLinqClass menuObj = new MenuLinqClass();


        public NewsPostPublicController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

        }

        public ActionResult Index()
        {
            var allNews = objNews.getNews();
            if (allNews == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(allNews);
            }
        }

        public PartialViewResult topNews()
        {
            var topNews = objNews.getTopNews();
            return PartialView(topNews);
        }


        public ActionResult NotFound()
        {
            return View();
        }
    }


}



