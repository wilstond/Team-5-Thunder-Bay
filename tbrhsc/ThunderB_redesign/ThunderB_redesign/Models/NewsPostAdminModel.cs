using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class NewsPostAdminModel
    {
        //an instance of the class database
        LinqDataContext objNews = new LinqDataContext();
        public IQueryable<newsTable> getNews()
        {
            //entering the news table and assigning it into a variable
            //grab all the news
            var news = objNews.newsTables.Select(x => x);
            return news;
        }
    }
}