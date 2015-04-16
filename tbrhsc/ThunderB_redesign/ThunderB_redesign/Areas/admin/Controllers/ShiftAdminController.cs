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

            public ActionResult Edit(int Id)
            {

                var editvolunteer = objShift.getShiftbyID(Id);
                if (editvolunteer == null)
                {
                    return View("Error");
                }
                return View(editvolunteer);
            }

            // POST: /Update
            [HttpPost]
            public ActionResult Edit(int Id, volshift shift)
            {
                //If all the input were valid , then database will be updated
                if (ModelState.IsValid)
                {
                    
                    try
                    {
                       
                        objShift.commitUpdate(Id,shift.name, shift.email,shift.shiftday);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }
                return View();
            }

            // GET: /Delete
            public ActionResult Delete(int Id)
            {

                //It shows Delete view according to the selected id
                var voldel = objShift.getShiftbyID(Id);
                if (voldel == null)
                {
                    return View("Error");
                }
                return View(voldel);
            }

            [HttpPost]
            public ActionResult Delete(int Id,volshift shift)
            {
                //Selected value will be deleted from the database
                try
                {
                    objShift.commitDelete(Id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            public ActionResult Insert()
            {

                return View();
            }
            [HttpPost]
            public ActionResult Insert(volshift shift)
            {
                if (ModelState.IsValid)
                {

                    objShift.commitInsert(shift);
                    return RedirectToAction("Thanks");

                    //return View();

                }

                return View();
            }
        public ActionResult Thanks()
            {
                return View();
            }

        }

      
    }
