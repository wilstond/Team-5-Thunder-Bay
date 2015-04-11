using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

using Postal;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class NewsletterAdminController : Controller
    {

        SubscriberClass objSubscriber = new SubscriberClass();

        // GET: admin/NewsletterAdmin
        public ActionResult Index()
        {
            var allSubscribers = objSubscriber.getSubscribers();
            return View(allSubscribers);
        }

        public ActionResult Details(int id)
        {
            var sub = objSubscriber.getSubscriberByID(id);
            if (sub == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(sub);
            }
        }

        public ActionResult Update(int id)
        {
            var sub = objSubscriber.getSubscriberByID(id);
            if (sub == null)
            {
                return View("NotFound");
            }

            return View(sub);
        }

        [HttpPost]
        public ActionResult Update(int sub_id, subscriber sub)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objSubscriber.commitUpdate(sub_id, sub.sub_name, sub.sub_email);
                    return RedirectToAction("Details/" + sub_id);
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            var prod = objSubscriber.getSubscriberByID(id);
            if (prod == null)
            {
                return View("NotFound");
            }

            return View(prod);
        }

        [HttpPost]
        public ActionResult Delete(int id, product prod)
        {
            try
            {
                objSubscriber.commitDelete(id);
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


        public ActionResult SendNewsletter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendNewsletter(NewsletterModel newsletter)
        {
            // Code to send newsletter via email

            dynamic email = new Email("NewsletterMail");
            email.subject = newsletter.subject;
            email.message = newsletter.message;

            //Get all subscribers

            var allSubscribers = objSubscriber.getSubscribers();
            foreach (var item in allSubscribers)
            {
                email.to = item.sub_email;
                email.Send();
            }

            return RedirectToAction("NewsletterSent");
        }

        public ActionResult NewsletterSent()
        {
            return View();
        }

    }
}