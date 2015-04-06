using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;
using ThunderB_redesign.Filters;


using ThunderB_redesign.Models;
using System.IO;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    [Authorize]
    public class PageAdminController : Controller
    {
        //creating new object
        
        PageLinqClass objPage = new PageLinqClass();
        MenuLinqClass menuObj = new MenuLinqClass();

        public PageAdminController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
            ViewBag.menuTree = menuObj.getMenuTree();
            ViewBag.user_id = Membership.GetUser().ProviderUserKey;
            ViewBag.breadCrumbs = menuObj.getBreadcrumbList();

        }

        // GET: admin/PageAdmin
        public ActionResult Index()
        {
            var AllPages = objPage.getPages();
            return View(AllPages);
        }

        //--addition to allow ckeditor pload local images; followed tutorial below
        //---http://amitraya.blogspot.ca/2014/09/ck-editor-implement-your-own-image.html
        public void uploadnow(HttpPostedFileWrapper upload)
        {
            if(upload!=null)
            {
                string ImageName = upload.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/uploads"), ImageName);
                upload.SaveAs(path);
            }
        }
 
        //--addition to allow ckeditor pload local images; followed tutorial below
        //---http://amitraya.blogspot.ca/2014/09/ck-editor-implement-your-own-image.html
        public ActionResult uploadPartial()
        {
            var appData = Server.MapPath("~/Images/uploads");
            var images = Directory.GetFiles(appData).Select(x => new imagesviewmodel
            {
                Url = Url.Content("/images/uploads/" + Path.GetFileName(x))
            });
            return View(images);
        }

        // GET: admin/PageAdmin/Details/5
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

        // GET: admin/PageAdmin/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: admin/PageAdmin/Create
        [HttpPost]
        public ActionResult Create(page page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objPage.commitInsert(page); // insert is committed and user is redirected to home page
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //Content(ex.Message);
                    return View(ex.Message); //if error occurs, user remains on insert page and no insert is commited
                }
            }
            else
            {
                return View();
            }
        }

        // GET: admin/PageAdmin/Edit/5
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

        // POST: admin/PageAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, page page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //update is commited based on updated values and user redirected back to Details page 
                    objPage.commitUpdate(id, page.page_title, page.user_id, page.page_content, page.page_created,

page.menu_id, page.page_visibility, page.page_slug);
                    return RedirectToAction("Details/" + id);
                }
                catch
                {
                    return View(); //if error occurs no action is performed and user remains on the page
                }
            }

            return View();
        }

        // GET: admin/PageAdmin/Delete/5
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

        // POST: admin/PageAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

        //Check if Slug is available - it needs to be unique
        //based on the example found here:
        //http://stackoverflow.com/questions/18037292/client-side-validation-for-unique-field-mvc

        [HttpPost]
        public JsonResult IsSlugAvailable(string page_slug) {

            var slug = objPage.getSlug(page_slug);

            return Json(slug == null);
        }
    }
}
