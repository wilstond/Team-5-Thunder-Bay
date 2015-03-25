using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class AdminFaqController : Controller
    {
        FaqClass objFaq = new FaqClass(); 


        public ActionResult Index()
        {
            var faq = objFaq.GetFaqs();
            return View(faq);
        }


        // GET: /Admin/Create

        public ActionResult Create() //returns the view to insert  new 
        {
            return View();
        }

        // 
        // POST: /Admin/Create

        [HttpPost] //filter this action so that it only does post requests
        public ActionResult Create(faq faq) //takes an instance of the Faq class 
        {
            if (ModelState.IsValid) //if form is valid then insert concert instance
            {
                try
                {
                    objFaq.commitInsert(faq);
                    return RedirectToAction("Index"); //after insert return to the index view
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //
        // GET: /Admin/Edit/

        public ActionResult Edit(int id)
        {
            var faq = objFaq.getFaqByID(id); 
            if (faq == null)
            {
                return View("NotFound");
            }
            return View(faq);
        }

        //
        // POST: /Admin/Edit/

        [HttpPost]
        public ActionResult Edit(int id, faq faq) //edit method that takes id and Faq class as parameters
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objFaq.commitUpdate(id, faq.question, faq.answer, faq.category); //updating row with the new fields from the form 

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var faq = objFaq.getFaqByID(id); //selecting a row of the table based on the id
            if (faq == null)
            {
                return View("NotFound");
            }
            return View(faq);
        }

        [HttpPost]
        public ActionResult Delete(int id, faq faq)
        {
            try
            {
                objFaq.commitDelete(id); //deleting instance

                return RedirectToAction("Index"); //and returning to index view
            }
            catch
            {
                return View();
            }
        }

    }
}