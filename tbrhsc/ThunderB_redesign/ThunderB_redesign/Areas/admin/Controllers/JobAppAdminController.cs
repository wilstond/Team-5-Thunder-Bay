using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class JobAppAdminController : Controller
    {
        // GET: admin/JobApp
        jobAppLinqClass objJobApp = new jobAppLinqClass();
        JobLinqClass objJob = new JobLinqClass();


        public ActionResult Index([Bind(Prefix = "id")] int jobId)
        {

            var job = objJob.getJobByID(jobId);
            return View(job);
        }


        public ActionResult _jobApplications(int job_id)
        {
            var applications = objJobApp.GetJobApplicationsByJobId(job_id);
            return PartialView(applications);
        }


        // GET: admin/JobApp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/JobApp/Create

        public ActionResult Delete(int id)
        {
            var app = objJobApp.getAppByID(id);
            if (app == null)
            {
                return View("NotFound");
            }
            return View(app);
        }

        // POST: admin/JobApp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, jobApplication app)
        {
            try
            {
                objJobApp.commitDelete(id);
                return RedirectToAction("Index","JobAdmin");
            }
            catch
            {
                return View();
            }
        }
    }
}
