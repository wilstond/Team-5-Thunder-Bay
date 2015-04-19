using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ThunderB_redesign.Models;
using Postal;

using System.Diagnostics;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Controllers
{
    
    public class DonationController : Controller
    {
        MenuLinqClass menuObj = new MenuLinqClass();
        
        public DonationController()
        {
            //Add menu items to view data

            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DonationVM objDonationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Add donation to database
                    objDonationVM.addDonationInfo();

                    

                    //Proceed to paypal for payment
                    proceedToPaypal(objDonationVM.dtn_amount);

                    //Send thank you email
                    dynamic email = new Email("DonationThankYou");
                    email.name = objDonationVM.dnr_name;
                    email.to = objDonationVM.dnr_email;
                    email.Send();
                
                    //Display thank you message
                    //return RedirectToAction("Success", objDonationVM);

                }
                catch
                {
                    return View("Index");
                }
            }

            return View();

        }

        public ActionResult Success()
        {
            return View();
        }
        
        //Method to process info and redirect to paypal

        private void proceedToPaypal(System.Nullable<decimal> amount)
        {
            string url = "";

            string business = "thunderbayhospital@gmail.com";  // your paypal email
            string description = "Donation towards Thunder Bay Hospital"; // '%20' represents a space. remember HTML!
            string country = "CA";                  // AU, US, etc.
            string currency = "CAD";                 // AUD, USD, etc.

            url += "https://www.sandbox.paypal.com/cgi-bin/webscr" +
                "?cmd=" + "_donations" +
                "&business=" + business +
                "&lc=" + country +
                "&item_name=" + description +
                "&amount=" + amount +
                "&currency_code=" + currency +
                "&bn=" + "PP%2dDonationsBF";

            //Process.Start(url);

            Response.Redirect(@url);
            
        }

    }

}