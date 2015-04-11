using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public bool commitInsert(subscriber sub) 
        {
            using (objLinq) 
            {
                objLinq.subscribers.InsertOnSubmit(sub);
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