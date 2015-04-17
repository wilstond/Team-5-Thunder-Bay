using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Models
{
    public class GiftShopVM
    {
        LinqDataContext objDataContext = new LinqDataContext();

        //Category Table fields
        
        [DisplayName("Category ID")]
        public int cat_id { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Please enter the category name")]
        public string cat_name { get; set; }

        //Product Table fields

        [DisplayName("Product ID")]
        public int prd_id { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter the product name")]
        public string prd_name { get; set; }


        [DisplayName("Product Description")]
        [Required(ErrorMessage = "Please enter the product description")]
        public string prd_description { get; set; }


        [DisplayName("Price")]
        [Required(ErrorMessage = "Please enter the price")]
        public decimal prd_price { get; set; }


        [DisplayName("Count")]
        [Required(ErrorMessage = "Please enter the count")]
        public int prd_count { get; set; }

        [DisplayName("Category ID")]
        [Required(ErrorMessage = "Please select a category")]
        public int prd_cat_id { get; set; }


        [DisplayName("Product Image URL")]
        public string prd_img_url { get; set; }


        public IEnumerable<product> getAllProducts()
        {
            var allProducts = objDataContext.products.Select(x => x);
            return allProducts;
        }

        public product getProductByID(int id)
        {
            var prodDetails = objDataContext.products.SingleOrDefault(x => x.prd_id == id);
            return prodDetails;
        }

        public IEnumerable<product> getProductsByCategoryID(int id)
        {
            var prodsByCat = objDataContext.products.Where(x => x.prd_cat_id == id);
            return prodsByCat;
        }

        public IEnumerable<product_category> getCategories()
        {
            var allCategories = objDataContext.product_categories.Select(x => x);
            return allCategories;
        }

        public product_category getCategoryDetailsByID(int id)
        {
            var categoryDetails = objDataContext.product_categories.SingleOrDefault(x => x.prd_cat_id == id);
            return categoryDetails;
        }

        
        public List<GiftShopVM> getAllProductsWithDetails()
        {
            var allProducts = getAllProducts();
            List<GiftShopVM> allProductsWithDetails = new List<GiftShopVM>();

            foreach (var prod in allProducts)
            {
                var categoryInfo = getCategoryDetailsByID(prod.prd_cat_id);
                GiftShopVM completeProductInfo = new GiftShopVM();
                completeProductInfo.prd_id = prod.prd_id;
                completeProductInfo.prd_name = prod.prd_name;
                completeProductInfo.prd_description = prod.prd_description;
                completeProductInfo.prd_price = prod.prd_price;
                completeProductInfo.prd_count = prod.prd_count;
                completeProductInfo.prd_img_url = prod.prd_img_url;
                completeProductInfo.prd_cat_id = prod.prd_cat_id;
                completeProductInfo.cat_id = categoryInfo.prd_cat_id;
                completeProductInfo.cat_name = categoryInfo.prd_cat_name;

                allProductsWithDetails.Add(completeProductInfo);

            }

            return allProductsWithDetails;

        }

        public bool addNewProduct()
        {
            product prod = new product();
            //prod.prd_id = prd_id;
            prod.prd_name = prd_name;
            prod.prd_description = prd_description;
            prod.prd_count = prd_count;
            prod.prd_img_url = prd_img_url;
            prod.prd_cat_id = prd_cat_id;
            commitInsertProduct(prod);
            return true;
        }

        public bool addNewCategory(product_category prd_cat)
        {
            product_category cat = new product_category();
            cat.prd_cat_name = prd_cat.prd_cat_name;
            commitInsertCategory(cat);
            return true;
        }

        public bool commitInsertProduct(product prod)
        {
            objDataContext = new LinqDataContext();

            using (objDataContext)
            {
                objDataContext.products.InsertOnSubmit(prod);
                objDataContext.SubmitChanges();
                return true;
            }
        }

        public bool commitInsertCategory(product_category cat)
        {
            objDataContext = new LinqDataContext();

            using (objDataContext)
            {
                objDataContext.product_categories.InsertOnSubmit(cat);
                objDataContext.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdateProduct(int _id, product objProduct)
        {
            using (objDataContext)
            {
                var productUpd = objDataContext.products.SingleOrDefault(x => x.prd_id == _id);
                productUpd.prd_name = objProduct.prd_name;
                productUpd.prd_price = objProduct.prd_price;
                productUpd.prd_description = objProduct.prd_description;
                productUpd.prd_count = objProduct.prd_count;
                productUpd.prd_img_url = objProduct.prd_img_url;
                productUpd.prd_cat_id = objProduct.prd_cat_id;
                objDataContext.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdateCategory(int _id, product_category objCategory)
        {
            using (objDataContext)
            {
                var categoryUpd = objDataContext.product_categories.SingleOrDefault(x => x.prd_cat_id == _id);
                categoryUpd.prd_cat_name = objCategory.prd_cat_name;
                objDataContext.SubmitChanges();
                return true;
            }
        }

        public bool commitDeleteProduct(int _id)
        {
            using (objDataContext)
            {
                var prodDel = objDataContext.products.Single(x => x.prd_id == _id);
                objDataContext.products.DeleteOnSubmit(prodDel);
                objDataContext.SubmitChanges();
                return true;
            }
        }

    }
}