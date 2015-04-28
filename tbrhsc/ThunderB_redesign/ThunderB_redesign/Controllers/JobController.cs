using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class JobController : Controller
    {

        MenuLinqClass menuObj = new MenuLinqClass();
        JobLinqClass objJob = new JobLinqClass();

        public JobController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }
      
        //getting the list of jobs
        public ActionResult Index()
        {
            var allJobs = objJob.GetJobs();
            return View(allJobs);
        }


        //detail view of the selected job
        public ActionResult Details(int id)
        {
            var job = objJob.getJobByID(id);
            if (job == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(job);
            }
        }
    }
}