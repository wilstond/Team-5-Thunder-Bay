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

        
        // GET: doctor
        public ActionResult Index(string searchTerm = null)
        {
            var model = db.doctors.Where(x => searchTerm == null || x.dr_name.Contains(searchTerm)).Select(x => x);
            return View(model);
        }

        // GET: doctor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: doctor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: doctor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
