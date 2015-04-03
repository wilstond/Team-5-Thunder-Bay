using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class ERAdminController : Controller
    {

        static int numDoctors;
        MenuLinqClass menuObj = new MenuLinqClass();
        TriageViewModel triageObj = new TriageViewModel();
        LinqDataContext db = new LinqDataContext();
       

        public ERAdminController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

            ViewBag.emergencyList = triageObj.getEmergencyList();

            triageObj.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
            Dictionary<int, string> docList = new Dictionary<int, string>();

            foreach (doctor doc in triageObj.ERdoctors)
            {
                docList.Add(doc.dr_id, doc.dr_name);
            }

            ViewBag.docList = docList;
        }


        public TimeSpan CalcWaitTime()
        { 
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();
                model.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
                Dictionary<int, TimeSpan> docStats = new Dictionary<int, TimeSpan>();
                Dictionary<string, TimeSpan> docOutput = new Dictionary<string, TimeSpan>();
                numDoctors = model.ERdoctors.Count();

                foreach (doctor doc in model.ERdoctors)
                {
                    docStats.Add(doc.dr_id, model.getWaitTimes(doc.dr_id));
                    docOutput.Add(doc.dr_name, model.getWaitTimes(doc.dr_id));
                }

                ViewBag.docOutput = docOutput;

                System.TimeSpan minWaitTime = docStats.OrderBy(x => x.Value).First().Value;

                return minWaitTime;
            }

        }


        public ActionResult Index()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();

                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                //model.ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
                //Dictionary<int, string> docList = new Dictionary<int, string>();

                //foreach (doctor doc in model.ERdoctors)
                //{
                //    docList.Add(doc.dr_id, doc.dr_name);
                //}
                model.SelectedERpatient = null;

                //ViewBag.docList = docList;
                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View(model);
            }
        }

        public ActionResult Select()
        {
            ViewBag.TotalWait = CalcWaitTime();
            ViewBag.numDoctors = numDoctors;

            return View();
        }

        [HttpGet]
        public ActionResult New(string whatever)
        {
            return RedirectToAction("Index");
        }

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
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }

        [HttpPost]
        public ActionResult Insert(triage obj)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();

                emergency_level selectedEmLevel = db.emergency_levels.Single(e => e.em_id == obj.em_id);
                DateTime arrival = DateTime.Now;
                obj.arrival = arrival.ToLocalTime();
                obj.discharge = arrival.AddHours(Convert.ToDouble(selectedEmLevel.em_duration_hrs)).ToLocalTime();
                obj.patient_name = selectedEmLevel.em_description;
                obj.dr_id = model.getShortQueu();

                db.triages.InsertOnSubmit(obj);
                db.SubmitChanges();

                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();
               

                model.SelectedERpatient = db.triages.Single(x => x.case_id == obj.case_id);
                model.DisplayMode = "Read";

                ViewBag.TotalWait = CalcWaitTime();
                ViewBag.numDoctors = numDoctors;

                return View("Index", model);
            }
        }

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

        [HttpPost]
        public ActionResult Update(int case_id, triage obj)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                var pat_upd = db.triages.Single(x => x.case_id == case_id);
                pat_upd.patient_name = obj.patient_name;
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



    }
}