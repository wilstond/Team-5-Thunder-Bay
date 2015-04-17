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
            
            //X Axis values
            List<string> xValuesPie = new List<string>();
            List<string> xValuesLine = new List<string>();
            var xValuesArr = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "Ocotober", "November", "December" };
            //Adding to List<string>
            foreach(var mon in xValuesArr) 
            {
                xValuesPie.Add(mon);
                xValuesLine.Add(mon);
            }

            //Y Axis values
            var amounts = new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            //Calculating donations for each month
            foreach (var dtn in allDonations)
            {
                amounts[dtn.dtn_date.Month - 1] = amounts[dtn.dtn_date.Month - 1] + (double)dtn.dtn_amount;
            }
            //Adding to a list
            List<string> yValuesPie = new List<string>();
            List<string> yValuesLine = new List<string>();
            foreach (var amt in amounts)
            {
                yValuesPie.Add(amt.ToString());
                yValuesLine.Add(amt.ToString());
            }


            //Add to viewdata for line graph
            ViewData["xValuesLine"] = xValuesLine;
            ViewData["yValuesLine"] = yValuesLine;
            
            //Remove months with no donations in order to render a proper pie chart
            for (int i = 11; i >= 0; i--)
            {
                if (yValuesPie.ElementAt(i).Equals("0"))
                {
                    xValuesPie.RemoveAt(i);
                    yValuesPie.RemoveAt(i);
                }
            }

            //Add to viewdata for pie chart
            ViewData["xValuesPie"] = xValuesPie;
            ViewData["yValuesPie"] = yValuesPie;
        }

        // GET: admin/DonationAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonthlyDonationAnalytics()
        {
            return View();
        }

        public ActionResult List()
        {
            var objDonor = new DonorClass();
            var allDonors = objDonor.getDonors();
            return View(allDonors);
        }

        public ActionResult Update(int id)
        {
            var objDonor = new DonorClass();
            var donorDetails = objDonor.getDonorById(id);
            if (donorDetails == null)
            {
                return View("NotFound");
            }
            return View(donorDetails);
        }

        [HttpPost]
        public ActionResult Update(int dnr_id, donor donorUpd)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var objDonor = new DonorClass();
                    objDonor.commitUpdate(dnr_id, donorUpd);
                    return RedirectToAction("Details", new { id = dnr_id});
                }
                catch
                {
                    return View();
                }
            }

            return View();

        }

        public ActionResult Details(int id)
        {
            var objDonor = new DonorClass();
            var donorDetails = objDonor.getDonorById(id);
            if (donorDetails == null)
            {
                return View("NotFound");
            }
            return View(donorDetails);
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult DonationGraphPie()
        {
            var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                            .AddTitle("2015 Monthly Comparison")
                            .AddSeries(chartType: "Doughnut",
                            xValue: (IEnumerable<string>)@ViewData["xValuesPie"],
                            yValues: (IEnumerable<string>)@ViewData["yValuesPie"])
                            .GetBytes("jpeg");

            return File(myChart, "image/jpeg");
        }

        public ActionResult DonationGraphLine()
        {
            var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                            .AddTitle("Monthly Donation Graph")
                            .AddSeries(chartType: "Column",
                            xValue: (IEnumerable<string>)@ViewData["xValuesLine"],
                            yValues: (IEnumerable<string>)@ViewData["yValuesLine"])
                            .GetBytes("jpeg");

            return File(myChart, "image/jpeg");
        }

    }
}