using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class NewsPostPublic
    {
        //an instance of the linq class
        LinqDataContext objNews = new LinqDataContext();

        public IQueryable<newsTable> getNews()
        {
            //an anonymous variable that is the instance of the linq object
            var allNews = objNews.newsTables.Select(x => x);
            return allNews;
        }

        //public newsTable getNewsByID(int _id)
        //{
        //    var allNew = objNews.newsTables.SingleOrDefault(x => x.Id == _id);
        //    return allNew;
        //}
    }
}