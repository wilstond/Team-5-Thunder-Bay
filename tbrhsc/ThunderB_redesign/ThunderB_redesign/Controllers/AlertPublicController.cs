using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class AlertPublicController : Controller
    {
        AlertModelClass objAlert = new AlertModelClass();
        // GET: Alert
        public ActionResult Index()
        {
            //grabbing the id of the activated row
            int alertid = objAlert.getAlert();

            //sending the id through the viewbag 
            ViewBag.AlertId = alertid;

            //returning the partialview associated with the id that was passed through the viewbag
            return PartialView();
        }
    }
}