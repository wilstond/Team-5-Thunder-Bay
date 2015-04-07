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
            var allMenuItems = menuObj.menu_categories.Where(x => x.parent_id == 0 && x.menu_id < 6);
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
                var subMenu = menuObj.menu_categories.
                    Where(x => x.parent_id == menuItem.menu_id && x.menu_slug == "Page");

                //List<menu_category> subMenu = getSubMenuItemsByParentId(menuItem.menu_id).ToList();
                foreach (menu_category submenuItem in subMenu)
                {
                    menuTree.Add(submenuItem);
                }
            }
            return menuTree;
        }

        public Dictionary<int, string> getBreadcrumbList()
        {
            Dictionary<int, string> menuBreadcrumbList = new Dictionary<int, string>();
            var query = (from m in menuObj.menu_categories
                    join p in menuObj.menu_categories on m.parent_id equals p.menu_id
            select new 
                {
                    menuId = m.menu_id,
                    menuText = m.menu_text,
                    parentId = p.menu_id,
                    shortBreadcrumb = " <a href='/Page/Index?menu_id=" + m.menu_id + "'>" + m.menu_text + "</a> ",
                    fullBreadcrumb = p.menu_text + " > <a href='/Page/Index?menu_id=" + m.menu_id + "'>" + m.menu_text + "</a> "
                }).ToList();

            foreach (var row in query)
            {
                if (row.parentId == 0)
                {
                    menuBreadcrumbList.Add(row.menuId, row.shortBreadcrumb);
                }
                else
                {
                    menuBreadcrumbList.Add(row.menuId, row.fullBreadcrumb);
                }
            }
            return menuBreadcrumbList;

        }
    }
}