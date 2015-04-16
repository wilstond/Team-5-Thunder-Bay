using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class ShiftController : Controller
    {
        ShiftLinqClass objShift = new ShiftLinqClass();

        MenuLinqClass menuObj = new MenuLinqClass();

        public ShiftController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

        }
        // GET: Shift
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(volshift shift)
        {
            if (ModelState.IsValid)
            {
              
                    objShift.commitInsert(shift);
                    return RedirectToAction("Thanks");
               
                    //return View();
                
            }

            return View();
        }
    }
}