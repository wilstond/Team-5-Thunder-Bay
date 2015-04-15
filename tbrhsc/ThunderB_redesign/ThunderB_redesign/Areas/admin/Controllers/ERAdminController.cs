using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    //this controller is only access by users with Admin role assigned to them
    [Authorize(Roles = "Admin")]
    public class ERAdminController : Controller
    {
        //---variable to hold number of doctors in the ER
        static int numDoctors;

        MenuLinqClass menuObj = new MenuLinqClass();
        TriageViewModel triageObj = new TriageViewModel();
        LinqDataContext db = new LinqDataContext();


        public ERAdminController()
        {

            // get a list of emergencies from the database

            ViewBag.emergencyList = triageObj.getEmergencyList();

            triageObj.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
            Dictionary<int, string> docList = new Dictionary<int, string>();

            foreach (doctor doc in triageObj.ERdoctors)
            {
                docList.Add(doc.dr_id, doc.dr_name);
            }

            ViewBag.docList = docList;
        }


        // function to calculate waiting time based on number of the doctors assigned to ER (doctors table)
        // number and recorded time of existing ER cases and types of emergencies

        public TimeSpan CalcWaitTime()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();

                // get all current er cases

                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                // get doctors assigned to ER from doctors table

                model.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);

                // docStats contains an associative array ("doc_id" => "wait time for this dr_id") 
                // used to select minimum wait time and dr_id associated with it

                Dictionary<int, TimeSpan> docStats = new Dictionary<int, TimeSpan>();

                // docOutput contains associative array ("doctor name" => "wait for this doctor")
                // used to display doctor list and their respective wait times in the header of the page

                Dictionary<string, TimeSpan> docOutput = new Dictionary<string, TimeSpan>();
                numDoctors = model.ERdoctors.Count();
                ViewBag.numDoctors = numDoctors;

                foreach (doctor doc in model.ERdoctors)
                {
                    docStats.Add(doc.dr_id, model.getWaitTimes(doc.dr_id));
                    docOutput.Add(doc.dr_name, model.getWaitTimes(doc.dr_id));
                }

                ViewBag.docOutput = docOutput;

                // now selecting minimum waiting time for displaying in the ER Monitor

                System.TimeSpan minWaitTime = docStats.OrderBy(x => x.Value).First().Value;

                return minWaitTime;
            }

        }

        //--CRUD operations in the controller are performed in a single window to create a 
        // more user friendly experience for the stressed ER personnel with least possible buttons to click
        // and most operations are done in the code without user input
        // Single page CRUD original tutorial was found here but has been modified almost beyond recognition
        //--http://www.bipinjoshi.net/articles/4a00a9ce-73e5-4d89-aaae-2d835eca0854.aspx

        [OutputCache(NoStore = true, Duration = 3)] // every 3 sec
        public ActionResult Index()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();

                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = null;

                ViewBag.TotalWait = CalcWaitTime();

                return View(model);
            }
        }

        //--prevents typing /admin/ERAdmin/Select 

        [HttpGet]
        public ActionResult Select(string whatever)
        {

            return RedirectToAction("Index");

        }

        //--prevents typing /admin/ERAdmin/New 

        [HttpGet]
        public ActionResult New(string whatever)
        {
            return RedirectToAction("Index");
        }

        //--function to display form to insert new case/patient into database

        [HttpPost]
        public ActionResult New()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = null;
                model.DisplayMode = "Write";

                ViewBag.TotalWait = CalcWaitTime();
                //  ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }

        //prevents attempt to commit insert without model parameter

        [HttpGet]
        public ActionResult Insert(string whatever)
        {

            return RedirectToAction("Index");

        }

        //function to insert new case /patient into database

        [HttpPost]
        public ActionResult Insert(triage obj)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();

                //--pull up emergency description from emergency levels table to match with id selected  by user

                emergency_level selectedEmLevel = db.emergency_levels.Single(e => e.em_id == obj.em_id);

                //--arrival time is registered automatically based on timezone of the hospital location (Utc - 4hrs)

                DateTime arrival = DateTime.UtcNow.AddHours(-4);
                obj.arrival = arrival;

                //discharge time is estimated based on the time of arrival added to the time recorded in the 
                //emergency_levels table for the selected type of emergency

                obj.discharge = arrival.AddHours(Convert.ToDouble(selectedEmLevel.em_duration_hrs));

                //--field patient_name is filled with the short description of emergency rather than actual patient name which is far
                //more informative for our case

                obj.patient_name = selectedEmLevel.em_description;

                //--doctor id is assigned automatically based on calculation in the TriageViewModel
                // of the doctor with shortes wait time

                obj.dr_id = model.getShortQueu();

                db.triages.InsertOnSubmit(obj);
                db.SubmitChanges();

                //--after insert patient list is formed again for display

                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = db.triages.Single(x => x.case_id == obj.case_id);
                model.DisplayMode = "Read";

                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }

        //--select action highlights selected field and displays it below the list
        // in read mode

        [HttpPost]
        public ActionResult Select(int id)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = db.triages.Single(x => x.case_id == id);
                model.DisplayMode = "Read";

                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }


        //--prevents typing /admin/ERAdmin/Edit 

        [HttpGet]
        public ActionResult Edit(string whatever)
        {
            return RedirectToAction("Index");
        }

        //Edit action displays selected (and now highlighted) record in read/wite mode
        //the only field that permits editing is discharge time
        // Discharge time does not allow to enter past time 
        // If the discharge occured in the past - the case is closed, and should be discharged 
        // from ER queue altogether


        [HttpPost]
        public ActionResult Edit(int id)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = db.triages.Single(x => x.case_id == id);
                model.DisplayMode = "ReadWrite";

                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }


        //--prevents typing /admin/ERAdmin/Update 

        [HttpGet]
        public ActionResult Update(string whatever)
        {
            return RedirectToAction("Index");
        }

        //--if edited discharge time meets validation criteria an update is performed

        [HttpPost]
        public ActionResult Update(int case_id, triage obj)
        {
            if (ModelState.IsValid)
            {
                using (LinqDataContext db = new LinqDataContext())
                {
                    var pat_upd = db.triages.Single(x => x.case_id == case_id);
                    if (pat_upd.discharge != Convert.ToDateTime(obj.discharge))
                    {
                        pat_upd.patient_name = obj.patient_name + " (modified)";
                    }
                    else
                    {
                        pat_upd.patient_name = obj.patient_name;
                    }
                    pat_upd.arrival = Convert.ToDateTime(obj.arrival);
                    pat_upd.discharge = Convert.ToDateTime(obj.discharge);
                    db.SubmitChanges();

                    TriageViewModel model = new TriageViewModel();
                    model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                    model.SelectedERpatient = pat_upd;
                    model.DisplayMode = "Read";

                    ViewBag.TotalWait = CalcWaitTime();
                    ViewBag.numDoctors = numDoctors;

                    return View("Index", model);
                }
            }
            else
            {
                using (LinqDataContext db = new LinqDataContext())
                {
                    TriageViewModel model = new TriageViewModel();
                    model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();
                    var pat_upd = db.triages.Single(x => x.case_id == case_id);

                    model.SelectedERpatient = pat_upd;
                    model.DisplayMode = "ReadWrite";

                    ViewBag.TotalWait = CalcWaitTime();
                    ViewBag.numDoctors = numDoctors;
                    return View("Index", model);
                }
            }
        }


        //--prevents typing /admin/ERAdmin/Delete 

        [HttpGet]
        public ActionResult Delete(string whatever)
        {
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                triage pat_delete = db.triages.Single(x => x.case_id == id);
                db.triages.DeleteOnSubmit(pat_delete);
                db.SubmitChanges();

                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = null;

                model.DisplayMode = "";

                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }


        //--prevents typing /admin/ERAdmin/Cancel 

        [HttpGet]
        public ActionResult Cancel(string whatever)
        {
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult Cancel(int id)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();
                model.SelectedERpatient = db.triages.Single(x => x.case_id == id);

                model.DisplayMode = "Read";

                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }


        [OutputCache(NoStore = true, Duration = 3)] // every 3 sec
        public ActionResult AdminMonitor()
        {
            ViewBag.TotalWait = CalcWaitTime();
            return PartialView("AdminMonitor");
        }



    }
}