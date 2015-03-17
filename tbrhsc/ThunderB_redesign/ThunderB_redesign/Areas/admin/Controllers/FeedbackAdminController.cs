using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class FeedbackAdminController : Controller
    {
        FeedbackLinqClass objFeedback = new FeedbackLinqClass();
        public ActionResult Index()
        {
            var feedback = objFeedback.getfeedback();
            return View(feedback);
        }
    }
}