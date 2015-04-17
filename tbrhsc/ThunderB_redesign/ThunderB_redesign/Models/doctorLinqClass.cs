using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class doctorLinqClass
    {

        LinqDataContext objDoc = new LinqDataContext();

        public IQueryable<doctor> GetDoctors()
        {
            var allDoctors = objDoc.doctors.Select(x => x);
            return allDoctors;
        }

        public doctor getDoctorByID(int _DocId) //this method returns a row of the Faq table based on the Faq id
        {
            var allDoctors = objDoc.doctors.SingleOrDefault(x => x.dr_id == _DocId);  //SingleOrDefault(x => x.Faq_Id == _FaqId); //the SingleOrDeafult returns the value that satifies a condition or a default value in this case the conditions is that the parameter id matches one of the ids in the Faq table
            return allDoctors;
        }

        public bool commitInsert(doctor doc) //this method returns a boolean value depending on the successful insertion of the Faq row
        {
            using (objDoc) //using the mapped table
            {
                objDoc.doctors.InsertOnSubmit(doc);//this method adds an entity in a pending insert state to the entity table
                objDoc.SubmitChanges();//executes the commands to implement the changes to the database
                return true;
            }
        }

/*        public bool commitUpdate(int _FaqId, string _question, string _answer, string _category) //Update method that takes one instance of all the fields of the Faq table and also returns a boolean depending in success of update
        {
            using (objFaq)
            {
                var objUpFaq = objFaq.faqs.Single(x => x.id == _FaqId);//linq method that returns the one instance of the table that has the same id as the parameter
                objUpFaq.answer = _answer;//setting the new value 
                objUpFaq.question = _question;
                objUpFaq.category = _category;
                objFaq.SubmitChanges();
                return true;
            }
        }*/

        public bool commitDelete(int _DocId) //delete method that returns true if successful
        {
            using (objDoc)
            {
                var objDelDoc = objDoc.doctors.SingleOrDefault(x => x.dr_id == _DocId); //selecting an instance of the entity based on id
                objDoc.doctors.DeleteOnSubmit(objDelDoc);//deleting instance
                objDoc.SubmitChanges();//implementing changes
                return true;
            }
        }



    }
}