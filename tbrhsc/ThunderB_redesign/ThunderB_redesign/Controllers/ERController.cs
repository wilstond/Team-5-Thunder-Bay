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
        static int numDoctors;
        MenuLinqClass menuObj = new MenuLinqClass();
        TriageViewModel triageObj = new TriageViewModel();

        public ERController()
        {
            //create main menu
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

            // ViewBag.emergencyList = triageObj.getEmergencyList();

        }


        public string CalcWaitTime()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                // get all current er cases

                model.ERpatients = model.getERPatients().ToList();

                // get doctors assigned to ER from doctors table

                model.ERdoctors = model.getERdoctors();

                // docStats contains an associative array ("doc_id" => "wait time for this dr_id") 
                // used to select minimum wait time and dr_id associated with it

                Dictionary<int, TimeSpan> docStats = new Dictionary<int, TimeSpan>();

                numDoctors = model.ERdoctors.Count();

                foreach (doctor doc in model.ERdoctors)
                {
                    docStats.Add(doc.dr_id, model.getWaitTimes(doc.dr_id));
                }

                // now selecting minimum waiting time for displaying in the ER Monitor

                System.TimeSpan minWaitTime = docStats.OrderBy(x => x.Value).First().Value;

                return minWaitTime.ToString("%h'hr. '%m'min.'");
            }

        }

        // GET: ER
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)] // this attribute prevents browser from using cached value for wait time
        public ActionResult Monitor()
        {
            ViewBag.TotalWait = CalcWaitTime();

            return PartialView("Monitor");
        }
    }
}