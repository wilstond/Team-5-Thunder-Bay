using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Postal;


using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class AppointmentController : Controller
    {
        LinqDataContext db = new LinqDataContext();
        AppointmentLinqClass apptObject = new AppointmentLinqClass();
        MenuLinqClass menuObj = new MenuLinqClass();


        public AppointmentController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

            List<doctor> docList = new List<doctor>();
            IQueryable<doctor> AllDoctors = db.doctors.Select(x => x);

            foreach (doctor doc in AllDoctors)
            {
                docList.Add(doc);
            }

            ViewBag.docList = docList;

        }

        // GET: Appointment/Form
        // Hidden fields for the form are populated in the controller
        public ActionResult Index()
        {
            return View();
        }

        // POST: Appointment/Form
        // This function inserts patient's information in the database and at the same time sends
        // patient a confirmation email . Email sending is using a Nuget package Postal.Mvc5
        // found using this tutorial --http://aboutcode.net/postal/--
        [HttpPost]
        public ActionResult Index(appointment _apt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    apptObject.commitInsert(_apt); // insert is committed and user is redirected to home page
                    var last_id = _apt.apt_id;
                    
                    // sending an email after request is entered in the database

                    dynamic email = new Email("Request_Confirmation");
                    email.Doctor = db.doctors.Where(x => x.dr_id == _apt.dr_id).SingleOrDefault().dr_name.ToString();
                    email.Patient = _apt.pat_name.ToString();
                    email.To = _apt.pat_email.ToString();
                    email.Phone = _apt.pat_phone.ToString();
                    email.FollowUpDate = _apt.date_req.AddDays(7).ToShortDateString().ToString();

                    email.Send();

                    //  values in the viewbag are displayed in the details page

                    ViewBag.Doctor = email.Doctor;
                    ViewBag.Patient = email.Patient;
                    ViewBag.Phone = email.Phone;
                    ViewBag.To = email.To;
                    ViewBag.FollowUpDate = email.FollowUpDate;

                    return View("Details", _apt);
                }
                catch (Exception ex)
                {
                    //Content(ex.Message);
                    return View(ex.Message); //if error occurs, user remains on insert page and no insert is commited
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details()
        {
            return View();
        }

        
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
