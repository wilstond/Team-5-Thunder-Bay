using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class ERController : Controller
    {
        Int16 numDoctors = 2;
        MenuLinqClass menuObj = new MenuLinqClass();

        public ERController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

        }


        public TimeSpan CalcWaitTime()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                TimeSpan TotalWait = new TimeSpan();
                foreach (triage item in model.ERpatients)
                {
                    System.TimeSpan diff = item.discharge.Subtract(item.arrival);
                    TotalWait += diff;

                }

                var resWaitTime = new TimeSpan(TotalWait.Ticks / numDoctors);
                return resWaitTime;
            }

        }

        public ActionResult Index()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                TriageViewModel model = new TriageViewModel();
                model.ERpatients = db.triages.OrderBy(x => x.case_id).ToList();

                model.SelectedERpatient = null;

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
                db.triages.InsertOnSubmit(obj);
                db.SubmitChanges();

                TriageViewModel model = new TriageViewModel();
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