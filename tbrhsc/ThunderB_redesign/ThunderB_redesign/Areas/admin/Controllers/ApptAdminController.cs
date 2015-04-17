using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Postal;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ThunderB_redesign.Filters;


using ThunderB_redesign.Models;
using System.IO;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    //  this controller is restricted to Admin or Doctor users
    [Authorize(Roles="Doctor, Admin")]
    [InitializeSimpleMembership]
    public class ApptAdminController : Controller
    {
        LinqDataContext db = new LinqDataContext();

        AppointmentLinqClass apptObject = new AppointmentLinqClass();
        static int user_id;
        static int dr_id;

        

        // GET: admin/ApptAdmin
        //List all appointments by doctor Id
        public ActionResult Index()
        {
            // pulling logged-in user_id from WebSecurity object

            user_id = WebSecurity.GetUserId(User.Identity.Name);

            // selecting  a doctor with logged-in user id

            var selDoctor = apptObject.getDoctorByUserId(user_id);

            // if no doctor has this user_id we return NotFound view, saying that the user is likely not a doctor and therefore
            // can not view doctor's appointments

            if (selDoctor == null)
            {
                return View("NotFound");
            }
            else
            {
                // selecting appointments by doctor id

                dr_id = selDoctor.dr_id;
                ViewBag.dr_id = dr_id;
                var appts = apptObject.getAppointmentsbyDr(dr_id);


                return View(appts);
            }

        }

        public ActionResult NotFound()
        {
            return View();
        }

        // GET: admin/ApptAdmin/Details/5
        public ActionResult Details(int id)
        {
            var selAppt = apptObject.getAppointmentById(id);
            return View(selAppt);
        }



        // GET: admin/ApptAdmin/Edit/id
        // select appointment by id and pass it to the Edit view
        public ActionResult Edit(int id)
        {
            var selAppt = apptObject.getAppointmentById(id);

            return View(selAppt);
        }

        // POST: admin/ApptAdmin/Edit/id
        // update selected appointment booking date and time
        [HttpPost]
        public ActionResult Edit(int id, appointment appt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (appt.date_book != null)
                    {
                        //appt.time_book = appt.date_book.ToString();
                        appt.app_status = "Booked"; // if booking date has been set - appointment status is changed to Booked instead of Pending
                    }

                    // Update is committed

                        apptObject.commitUpdate(id, appt.dr_id, appt.date_req, appt.date_book, appt.time_book, appt.pat_name,
                        appt.pat_phone, appt.pat_email, appt.app_status);

                    // After appointment is booked patient gets an email notification
                    // Email function is using templates found at the Views/Emails folder. Booking_Confirmation.cshtml


                        dynamic email = new Email("Booking_Confirmation");
                        email.Doctor = db.doctors.Where(x => x.dr_id == appt.dr_id).SingleOrDefault().dr_name.ToString();
                        email.Patient = appt.pat_name.ToString();
                        email.To = appt.pat_email.ToString();
                        email.Phone = appt.pat_phone.ToString();
                        email.BookDate = Convert.ToDateTime(appt.date_book).ToShortDateString();
                        email.BookTime = Convert.ToDateTime(appt.date_book).ToShortTimeString();
                        email.CancelDate = Convert.ToDateTime(appt.date_book).AddDays(-2).ToShortDateString();

                        email.Send();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            return View(appt);
        }

        // GET: admin/ApptAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var delAppt = apptObject.getAppointmentById(id);

            return View(delAppt);
        }

        // POST: admin/ApptAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, appointment apt)
        {
            try
            {
                // delete appointment based on appointment id

                var delAppt = apptObject.getAppointmentById(id);

                apptObject.commitDelete(id);

                // send an email with cancellation notice 
                // Email sending is using a Nuget package Postal.Mvc5
                // I found it using this tutorial --http://aboutcode.net/postal/--

                dynamic email = new Email("Cancel_appointment");

                // Email function is using templates found at the Views/Emails folder
                // this particular email is using Cancel_appointment.cshtml template

                email.Doctor = db.doctors.Where(x => x.dr_id == delAppt.dr_id).SingleOrDefault().dr_name.ToString();
                email.Patient = delAppt.pat_name.ToString();
                email.To = delAppt.pat_email.ToString();
                email.BookDate = Convert.ToDateTime(delAppt.date_book).ToShortDateString();
                email.BookTime = Convert.ToDateTime(delAppt.date_book).ToShortTimeString();

                email.Send();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
