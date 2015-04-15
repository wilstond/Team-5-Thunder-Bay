using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class DoctorAdminController : Controller
    {
        // GET: admin/DoctorAdmin
        public ActionResult Index()
        {
            return View();
        }

        // GET: admin/DoctorAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/DoctorAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/DoctorAdmin/Create
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

        // GET: admin/DoctorAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/DoctorAdmin/Edit/5
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

        // GET: admin/DoctorAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/DoctorAdmin/Delete/5
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
