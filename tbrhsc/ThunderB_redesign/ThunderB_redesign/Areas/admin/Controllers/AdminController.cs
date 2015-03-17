using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class DashboardController : Controller
    {

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
