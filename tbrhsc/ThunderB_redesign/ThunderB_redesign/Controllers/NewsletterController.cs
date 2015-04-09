using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult NewsletterForm()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ProcessSubscription(subscriber sub)
        {
            if (ModelState.IsValid)
            {
                var objSub = new SubscriberClass();
                objSub.commitInsert(sub);
                return PartialView();
            }
            else
            {
                return PartialView("NewsletterForm");
            }
        }

    }
}