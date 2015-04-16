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


            //triageObj.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
            triageObj.ERdoctors = triageObj.getERdoctors();
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

            // get all current er cases

            //model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();
            triageObj.ERpatients = triageObj.getERPatients().ToList();

            // get doctors assigned to ER from doctors table

            triageObj.ERdoctors = triageObj.getERdoctors();

            // docStats contains an associative array ("doc_id" => "wait time for this dr_id") 
            // used to select minimum wait time and dr_id associated with it

            Dictionary<int, TimeSpan> docStats = new Dictionary<int, TimeSpan>();

            // docOutput contains associative array ("doctor name" => "wait for this doctor")
            // used to display doctor list and their respective wait times in the header of the page

            Dictionary<string, TimeSpan> docOutput = new Dictionary<string, TimeSpan>();
            numDoctors = triageObj.ERdoctors.Count();
            ViewBag.numDoctors = numDoctors;

            foreach (doctor doc in triageObj.ERdoctors)
            {
                docStats.Add(doc.dr_id, triageObj.getWaitTimes(doc.dr_id));
                docOutput.Add(doc.dr_name, triageObj.getWaitTimes(doc.dr_id));
            }

            ViewBag.docOutput = docOutput;

            // now selecting minimum waiting time for displaying in the ER Monitor

            System.TimeSpan minWaitTime = docStats.OrderBy(x => x.Value).First().Value;

            return minWaitTime;


        }

        //--CRUD operations in the controller are performed in a single window to create a 
        // more user friendly experience for the stressed ER personnel with least possible buttons to click
        // and most operations are done in the code without user input
        // Single page CRUD original tutorial was found here but has been modified almost beyond recognition
        //--http://www.bipinjoshi.net/articles/4a00a9ce-73e5-4d89-aaae-2d835eca0854.aspx

        [OutputCache(NoStore = true, Duration = 3)] // every 3 sec
        public ActionResult Index()
        {
            //using (LinqDataContext db = new LinqDataContext())
            //{
            TriageViewModel model = new TriageViewModel();

            model.ERpatients = model.getERPatients().ToList();

            model.SelectedERpatient = null;

            ViewBag.TotalWait = CalcWaitTime();

            return View(model);
            //}
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
                model.ERpatients = model.getERPatients().ToList();

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

                emergency_level selectedEmLevel = model.getEmergencyById(obj.em_id);

                //--arrival time is registered automatically based on timezone of the hospital location (Utc - 4hrs)

                DateTime arrival = DateTime.UtcNow.AddHours(-4);
                obj.arrival = arrival;

                //--doctor id is assigned automatically based on calculation in the TriageViewModel
                // of the doctor with shortes wait time

                obj.dr_id = model.getShortQueu().Key;

                //discharge time is estimated based on the time of last patient discharge time added to the time recorded in the 
                //emergency_levels table for the selected type of emergency

                var estDischarge = Convert.ToDouble(selectedEmLevel.em_duration_hrs) + model.getShortQueu().Value.TotalHours;
                obj.discharge = arrival.AddHours(estDischarge);

                //--field patient_name is filled with the short description of emergency rather than actual patient name which is far
                //more informative for our case

                obj.patient_name = selectedEmLevel.em_description;

                //--insert is performed against ViewModel

                model.commitInsert(obj);


                //--after insert patient list is formed again for display

                model.ERpatients = model.getERPatients().ToList();

                model.SelectedERpatient = model.getERPatientById(obj.case_id);

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
                model.ERpatients = model.getERPatients().ToList();

                model.SelectedERpatient = model.getERPatientById(id);

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
                model.ERpatients = model.getERPatients().ToList();

                model.SelectedERpatient = model.getERPatientById(id);

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
            //--Custome server side validation to make sure that updated discharge time is not earliear than previous 
            //--patients. I cannot change discharge date for patient # 2 for a particular doctor if patient # 1 is being discherged later

            using (LinqDataContext db = new LinqDataContext())
            {
                var prevPatients = db.triages.Where(x => x.case_id < case_id
                                                    && x.dr_id == obj.dr_id
                                                    && x.discharge > obj.discharge).FirstOrDefault();
                if (prevPatients != null)
                {
                    // if previous patients with later discharge time than validation fails, and new error is added to the model

                    ModelState.AddModelError("ErrorDischarge", "There are previous patients with later discharge date");
                }
            }

            if (ModelState.IsValid)
            {
                using (LinqDataContext db = new LinqDataContext())
                {
                    var pat_upd = db.triages.Single(x => x.case_id == case_id);
                    int dr_id = pat_upd.dr_id;
                    var dischargeTimeDiff = new TimeSpan(0);

                    //checking if discharge time was actually changed and saving difference
                    // between original and updated value in dischargeTimeDiff variable
                    // it will be used later to update subsequent cases discharge time
                    // "*" is added to case type showing that discharge time has been modified

                    if (pat_upd.discharge != Convert.ToDateTime(obj.discharge))
                    {
                        dischargeTimeDiff = Convert.ToDateTime(obj.discharge).Subtract(pat_upd.discharge);
                        pat_upd.patient_name = obj.patient_name + "*";
                    }
                    else
                    {
                        pat_upd.patient_name = obj.patient_name;
                    }
                    pat_upd.arrival = Convert.ToDateTime(obj.arrival);
                    pat_upd.discharge = Convert.ToDateTime(obj.discharge);
                    db.SubmitChanges();

                    TriageViewModel model = new TriageViewModel();

                    var subseqPatients = db.triages.Where(x => x.case_id > case_id && x.dr_id == dr_id).Select(x => x);
                    foreach (triage patient in subseqPatients)
                    {
                        patient.discharge = patient.discharge.AddHours(dischargeTimeDiff.TotalHours);
                        db.SubmitChanges();
                    }

                    // reload patient list
                    model.ERpatients = model.getERPatients().ToList();

                    model.SelectedERpatient = pat_upd;
                    model.DisplayMode = "Read";

                    ViewBag.TotalWait = CalcWaitTime();

                    return View("Index", model);
                }
            }
            else
            {

                // if validation failed, the form remaines displayed with error messages
                using (LinqDataContext db = new LinqDataContext())
                {
                    TriageViewModel model = new TriageViewModel();
                    // reload patient list
                    model.ERpatients = model.getERPatients().ToList();

                    var pat_upd = db.triages.Single(x => x.case_id == case_id);

                    model.SelectedERpatient = pat_upd;
                    model.DisplayMode = "ReadWrite";

                    ViewBag.TotalWait = CalcWaitTime();
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
                var pat_delete = db.triages.Single(x => x.case_id == id);
                int dr_id = pat_delete.dr_id;
                var dischargeTimeDiff = new TimeSpan(0);

                //checking if discharge time is later than current time
                // if not, deleting this case will not affect subsequent cases
                // if yes, difference between current time and estimated discharge time is
                // stored in variable dischargeTimeDiff
                // it will be used later to update subsequent cases discharge time


                if (pat_delete.discharge > DateTime.UtcNow.AddHours(-4))
                {
                    dischargeTimeDiff = DateTime.UtcNow.AddHours(-4).Subtract(pat_delete.discharge);
                }
                db.triages.DeleteOnSubmit(pat_delete);
                db.SubmitChanges();

                var subseqPatients = db.triages.Where(x => x.case_id > id && x.dr_id == dr_id).Select(x => x);
                foreach (triage patient in subseqPatients)
                {
                    patient.discharge = patient.discharge.AddHours(dischargeTimeDiff.TotalHours);
                    db.SubmitChanges();
                }

                TriageViewModel model = new TriageViewModel();
                // reload patient list
                model.ERpatients = model.getERPatients().ToList();
                model.SelectedERpatient = null;

                model.DisplayMode = "";

                ViewBag.TotalWait = CalcWaitTime();

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

                //reload list of patients
                model.ERpatients = model.getERPatients().ToList();
                model.SelectedERpatient = db.triages.Single(x => x.case_id == id);

                model.DisplayMode = "Read";

                ViewBag.TotalWait = CalcWaitTime();

                return View("Index", model);
            }
        }

        // this action creats a partial view of admin-side monitor
        // OutputCacheAttribute tells browser to NOT use total wait time cached value
        [OutputCache(NoStore = true, Duration = 0)] // every 3 sec
        public ActionResult AdminMonitor()
        {
            ViewBag.TotalWait = CalcWaitTime();
            return PartialView("AdminMonitor");
        }



    }
}