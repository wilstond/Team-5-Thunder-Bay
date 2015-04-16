using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Controllers
{
    public class GiftShopController : Controller
    {
        MenuLinqClass menuObj = new MenuLinqClass();

        public GiftShopController()
        {
            ViewData["MenuItems"] = menuObj.getMenuItems();

            var menuItems = (IEnumerable<ThunderB_redesign.Models.menu_category>)ViewData["MenuItems"];

            foreach (var menuItem in menuItems)
            {
                ViewData["SubMenuItems for " + menuItem.menu_id.ToString()] = menuObj.getSubMenuItemsByParentId(menuItem.menu_id);
            }
        }

        // GET: GiftShop
        public ActionResult Index()
        {
            GiftShopVM objGiftShopVM = new GiftShopVM();
            var allProducts = objGiftShopVM.getAllProductsWithDetails();
            return View(allProducts);
        }

        public ActionResult Details(int id)
        {
            GiftShopVM objGiftShopVM = new GiftShopVM();
            var productDetails = objGiftShopVM.getProductByID(id);
            return View(productDetails);
        }

    }
}