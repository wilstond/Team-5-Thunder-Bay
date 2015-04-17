using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;
using PagedList;

namespace ThunderB_redesign.Controllers
{
    public class doctorController : Controller
    {
         MenuLinqClass menuObj = new MenuLinqClass();
         LinqDataContext db = new LinqDataContext();

        public ActionResult Autocomplete(string term)
        {
            var model = db.doctors
                .Where(x => x.dr_name.StartsWith(term)).Take(10).Select(x => new
                {
                    label = x.dr_name
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public doctorController()
         {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }
        
      
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = db.doctors.Where(x => searchTerm == null || x.dr_name.StartsWith(searchTerm)).Select(x => x).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("SearchResults", model);
            }


            return View(model);
           
        }
        
     /*   public ActionResult SearchResults(string searchTerm = null)
        {
            
        }*/


    }
}
