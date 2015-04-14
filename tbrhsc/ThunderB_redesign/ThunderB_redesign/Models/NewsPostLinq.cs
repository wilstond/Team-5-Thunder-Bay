using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class NewsPostLinq
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

        public IQueryable<newsTable> getTopNews()
        {
            var topNews = objNews.newsTables.OrderByDescending(x => x.Id).Take(5);
            return topNews;
        }

        public IQueryable<newsTable> orderNews()
        {
            var orderNews = objNews.newsTables.OrderByDescending(x => x.Id);
            return orderNews;
        }

        //public IQueryable<newsTable> getNews()
        //{
        //    //an anonymous variable that is the instance of the linq object
        //    var allNews = objNews.newsTables.Select(x => x);
        //    return allNews;
        //}

        public newsTable getNewsByID(int _id)
        {
            var allNew = objNews.newsTables.SingleOrDefault(x => x.Id == _id);
            return allNew;
        }

        //an instance the newstable model as a parameter
        public bool commitInsert(newsTable newNews)
        {
            //ensuring data will be disposed of when finished using objNews
            using (objNews)
            {
                //using the model to set table columns to new balues veing passed and providing it to the insert command
                objNews.newsTables.InsertOnSubmit(newNews);
                //committing insert against database
                objNews.SubmitChanges();
                return true;
            }
        }


        //committing the update functionality
        public bool commitUpdate(int _id, string _stories, string _headline, string _date, string _author)
        {
            using (objNews)
            {
                var objUpdateNews = objNews.newsTables.Single(x => x.Id == _id);
                //setting table columns to new values being passed
                objUpdateNews.stories = _stories;
                objUpdateNews.headline = _headline;
                objUpdateNews.date = _date;
                objUpdateNews.author = _author;
                //commit against database
                objNews.SubmitChanges();
                return true;
            }
        }

        //delete news post functionality
        public bool commitDelete(int _id)
        {
            using (objNews)
            {
                var objDeleteNews = objNews.newsTables.Single(x => x.Id == _id);
                //delete command
                objNews.newsTables.DeleteOnSubmit(objDeleteNews);
                //committing delete
                objNews.SubmitChanges();
                return true;
            }
        }
    }

}    
