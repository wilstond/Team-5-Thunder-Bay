using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Models
{
    public class SubscriberClass
    {

        LinqDataContext objLinq = new LinqDataContext();

        public IEnumerable<subscriber> getSubscribers()
        {
            var allSubscribers = objLinq.subscribers.Select(x => x);
            return allSubscribers;
        }

        public subscriber getSubscriberByID(int _id)
        {
            var sub = objLinq.subscribers.SingleOrDefault(x => x.sub_id == _id);

            //var allProducts = from x in objProd.products select x;

            return sub;
        }

        public bool commitInsert(subscriber sub) 
        {
            using (objLinq) 
            {
                objLinq.subscribers.InsertOnSubmit(sub);
                objLinq.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _subId, string _subName, string _subEmail)
        {
            using (objLinq)
            {
                var subUpd = objLinq.subscribers.Single(x => x.sub_id == _subId);
                subUpd.sub_name = _subName;
                subUpd.sub_email = _subEmail;
                objLinq.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int subID) //delete method that returns true if successful
        {
            using (objLinq)
            {
                var objSubscriber = objLinq.subscribers.Single(x => x.sub_id == subID); //selecting an instance of the entity based on id
                objLinq.subscribers.DeleteOnSubmit(objSubscriber);//deleting instance
                objLinq.SubmitChanges();//implementing changes
                return true;
            }
        }

    }
}