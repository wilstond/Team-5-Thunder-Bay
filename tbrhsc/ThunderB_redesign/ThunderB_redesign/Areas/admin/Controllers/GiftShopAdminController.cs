using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Create(product prod, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                GiftShopVM objGiftShopVM = new GiftShopVM();
                
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
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
                prod.prd_img_url = file.FileName;
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

        public ActionResult ImageUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
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

            }
            // after successfully uploading redirect the user
            return View();
        }

    }
}