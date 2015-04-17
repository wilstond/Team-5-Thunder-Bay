using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class DoctorAdminController : Controller
    {

        LinqDataContext db = new LinqDataContext();
        doctorLinqClass objDoc = new doctorLinqClass();

        //getting list of deparment and inserting into View bag -- Irina's code
        public DoctorAdminController()
        {
            List<department> objDepartments = new List<department>();
            IQueryable<department> allDepartments  = db.departments.Select(x => x);
            foreach (department dept in allDepartments)
            {
                objDepartments.Add(dept);
            }
            ViewBag.objDepartments = objDepartments;
        }

        // GET: admin/DoctorAdmin
        public ActionResult Index()
        {   //al doctors
            var allDoctors = objDoc.GetDoctors();
            return View(allDoctors);
        }

        // GET: admin/DoctorAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/DoctorAdmin/Create
        [HttpPost]
        public ActionResult Create(doctor doc)
        {
            if (ModelState.IsValid) //if form is valid then insert concert instance
            {
                try
                {
                    objDoc.commitInsert(doc);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: admin/DoctorAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            var doc = objDoc.getDoctorByID(id);
            if (doc == null)
            {
                return View("NotFound");
            }
            return View(doc);
        }

        // POST: admin/DoctorAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, doctor doc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objDoc.commitUpdate(id, doc.dr_name, doc.dr_specialty, doc.dr_office_name, doc.dr_office_address, doc.dr_office_phone, doc.user_id, doc.dept_id); //updating row with the new fields from the form 

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: admin/DoctorAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var doc = objDoc.getDoctorByID(id);
            if (doc == null)
            {
                return View("NotFound");
            }
            return View();
        }

        // POST: admin/DoctorAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, doctor doc)
        {
            try
            {
                objDoc.commitDelete(id); //deleting instance

                return RedirectToAction("Index"); //and returning to index view
            }
            catch
            {
                return View();
            }
        }
    }
}
