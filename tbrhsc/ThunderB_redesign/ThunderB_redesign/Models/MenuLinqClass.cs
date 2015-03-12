using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class MenuLinqClass
    {
        MenuLinqDataContext menuObj = new MenuLinqDataContext();

        public IEnumerable<menu_category> getMenuItems()
        {
            var allMenuItems = menuObj.menu_categories.Select(x => x);
            return allMenuItems;
        }

    }
}