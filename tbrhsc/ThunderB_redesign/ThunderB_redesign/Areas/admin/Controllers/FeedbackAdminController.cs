using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    //authorizing the page to protect against going to the page directly without logging in
    [Authorize]


    public class FeedbackAdminController : Controller
    {
        //connectin got the feedback table
        FeedbackLinqClass objFeedback = new FeedbackLinqClass();
        public ActionResult Index()
        {
            var feedback = objFeedback.getfeedback();
            return View(feedback);
        }

        //fetching the data for each feedback by their IDs
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

        //When the delete button is pressed the user is redirected back to the index page of the feedback
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
       
        }

        //when the delete button is posted the feedback is deleted by its ID
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