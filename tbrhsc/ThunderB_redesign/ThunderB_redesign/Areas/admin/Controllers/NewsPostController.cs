using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    [Authorize]

    public class NewsPostController : Controller
    {
        // GET: admin/NewsPost
        NewsPostAdminModel objNews = new NewsPostAdminModel();
        public ActionResult Index()
        {
            //grab everthing from the news table
            var news = objNews.getNews();
            return View();
        }
    }
}