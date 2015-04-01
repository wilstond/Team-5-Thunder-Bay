using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    [Authorize]

    public class AlertController : Controller
    {
        // GET: admin/Alert
        AlertModelClass objAlert = new AlertModelClass();
        public ActionResult Index()
        {
            //grabbing everything from the table rows
            var alerts = objAlert.getAlerts();
            //returning all possible alerts
            return View(alerts);
        }




        //were going to make a new page, this page will handle what happens whent he post button is hit
        //loading the page by itself
        //when the page is loaded with hitting post
        public ActionResult AlertPost()
        {
            return View();
        }





        //when post is hit, the page that is loaded is this one, which evaluates what to do when the button is hit
        [HttpPost]
        public ActionResult AlertPost(string alerts)
        {
            objAlert.activateAlert(Convert.ToInt32(alerts));
            return View();
        }

    }


}