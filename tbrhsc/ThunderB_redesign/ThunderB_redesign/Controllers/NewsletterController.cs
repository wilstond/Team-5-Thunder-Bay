using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Postal;

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
                
                //Send a notification to the subscriber
                dynamic email = new Email("NewsletterSubscription");
                email.to = sub.sub_email;
                email.name = sub.sub_name;
                email.Send();
                return PartialView();
            }
            else
            {
                return PartialView("NewsletterForm");
            }
        }

    }
}