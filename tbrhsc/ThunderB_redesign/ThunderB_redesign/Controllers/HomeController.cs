using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class HomeController : Controller
    {

        MenuLinqClass menuObj = new MenuLinqClass();

        public HomeController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

        }

        public ActionResult Index()
        {
            return View();
        }


    }
}
