using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class doctorController : Controller
    {
        MenuLinqClass menuObj = new MenuLinqClass();
        LinqDataContext db = new LinqDataContext();

         public doctorController()
         {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string searchTerm = null)
        {
            return View("SearchResult", searchTerm);
        }
        
        
        public ActionResult SearchResult(string searchTerm = null)
        {
            var model = db.doctors.Where(x => searchTerm == null || x.dr_name.Contains(searchTerm)).Select(x => x);
            return View(model);
        }


    }
}
