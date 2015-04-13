using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class VolunteerAdminController : Controller
    {

        MenuLinqClass menuObj = new MenuLinqClass();
        VolunteerViewModel VolObj = new VolunteerViewModel();
        LinqDataContext db = new LinqDataContext();

        public VolunteerAdminController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

           // ViewBag.emergencyList = triageObj.getEmergencyList();

          VolObj.volList = db.Volunteers.Select(x => x);
            Dictionary<int, string> VolunteerList = new Dictionary<int, string>();

            foreach (Volunteer vol in VolObj.volList)
            {
                VolunteerList.Add(vol.volunteer_id,vol.name);
            }

            ViewBag.VolunteerList = VolunteerList;
        }


        // GET: admin/VolunteerFeedback
        public ActionResult Index()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
               VolunteerViewModel model = new VolunteerViewModel();

                model.shifList = db.Shifts.OrderBy(x => x.volunteer_id).ToList();

                model.SelectedVolunteer = null;

                return View(model);
            }
        }       
    }
}