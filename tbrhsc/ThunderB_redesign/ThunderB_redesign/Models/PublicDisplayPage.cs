using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Models
{
    public class PublicDisplayPage
    {
        LinqDataContext objPage = new LinqDataContext(); //see line 26 in linq.designer.cs

        //returns list of all visible Pages from Pages table


        public IQueryable<page> getVisiblePages()
        {
            var AllPages = from x in objPage.pages where x.page_visibility.ToString() == "Y" select x;

            return AllPages;
        }

        //returns list of all Pages from selected menu_id
        public IQueryable<page> getPagesByMenu(int _menu_id = 1)
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
    }
}