﻿using System;
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
            
               // db.DeferredLoadingEnabled = false;
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

        // GET: admin/ApptAdmin/Edit/id
        // select appointment by id and pass it to the Edit view
        public ActionResult Edit(int id)
        {
            var selAppt = apptObject.getAppointmentById(id);
            if (selAppt.date_book == DateTime.MinValue.ToLocalTime())
            {
                ViewData["date_book"] = "";
            }
            else
            {
                ViewData["date_book"] = selAppt.date_book.ToLocalTime();
            }
            return View(selAppt);
        }

        // POST: admin/ApptAdmin/Edit/id
        // update selected appointment booking date and time
        [HttpPost]
        public ActionResult Edit(int id, appointment appt)
        {
            if(ModelState.IsValid){
            try
                {
                    if (appt.date_book != DateTime.MinValue.ToLocalTime())
                    {
                        appt.time_book = appt.date_book.ToShortTimeString();
                    }
                    apptObject.commitUpdate(id, appt.dr_id, appt.date_req, appt.date_book, appt.time_book, appt.pat_name,
                        appt.pat_phone, appt.pat_email, appt.pat_address, appt.pat_ohip, appt.fam_dr_name, appt.fan_dr_phone, appt.app_status);

                    return RedirectToAction("Index");
                }
            catch(Exception ex)
                {
                    return View(ex.Message);
                }
            }
            return View(appt);
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
