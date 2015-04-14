using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class NewsPostController : Controller
    {
        
        //The link to the Database as an object
        ////////////////////////////////////////
        NewsPostLinq objNews = new NewsPostLinq();

  
        //What happens when Index is called, fetches all news posts.
        //////////////////////////////////////// ///////////////////
        public ActionResult Index()
        {
            var allNews = objNews.getNews();
            return View(allNews);
        }


        // A possible details view functionality
        ////////////////////////////////////////

        //public ActionResult Details(int id)
        //{
        //    var allNews = objNews.getNewsByID(id);
        //    if (allNews == null)
        //    {
        //        return View("NotFound");
        //    }
        //    else
        //    {
        //        return View(allNews);
        //    }
        //}


        //The Insert functionality 
        ////////////////////////////////////////
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


        //Update functionality. Updates all fields when update is posted.
        ////////////////////////////////////////////////////////////////
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
        public ActionResult Update(int Id, newsTable news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.commitUpdate(Id, news.stories, news.headline, news.date, news.author);
                }
                catch
                {
                    return View();
                }

                return RedirectToAction("Index", new { id = Id });

            }
            
            return View(news);

        }


        //Delete functionality. Deletes by ID
        ////////////////////////////////////////
        public ActionResult Delete(int id)
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




