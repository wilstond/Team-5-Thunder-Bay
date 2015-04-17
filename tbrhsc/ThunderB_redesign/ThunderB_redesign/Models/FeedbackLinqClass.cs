using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class FeedbackLinqClass
    {
        //connecting to the feedback table in the database
        LinqDataContext objFeedback = new LinqDataContext();

        //getting all submitted feedback
        public IQueryable<feedback> getfeedback()
        {
            //creating an anonymous variable with its value being the instance of the LINQ class
            var allFeedbacks = objFeedback.feedbacks.Select(x => x);
            //returning IQueryable <feedback> for data bound control to bind to allfeedback;
            return allFeedbacks;
        }

        //getting specific feedback when selected by ID
        public feedback getFeedbackbyID(int _id)
        {
            var allfeedback = objFeedback.feedbacks.SingleOrDefault(x => x.Id == _id);
            return allfeedback;
        }

        //When the feedback is submitted by the user it is committed to the database
        public bool commitInsert(feedback feedback)//instance of Table Model
        {
            using (objFeedback)
            {
                //using Model to set tables columns to new values
                objFeedback.feedbacks.InsertOnSubmit(feedback);
                //commiting the insert against the Table
                objFeedback.SubmitChanges();
                return true;
            }
        }

        //when updating the feedback
        public bool commitUpdate(int _id, string _name, string _email, string _topic, string _feedback)
        {
            using (objFeedback)
            {
                var objUpFeedback = objFeedback.feedbacks.Single(x => x.Id == _id);
                //setting table columns to new values being inserted
                objUpFeedback.name = _name;
                objUpFeedback.email = _email;
                objUpFeedback.topic = _topic;
                objUpFeedback.fbcontent = _feedback;
                //commiting Update
                objFeedback.SubmitChanges();
                return true;
            }
        }

        //deleting the feedback, deletes by ID
        public bool commitDelete(int _id)
        {
            using (objFeedback)
            {
                var objDelFeedback = objFeedback.feedbacks.Single(x => x.Id == _id);
                //the delete command
                objFeedback.feedbacks.DeleteOnSubmit(objDelFeedback);
                //committing delete
                objFeedback.SubmitChanges();
                return true;
            }
        }
    }

}