using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class FeedbackController : Controller
    {

        FeedbackLinqClass objFeedback = new FeedbackLinqClass();

        MenuLinqClass menuObj = new MenuLinqClass();

        public FeedbackController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

        }

        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(feedback feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objFeedback.commitInsert(feedback);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
}