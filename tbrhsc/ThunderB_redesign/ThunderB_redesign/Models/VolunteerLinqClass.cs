using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class VolunteerLinqClass
    {

        LinqDataContext objVolunteer = new LinqDataContext();

        public IQueryable<Shift> getVolunteer()
        {
            //creating an anonymous variable with its value being the instance of the LINQ class
            var allVolunteer = objVolunteer.Shifts.Select(x => x);
            //returning IQueryable <feedback> for data bound control to bind to allfeedback;
            return allVolunteer;
        }

        public Shift getVolunteerbyID(int _id)
        {
            var allVolunteer = objVolunteer.Shifts.SingleOrDefault(x => x.shift_id == _id);
            return allVolunteer;
        }


        public bool commitInsert(Shift shift)//instance of Table Model
        {
            using (objVolunteer)
            {
                
                            //using Model to set tables columns to new values
                    objVolunteer.Shifts.InsertOnSubmit(shift);
                
                //commiting the insert against the Table
                objVolunteer.SubmitChanges();
                return true;
            }
        }


        public bool commitUpdate(int _id, string _day, string _shift)
        {
            using (objVolunteer)
            {
                var objUpVolunteer = objVolunteer.Shifts.Single(x => x.shift_id == _id);
                //setting table columns to new values being inserted
                objUpVolunteer.day = _day;
                objUpVolunteer.shifts = _shift;
                //commiting Update
                objVolunteer.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _id)
        {
            using (objVolunteer)
            {
                var objDelVolunteer = objVolunteer.Shifts.Single(x => x.shift_id == _id);
                //the delete command
                objVolunteer.Shifts.DeleteOnSubmit(objDelVolunteer);
                //committing delete
                objVolunteer.SubmitChanges();
                return true;
            }
        }
    }

}