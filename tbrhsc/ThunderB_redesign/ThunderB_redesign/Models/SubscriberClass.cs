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

    }
}