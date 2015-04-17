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
        {
            var allDoctors = objDoc.GetDoctors();
            return View(allDoctors);
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
        public ActionResult Create(doctor doc)
        {

            HttpPostedFileBase image = Request.Files["file"];

            var fileName = Path.GetFileName(image.FileName);
        

                if (ModelState.IsValid && image != null && image.ContentLength > 0)
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
