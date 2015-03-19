using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace ThunderB_redesign.Areas.admin.Models
{
    public class PageLinqClass
    {
        //establish connection to database table
        LinqDataContext objPage = new LinqDataContext(); //see line 26 in linq.designer.cs

        //returns list of all Pages from Pages table
        public IQueryable<Page> getPages()
        {
            var AllPages = objPage.Pages.Select(x => x);

            return AllPages;
        }


        public Page getPageByID(int _id)
        {
            var selPage = objPage.Pages.SingleOrDefault(x => x.page_id == _id);
            //select only row with page_id = _id
            return selPage;
        }

        public bool commitInsert(Page page)
        {
            using (objPage)
            {
                objPage.Pages.InsertOnSubmit(page);
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
                var pageUpd = objPage.Pages.Single(x => x.page_id == _page_id);
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
                var pageDel = objPage.Pages.Single(x => x.page_id == _page_id);
                objPage.Pages.DeleteOnSubmit(pageDel);

                objPage.SubmitChanges();
                return true;
            }
        }


    }
}