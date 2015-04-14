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

        //----function to get menu categories for populating drop-down list of menu categories
        public List<menu_category> getMenuTree()
        {
            var allMenuItems = menuObj.menu_categories.Where(x => x.parent_id == 0 && x.menu_id != 0);
            List<menu_category> menuTree = new List<menu_category>();
            foreach (menu_category menuItem in allMenuItems)
            {
                menuTree.Add(menuItem);
                var subMenu = menuObj.menu_categories.
                    Where(x => x.parent_id == menuItem.menu_id && x.menu_slug == "Page");

                foreach (menu_category submenuItem in subMenu)
                {
                    menuTree.Add(submenuItem);
                }
            }
            return menuTree;
        }

        //------function to create a breadcrumb navigation for dynamically created pages
        public Dictionary<int, string> getBreadcrumbList()
        {
            Dictionary<int, string> menuBreadcrumbList = new Dictionary<int, string>();
            
            //---query to select menu categories together with their parents
            var query = (from m in menuObj.menu_categories
                    join p in menuObj.menu_categories on m.parent_id equals p.menu_id
            select new 
                {
                    menuId = m.menu_id,
                    menuText = m.menu_text,
                    parentId = p.menu_id,
                    shortBreadcrumb = " <a href='/Page/Index?menu_id=" + m.menu_id + "'>" + m.menu_text + "</a> ",
                    fullBreadcrumb = " <a href='/Page/Index?menu_id=" + p.menu_id + "'>" + p.menu_text + "</a> > <a href='/Page/Index?menu_id=" + m.menu_id + "'>" + m.menu_text + "</a> "
                }).ToList();

            foreach (var row in query)
            {
                if (row.parentId == 0)
                {
                    //--if parent manu is "Home" we skip "Home" in breadcrumb, because it's hard-coded in the view
                    menuBreadcrumbList.Add(row.menuId, row.shortBreadcrumb);
                }
                else
                {
                    //--if parent is other than "Home" then we include parent menu link in the breadcrumb
                    menuBreadcrumbList.Add(row.menuId, row.fullBreadcrumb);
                }
            }
            return menuBreadcrumbList;

        }

        //---function to create headers for admin/PageAdmin section so we can display pages grouped by categories
        //---similar to getBreadcrumbList function but without a link
        public Dictionary<int, string> getHeadersList()
        {
            Dictionary<int, string> menuHeaderList = new Dictionary<int, string>();

            var query = (from m in menuObj.menu_categories
                         join p in menuObj.menu_categories on m.parent_id equals p.menu_id
                         where m.menu_slug == "Page"
                         select new
                         {
                             menuId = m.menu_id,
                             menuText = m.menu_text,
                             parentId = p.menu_id,
                             shortHeader = m.menu_text ,
                             fullHeader = p.menu_text + " > " + m.menu_text
                         }
                         
                         ).ToList();

            foreach (var row in query)
            {
                if (row.parentId == 0)
                {
                    menuHeaderList.Add(row.menuId, row.shortHeader);
                }
                else
                {
                    menuHeaderList.Add(row.menuId, row.fullHeader);
                }
            }

            menuHeaderList.OrderBy(Key => Key.Value);
            return menuHeaderList;

        }
    }
}