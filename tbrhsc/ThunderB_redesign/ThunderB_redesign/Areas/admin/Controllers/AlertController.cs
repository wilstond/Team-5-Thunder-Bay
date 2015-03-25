using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Areas.admin.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class AlertController : Controller
    {
        // GET: admin/Alert
        AlertModelClass objAlert = new AlertModelClass();
        public ActionResult Index()
        {
            var alerts = objAlert.getAlerts();
            return View(alerts);
        }
    }


}