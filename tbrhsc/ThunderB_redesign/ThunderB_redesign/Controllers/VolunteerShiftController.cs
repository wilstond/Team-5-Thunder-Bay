using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class VolunteerShiftController : Controller
    {
        VolunteerLinqClass objVolunteer = new VolunteerLinqClass();

        MenuLinqClass menuObj = new MenuLinqClass();

        public VolunteerShiftController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }

        // GET: VolunteerShift
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string[]days)
        {

            
            if (ModelState.IsValid)
            {
                Shift objShift=new Shift();
                
                try
                {
                    for (var i = 0; i <= 6; i++)
                    {
                        if (days[i] != "None")
                        {
                            objShift.volunteer_id = 1;
                            objShift.day = i.ToString();
                            objShift.shifts = days[i];
                            objVolunteer.commitInsert(objShift);
                           }
                        
                    }
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