using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class DonationAdminController : Controller
    {

        public DonationAdminController()
        {
            //Code to generate monthly graph
            var objDonationVM = new DonationVM();
            var allDonations = objDonationVM.getAllDonationDetails();
            var xValues = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "Ocotober", "November", "December" };
            var amounts = new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            foreach (var dtn in allDonations)
            {
                amounts[dtn.dtn_date.Month - 1] = amounts[dtn.dtn_date.Month - 1] + (double)dtn.dtn_amount;
            }

            List<string> yValues = new List<string>();

            foreach (var amt in amounts)
            {
                yValues.Add(amt.ToString());
            }

            ViewData["xValues"] = xValues;
            ViewData["yValues"] = yValues;
        }

        // GET: admin/DonationAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonationGraph()
        {
            var myChart = new Chart(width: 600, height: 400)
                            .AddTitle("Monthy Report")
                            .AddSeries(chartType: "Pie",
                            xValue: (IEnumerable<string>)@ViewData["xValues"],
                            yValues: (IEnumerable<string>)@ViewData["yValues"])
                            .GetBytes("jpeg");

            return File(myChart, "image/jpeg");
        }

    }
}