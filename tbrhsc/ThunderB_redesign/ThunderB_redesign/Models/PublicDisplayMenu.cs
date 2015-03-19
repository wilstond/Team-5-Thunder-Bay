using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ThunderB_redesign.Areas.admin.Models;

namespace ThunderB_redesign.Models
{
    public class PublicDisplayMenu
    {
        LinqDataContext objPage = new LinqDataContext(); //see line 26 in linq.designer.cs

        public IQueryable<menu_category> getMenuItems()
        {
            var allMenuItems = from x in objPage.menu_categories select x;

            return allMenuItems;
        }

        public menu_category getMenuById(int _menu_id)
        {
            var selMenu = objPage.menu_categories.SingleOrDefault(x => x.menu_id == _menu_id);
            return selMenu;
        }
    }
}