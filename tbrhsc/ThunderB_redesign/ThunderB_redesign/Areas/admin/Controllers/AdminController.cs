using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ThunderB_redesign.Filters;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class DashboardController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }      

    }
}
