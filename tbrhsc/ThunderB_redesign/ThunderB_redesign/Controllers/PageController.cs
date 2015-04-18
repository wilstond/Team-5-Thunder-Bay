using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class PageController : Controller
    {
        PageLinqClass objPage = new PageLinqClass();




        MenuLinqClass menuObj = new MenuLinqClass();

        public PageController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }

            ViewBag.breadCrumbs = menuObj.getBreadcrumbList();

        }



        public ActionResult _PageList(int menu_id = 0)
        {
            var AllPages = objPage.getVisiblePages();
            if (menu_id != 0)
            {
                AllPages = objPage.getPagesByMenu(menu_id);
            }
            return View(AllPages);
        }


        public ActionResult Index(int menu_id = 0)
        {
            if (menu_id == 0)
            {
                // Redirect the user to the main page
                return RedirectToAction("Index", "Home");
            }

            // get pages that belong to the current menu category

            var AllPages = objPage.getPagesByMenu(menu_id);

            // if there are any pages in the category, first one is displayed as a landing page for the category
            if (AllPages.Any())
            {
                var LandPage = AllPages.First();

                // if meta-title is emty, using page title as a meta title

                if (LandPage.meta_title == null)
                {
                    ViewBag.LandPageTitle = LandPage.page_title;
                }
                else
                {
                    ViewBag.LandPageTitle = LandPage.meta_title;
                }

                // meta-description for the landing page

                if (LandPage.meta_desc == null)
                {
                    ViewBag.LandPageDescription = "";
                }
                else
                {
                    ViewBag.LandPageDescription = LandPage.meta_desc;
                }

                // breadcrumb with links to all parent categories

                ViewBag.breadCrumb = objPage.getBreadcrumb(LandPage.menu_id);

                // list of sibling pages

                ViewBag.ListOfPages = AllPages.ToList();

                // list of subcategories of the current category if exist

                ViewBag.subMenuItems = menuObj.getSubMenuItemsByParentId(menu_id).ToList();

                return View(LandPage);
            }

            return View("PageNotFound");

        }


        public ActionResult Detail(string page_slug)
        {
            // select page based on a slug

            var selectPage = objPage.getPageBySlug(page_slug);

            if (selectPage == null)
            {
                // Redirect the user to the main page if page is not found
                return RedirectToAction("Index", "Home");
            }

            // find sibling pages, children of the same menu

            var relatedPages = objPage.getPagesByMenu(selectPage.menu_id);
            ViewBag.relatedPages = relatedPages.ToList();


            int user_id = selectPage.user_id;
            int page_id = selectPage.page_id;
            var pageById = objPage.getPageByID(page_id);

            // get meta information of the page

            if (pageById.meta_title == null)
            {
                ViewBag.LandPageTitle = pageById.page_title;
            }
            else
            {
                ViewBag.LandPageTitle = pageById.meta_title;
            }
            if (pageById.meta_desc == null)
            {
                ViewBag.LandPageDescription = "";
            }
            else
            {
                ViewBag.LandPageDescription = pageById.meta_desc;
            }

            // get breadcrumb for the page

            ViewBag.breadCrumb = objPage.getBreadcrumb(pageById.menu_id);

            return View(pageById);
            //return View(selectPage);

        }

        public ActionResult PageNotFound()
        {
            return View();
        }



    }
}
