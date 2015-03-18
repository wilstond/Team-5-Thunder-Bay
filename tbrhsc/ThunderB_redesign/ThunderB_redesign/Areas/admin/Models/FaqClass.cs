using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Areas.admin.Models
{
    public class FaqClass
    {
        FaqDataContext objFaq = new FaqDataContext();

        public IQueryable<Faq> GetFaqs()
        {
            var allFaqs = objFaq.Faqs.Select(x => x);
            return allFaqs;
        }

        public Faq getFaqByID(int _FaqId) //this method returns a row of the Faq table based on the Faq id
        {
            var allFaq = objFaq.Faqs.SingleOrDefault(x => x.id == _FaqId);  //SingleOrDefault(x => x.Faq_Id == _FaqId); //the SingleOrDeafult returns the value that satifies a condition or a default value in this case the conditions is that the parameter id matches one of the ids in the Faq table
            return allFaq;
        }

        public bool commitInsert(Faq faq) //this method returns a boolean value depending on the successful insertion of the Faq row
        {
            using (objFaq) //using the mapped table
            {
                objFaq.Faqs.InsertOnSubmit(faq);//this method adds an entity in a pending insert state to the entity table
                objFaq.SubmitChanges();//executes the commands to implement the changes to the database
                return true;
            }
        }

        public bool commitUpdate(int _FaqId, string _question, string _answer, string _category) //Update method that takes one instance of all the fields of the Faq table and also returns a boolean depending in success of update
        {
            using (objFaq)
            {
                var objUpFaq = objFaq.Faqs.Single(x => x.id == _FaqId);//linq method that returns the one instance of the table that has the same id as the parameter
                objUpFaq.answer = _answer;//setting the new value 
                objUpFaq.question = _question;
                objUpFaq.category = _category;
                objFaq.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _FaqId) //delete method that returns true if successful
        {
            using (objFaq)
            {
                var objDelFaq = objFaq.Faqs.Single(x => x.id == _FaqId); //selecting an instance of the entity based on id
                objFaq.Faqs.DeleteOnSubmit(objDelFaq);//deleting instance
                objFaq.SubmitChanges();//implementing changes
                return true;
            }
        }

    }
}