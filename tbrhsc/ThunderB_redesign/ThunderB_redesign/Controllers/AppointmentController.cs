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
        public ActionResult Form()
        {
            appointment newApt = new appointment();
            newApt.date_req = DateTime.Now;

            newApt.date_book = newApt.date_req.AddDays(-1);

            newApt.time_book = "00:00:00 AM";

            newApt.app_status = "Pending";
            return View(newApt);
        }

        // POST: Appointment/Form
        [HttpPost]
        public ActionResult Form(appointment _apt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    apptObject.commitInsert(_apt); // insert is committed and user is redirected to home page
                    var last_id = _apt.apt_id;
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

            dynamic email = new Email("Request_Confirmation");
            email.To = "ilecoche@acn.net";

            email.Send();
            return View();
        }

        public ActionResult SendEmail()
        {
            dynamic email = new Email("Request_Confirmation");
            email.To = "ilecoche@acn.net";
            
            email.Send();
            return View();
        }

       
       
       
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
