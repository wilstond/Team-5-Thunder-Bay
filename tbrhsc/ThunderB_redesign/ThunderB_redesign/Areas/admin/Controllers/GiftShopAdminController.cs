using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Areas.admin.Controllers
{
    public class GiftShopAdminController : Controller
    {
        // GET: admin/GiftShopAdmin
        public ActionResult Index()
        {
            GiftShopVM objGiftShopVM = new GiftShopVM();
            var allProducts = objGiftShopVM.getAllProductsWithDetails();
            return View(allProducts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(product prod)
        {
            if (ModelState.IsValid)
            {
                GiftShopVM objGiftShopVM = new GiftShopVM();
                objGiftShopVM.commitInsertProduct(prod);
                return View("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            GiftShopVM objGiftShopVM = new GiftShopVM();
            var productDetails = objGiftShopVM.getProductByID(id);
            if (productDetails == null)
            {
                return View("NotFound");
            }
            return View(productDetails);
        }

        public ActionResult Update(int id)
        {
            GiftShopVM objGiftShopVM = new GiftShopVM();
            var prodDetails = objGiftShopVM.getProductByID(id);
            if (prodDetails == null)
            {
                return View("Not found");
            }
            return View(prodDetails);
        }

        [HttpPost]
        public ActionResult Update(int prd_id, product prod)
        {
            if (ModelState.IsValid)
            {
                GiftShopVM objGiftShopVM = new GiftShopVM();
                objGiftShopVM.commitUpdateProduct(prd_id, prod);
                return RedirectToAction("Details/" + prd_id);
            }
            return View();
        }

    }
}