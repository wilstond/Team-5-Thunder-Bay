﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class HomeController : Controller
    {

        MenuLinqClass menuObj = new MenuLinqClass();
        NewsPostLinq newsObj = new NewsPostLinq();

        public HomeController()
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
            var lastNews = newsObj.getTopNews().First();
            return View(lastNews);
        }



    }
}
