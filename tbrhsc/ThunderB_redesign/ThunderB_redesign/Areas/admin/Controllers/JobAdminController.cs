using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
     [Authorize]
    public class JobAdminController : Controller
    {
        JobLinqClass objJob = new JobLinqClass();
        LinqDataContext db = new LinqDataContext();

        //getting job categories to the viewbag
        public JobAdminController()
        {
            List<jobCategory> objCategories = new List<jobCategory>();
            IQueryable<jobCategory> allCategories = db.jobCategories.Select(x => x);
            foreach (jobCategory cat in allCategories)
            {
                objCategories.Add(cat);
            }
            ViewBag.objCategories = objCategories;
        }

        public ActionResult Index()
        {
            var allJobs = objJob.GetJobs();
            return View(allJobs);
        }
        
        // GET: /Admin/Create
         
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(Job job)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitInsert(job);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {

            var job = objJob.getJobByID(id);
            if (job == null)
            {
                return View("NotFound");
            }
            return View(job);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Job job)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitUpdate(id, job.job_title, job.closing_date, job.job_description,
                        job.category_id);
                   return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            var prod = objJob.getJobByID(id);
            if (prod == null)
            {
                return View("NotFound");
            }
            return View(prod);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Job job)
        {
            try
            {
                objJob.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }




    }
}