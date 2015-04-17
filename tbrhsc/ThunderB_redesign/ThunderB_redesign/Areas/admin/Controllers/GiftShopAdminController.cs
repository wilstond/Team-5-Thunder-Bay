using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderB_redesign.Models;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Areas.admin.Controllers
{
    [Authorize]
    public class GiftShopAdminController : Controller
    {

        public GiftShopAdminController()
        {
            //Values populated for the dropdown list of categories
            GiftShopVM objGiftShop = new GiftShopVM();
            var allCategories = objGiftShop.getCategories();
            ViewData["categories"] = allCategories;
        }

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
        public ActionResult Create(product prod, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                GiftShopVM objGiftShopVM = new GiftShopVM();

                string pic;
                string path;

                if (file != null && file.ContentLength > 0)
                {
                    //Source for file upload: stackoverflow
                    pic = System.IO.Path.GetFileName(file.FileName);
                    path = System.IO.Path.Combine(
                                           Server.MapPath("~/Images/GiftShop"), pic);
                    // file is uploaded
                    file.SaveAs(path);

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    
                    //Assign Image name
                    prod.prd_img_url = file.FileName;
                }
                else
                {
                    prod.prd_img_url = "Not-Available.jpg";
                }
                
                objGiftShopVM.commitInsertProduct(prod);

                return RedirectToAction("Index");
            }
            return View();
        }

        //View to display details of products
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

        public ActionResult Delete(int id)
        {
            GiftShopVM objGiftShopVM = new GiftShopVM();
            var prod = objGiftShopVM.getProductByID(id);
            if (prod == null)
            {
                return View("NotFound");
            }

            return View(prod);
        }

        [HttpPost]
        public ActionResult Delete(int id, product prod)
        {
            try
            {
                GiftShopVM objGiftShop = new GiftShopVM();
                objGiftShop.commitDeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}