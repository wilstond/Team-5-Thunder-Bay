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
        JobLinqClass objJob = new JobLinqClass();

        public ActionResult Index()
        {
            var allJobs = objJob.GetJobs();
            return View(allJobs);
        }

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