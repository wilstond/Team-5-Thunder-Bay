using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ThunderB_redesign.Models
{
    public class PageLinqClass
    {
        LinqDataContext objPage = new LinqDataContext(); //see line 26 in linq.designer.cs

        //returns list of all visible Pages from Pages table


        public IQueryable<page> getVisiblePages()
        {
            var AllPages = from x in objPage.pages where x.page_visibility.ToString() == "Y" select x;

            return AllPages;
        }

        //returns list of all Pages from selected menu_id
        public IQueryable<page> getPagesByMenu(int _menu_id = 0)
        {
            var AllPages = from x in objPage.pages where (x.menu_id == _menu_id && x.page_visibility.ToString() == "Y") select x;

            return AllPages;
        }



        public page getPageByID(int _id)
        {
            var selPage = objPage.pages.SingleOrDefault(x => x.page_id == _id);
            //select only row with page_id = _id
            return selPage;
        }

        public page getPageBySlug(string _slug)
        {
            var selPage = objPage.pages.SingleOrDefault(x => x.page_slug == _slug);
            //select only row with page_slug = _slug
            return selPage;
        }

        //returns list of all Pages from Pages table
        public IQueryable<page> getPages()
        {
            var AllPages = objPage.pages.Select(x => x);

            return AllPages;
        }

        public string getSlug(string _slug)
        {
            var sel_page = objPage.pages.SingleOrDefault(x => x.page_slug == _slug);
            string slug = sel_page.page_slug;

            return slug;
        }


        public bool commitInsert(page _page)
        {
            using (objPage)
            {

                objPage.pages.InsertOnSubmit(_page);
                //inserts one row 
                objPage.SubmitChanges();
                //executes insert
                return true;
            }
        }

        public bool commitUpdate(int _page_id, string _page_title, int _user_id, string _page_content, DateTime _page_created, int _menu_id, char _page_visibility, string _page_slug)
        {
            using (objPage)
            {
                var pageUpd = objPage.pages.Single(x => x.page_id == _page_id);
                //Single = Select where

                pageUpd.page_title = _page_title;
                pageUpd.user_id = _user_id;
                pageUpd.page_content = _page_content;
                pageUpd.page_created = _page_created;
                pageUpd.menu_id = _menu_id;
                pageUpd.page_visibility = _page_visibility;
                pageUpd.page_slug = _page_slug;

                objPage.SubmitChanges();
                //executes update
                return true;

            }
        }

        public bool commitDelete(int _page_id)
        {
            using (objPage)
            {
                var pageDel = objPage.pages.Single(x => x.page_id == _page_id);
                objPage.pages.DeleteOnSubmit(pageDel);

                objPage.SubmitChanges();
                return true;
            }
        }
    }
}