//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//using ThunderB_redesign.Models;

//namespace ThunderB_redesign.Areas.admin.Controllers
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