using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Areas.admin.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class PageController : Controller
    {
        //creating new object

        PageLinqClass objPage = new PageLinqClass();
        
        //
        // GET: /Admin/Page/

        public ActionResult Index()
        {
            var AllPages = objPage.getPages();
            return View(AllPages);
        }

        //
        // GET: /Admin/Page/Details/5

        public ActionResult Details(int id)
        {
            var selectPage = objPage.getPageByID(id);
            if (selectPage == null)
            {
                return View("NotFound");
                // if Page with passed id parameter doesn't exist - return NotFound();

            }
            else
            {
                //Display Details of the Page
                return View(selectPage);
            }
        }

        //
        // GET: /Admin/Page/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Page/Create

        [HttpPost]
        public ActionResult Create(Page page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objPage.commitInsert(page); // insert is committed and user is redirected to home page
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(); //if error occurs, user remains on insert page and no insert is commited
                }
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Admin/Page/Edit/5

        public ActionResult Edit(int id)
        {
            var selPage = objPage.getPageByID(id); //variable is created to store selected Page
            if (selPage == null)
            {
                return NotFound(); //if Page is not found

            }
            else
            {
                return View(selPage); //displays update form for selected Page
            }
        }

        //
        // POST: /Admin/Page/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Page page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //update is commited based on updated values and user redirected back to Details page 
                    objPage.commitUpdate(id, page.page_title, page.user_id, page.page_content, page.page_created, page.menu_id, page.page_visibility, page.page_slug);
                    return RedirectToAction("Details/" + id);
                }
                catch
                {
                    return View(); //if error occurs no action is performed and user remains on the page
                }
            }

            return View();

        }

        //
        // GET: /Admin/Page/Delete/5

        public ActionResult Delete(int id)
        {
            var selPage = objPage.getPageByID(id);
            if (selPage == null)
            {
                return View("NotFound"); //if page is not found
            }
            else
            {
                return View(selPage); //confirmation to delete page for selected Page
            }
        }

        //
        // POST: /Admin/Page/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Page page)
        {
            try
            {
                //page deleted and user redirected to index
                objPage.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();//if error occurs - no delete action is commited
            }
        }

        //Not found page
        public ActionResult NotFound()
        {
            return View();
        }

    }
}
