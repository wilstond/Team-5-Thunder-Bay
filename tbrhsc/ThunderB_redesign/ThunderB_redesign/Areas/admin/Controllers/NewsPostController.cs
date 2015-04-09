using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Models.NewsPostLinq
{
    public class NewsPostLinq : Controller
    {
        newsClass objNews = new newsClass();

        public ActionResult Index()
        {
            var allNews = objNews.getNews();
            return View(allNews);
        }

        public ActionResult Details(int id)
        {
            var allNews = objNews.getNewsByID(id);
            if (allNews == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(allNews);
            }
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(newsTable allnews)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.commitInsert(allnews);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult Update(int id)
        {
            var allNews = objNews.getNewsByID(id);
            if (allNews == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(allNews);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, newsTable allNews)
        {
            try
            {
                objNews.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }

    
}







//{
//    [Authorize]

//    public class NewsPostController : Controller
//    {
//        //creating objects that link to the news table in database
//        FeedbackLinqClass objNewsDB = new FeedbackLinqClass();
//        MenuLinqClass menuobj = new MenuLinqClass();

//        public NewsPostController()
//        {
            
//        }





//        /// ///////////////////////////////////////////////////
  
//        public FeedbackController()
//        {
//            ViewData["MenuItems"] = menuObj.getMenuItems();

//            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

//            foreach (var menuItem in menuItems)
//            {
//                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
//            }

//        }





//        ////////////////////////////////////////////////////////
//        // GET: admin/NewsPost
//        NewsPostAdminModel objNews = new NewsPostAdminModel();
//        public ActionResult Index()
//        {
//            //grab everthing from the news table
//            var news = objNews.getNews();
//            return View();
//        }
//    }
//}