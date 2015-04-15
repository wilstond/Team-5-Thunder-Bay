using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class jobAppController : Controller
    {
        //
        // GET: /jobApp/
        jobAppLinqClass objApp = new jobAppLinqClass();
        MenuLinqClass menuObj = new MenuLinqClass();

        public jobAppController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }

        [HttpGet]
        public ActionResult Create(int job_id)
        {

            return View();
        }

        //
        // POST: /jobApplication/Create

        [HttpPost]
        public ActionResult Create(jobApplication app)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objApp.commitInsert(app);
                    return View("Thanks",app);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Thanks(jobApplication app)
        {
            return View(app);
        }


    }
}