using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

using ThunderB_redesign.Controllers;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class NewsPostController : Controller
    {
        
        //The link to the Database as an object
        NewsPostLinq objNews = new NewsPostLinq();

  
        //What happens when Index is called, fetches all news posts.
        public ActionResult Index()
        {
            var allNews = objNews.orderNews();
            return View(allNews);
        }


        //when Insert (creating a new News Post) is hit, the user is redirected to the original index page which lists all the news posts.
        public ActionResult Insert()
        {
            return View();
        }

        //when the insert (publish) button is posted, the information is committed to the database and also sent to the newsletter functionality
        [HttpPost]
        public ActionResult Insert(newsTable allnews)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.commitInsert(allnews);
                    //Code to send news to subscribers
                    NewsletterController.SendNewsAlerts("news", allnews.headline + "<br/><br/>" + allnews.stories);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }


        //returning to the index page of the News Post when update button is pressed
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

        //committing all fields into the database to update the selected News Post
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




