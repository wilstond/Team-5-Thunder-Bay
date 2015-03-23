using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign
{
    public class SlugToIdAttribute : ActionFilterAttribute
    {
        PageClass objPage = new PageClass();


        //static readonly IDictionary<string, int> SlugIds = new Dictionary<string, int>
        //{
        //    {"np1", 1}, 
        //    {"another-slug", 3}
        //};



        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IDictionary<string, int> SlugIds = new Dictionary<string, int>();

            var allpages = objPage.getVisiblePages();

            foreach(var page in allpages){
                SlugIds.Add(page.page_slug, page.page_id);
            }

            var slug = filterContext.RouteData.Values["slug"] as string;
            if (slug != null)
            {
                int id;
                SlugIds.TryGetValue(slug, out id);
                filterContext.ActionParameters["id"] = id;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
