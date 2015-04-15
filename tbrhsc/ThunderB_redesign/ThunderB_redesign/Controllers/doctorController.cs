using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Controllers
{
    public class doctorController : Controller
    {
        // GET: doctor
        public ActionResult Index()
        {
            return View();
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
