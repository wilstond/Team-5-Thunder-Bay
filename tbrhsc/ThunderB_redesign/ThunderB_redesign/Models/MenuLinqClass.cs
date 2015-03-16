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
            var allMenuItems = menuObj.menu_categories.Where(x => x.parent_id == 0);
            return allMenuItems;
        }

        public IEnumerable<menu_category> getSubMenuItemsByParentId(int parentId)
        {
            var subMenuItems = menuObj.menu_categories.Where(x => x.parent_id == parentId);
            return subMenuItems;
        }

    }
}