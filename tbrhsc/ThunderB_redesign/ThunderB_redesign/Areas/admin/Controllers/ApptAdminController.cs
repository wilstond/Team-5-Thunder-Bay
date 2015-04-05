using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;
using ThunderB_redesign.Filters;


using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    [Authorize]
    public class ApptAdminController : Controller
    {
        LinqDataContext db = new LinqDataContext();

        AppointmentLinqClass apptObject = new AppointmentLinqClass();
        static int user_id;
        static int dr_id;

        public ApptAdminController()
        {
            user_id = Convert.ToInt16(Membership.GetUser().ProviderUserKey);
            
                db.DeferredLoadingEnabled = false;
                dr_id = db.doctors.Where(x => x.user_id == user_id).SingleOrDefault().dr_id;
            
            ViewBag.dr_id = dr_id;
        }

        // GET: admin/ApptAdmin
        //List all appointments by doctor Id
        public ActionResult Index()
        {
                var appts = apptObject.getAppointmentsbyDr(dr_id);
                return View(appts);
        }

        // GET: admin/ApptAdmin/Details/5
        public ActionResult Details(int id)
        {
            var selAppt = apptObject.getAppointmentById(id);
            return View(selAppt);
        }

        // GET: admin/ApptAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/ApptAdmin/Create
        [HttpPost]
        public ActionResult Create(appointment appt)
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

        // GET: admin/ApptAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            var selAppt = apptObject.getAppointmentById(id);
            return View(selAppt);
        }

        // POST: admin/ApptAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, appointment appt)
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

        // GET: admin/ApptAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/ApptAdmin/Delete/5
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
