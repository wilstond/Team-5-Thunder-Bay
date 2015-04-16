using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class ShiftAdminController : Controller
    {
       
            ShiftLinqClass objShift = new ShiftLinqClass();
            public ActionResult Index()
            {
                var shift = objShift.getShift();
                return View(shift);
            }

            public ActionResult Details(int id)
            {
                var feedback = objShift.getShiftbyID(id);
                if (feedback == null)
                {
                    return View("NotFound");
                }
                else
                {
                    return View(feedback);
                }
            }

            public ActionResult Delete(int id)
            {
                try
                {
                    objShift.commitDelete(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
                //var feedback = objFeedback.getFeedbackbyID(id);
                //if (feedback == null)
                //{
                //    return View("NotFound");
                //}
                //else
                //{
                //    return View(feedback);
                //}
            }

            [HttpPost]
            public ActionResult Delete(int id, feedback feedback)
            {
                try
                {
                    objShift.commitDelete(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }


        }

      
    }
