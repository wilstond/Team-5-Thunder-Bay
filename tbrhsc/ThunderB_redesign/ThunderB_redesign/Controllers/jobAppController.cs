using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class jobAppController : Controller
    {
        //
        // GET: /jobApp/
        jobAppLinqClass objJob = new jobAppLinqClass();

        [HttpGet]
        public ActionResult Create(int job_id)
        {

            return View();
        }

        //
        // POST: /jobApplication/Create

        [HttpPost]
        public ActionResult Create(jobApplication app)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitInsert(app);
                    return RedirectToAction("Index", "Job");
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