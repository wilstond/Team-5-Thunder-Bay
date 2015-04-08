using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult Index()
        {
            return View();
        }
    }
}