using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class FaqController : Controller
    {
        MenuLinqClass menuObj = new MenuLinqClass();

        FaqClass objFaq = new FaqClass(); 


        public FaqController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }

        public ActionResult Index()
        {
            var faq = objFaq.GetFaqsByCategory();
            return View(faq);
        }

        public PartialViewResult Patient()
        {
            string patient ="patient";
            var faq = objFaq.GetFaqsByCategory(patient);
            return PartialView("_category",faq);
        }

        public PartialViewResult Visitor()
        {
            string visitor = "visitor";
            var faq = objFaq.GetFaqsByCategory(visitor);
            return PartialView("_category", faq);
        }

        public PartialViewResult HealthProfessional()
        {
            string healthpro = "health professional";
            var faq = objFaq.GetFaqsByCategory(healthpro);
            return PartialView("_category", faq);
        }


        public ActionResult _categories()
        {

            var faq = objFaq.GetFaqs().Select(x => x.category);
            ViewBag.Categories = new SelectList(faq, "Category","Category","General");
            return PartialView();
        }


    }
}