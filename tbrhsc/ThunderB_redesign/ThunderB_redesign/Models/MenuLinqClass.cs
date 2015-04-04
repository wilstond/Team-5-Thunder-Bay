using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class MenuLinqClass
    {
        LinqDataContext menuObj = new LinqDataContext();

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

        public List<menu_category> getMenuTree()
        {
            var allMenuItems = menuObj.menu_categories.Where(x => x.parent_id == 0 && x.menu_id != 0);
            List<menu_category> menuTree = new List<menu_category>();
            foreach (menu_category menuItem in allMenuItems)
            {
                menuTree.Add(menuItem);
                var subMenu = menuObj.menu_categories.Where(x => x.parent_id == menuItem.menu_id && x.menu_slug == "Page");

                //List<menu_category> subMenu = getSubMenuItemsByParentId(menuItem.menu_id).ToList();
                foreach (menu_category submenuItem in subMenu)
                {
                    menuTree.Add(submenuItem);
                }
            }
            return menuTree;
        }
    }
}