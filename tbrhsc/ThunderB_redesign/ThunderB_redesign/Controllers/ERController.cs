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
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

            ViewBag.emergencyList = triageObj.getEmergencyList();

        }


        public string CalcWaitTime()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();
                model.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
                Dictionary<int, TimeSpan> docStats = new Dictionary<int, TimeSpan>();
                Dictionary<string, TimeSpan> docOutput = new Dictionary<string, TimeSpan>();
                numDoctors = model.ERdoctors.Count();

                foreach (doctor doc in model.ERdoctors)
                {
                    docStats.Add(doc.dr_id, model.getWaitTimes(doc.dr_id));
                    docOutput.Add(doc.dr_name, model.getWaitTimes(doc.dr_id));
                }

                ViewBag.docOutput = docOutput;

                System.TimeSpan minWaitTime = docStats.OrderBy(x => x.Value).First().Value;

                return minWaitTime.ToString("%h'hr. '%m'min.'");
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