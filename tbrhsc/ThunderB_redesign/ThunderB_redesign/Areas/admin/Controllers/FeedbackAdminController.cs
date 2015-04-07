using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    [Authorize]
    public class FeedbackAdminController : Controller
    {
        FeedbackLinqClass objFeedback = new FeedbackLinqClass();
        public ActionResult Index()
        {
            var feedback = objFeedback.getfeedback();
            return View(feedback);
        }

        public ActionResult Details(int id)
        {
            var feedback = objFeedback.getFeedbackbyID(id);
            if (feedback == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(feedback);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                objFeedback.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
            //var feedback = objFeedback.getFeedbackbyID(id);
            //if (feedback == null)
            //{
            //    return View("NotFound");
            //}
            //else
            //{
            //    return View(feedback);
            //}
        }

        [HttpPost]
        public ActionResult Delete(int id, feedback feedback)
        {
            try
            {
                objFeedback.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }


}