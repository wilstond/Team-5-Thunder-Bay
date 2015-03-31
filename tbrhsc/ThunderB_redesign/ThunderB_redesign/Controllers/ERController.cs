using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class ERController : Controller
    {
        Int16 numDoctors = 2;
        MenuLinqClass menuObj = new MenuLinqClass();

        public ERController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

        }


        public TimeSpan CalcWaitTime()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                TimeSpan TotalWait = new TimeSpan();
                foreach (triage item in model.ERpatients)
                {
                    System.TimeSpan diff = item.discharge.Subtract(item.arrival);
                    TotalWait += diff;

                }

                var resWaitTime = new TimeSpan(TotalWait.Ticks / numDoctors);
                return resWaitTime;
            }

        }

        // GET: ER
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 3)] // every 3 sec
        public ActionResult Monitor()
        {
            ViewBag.TotalWait = CalcWaitTime();
            ViewBag.numDoctors = numDoctors;
            return PartialView("Monitor");
        }
    }
}