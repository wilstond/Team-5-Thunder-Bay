using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class ShiftLinqClass
    {
        LinqDataContext objShift = new LinqDataContext();

        public IQueryable<volshift> getShift()
        {
            //creating an anonymous variable with its value being the instance of the LINQ class
            var allShift = objShift.volshifts.Select(x => x);
            //returning IQueryable <feedback> for data bound control to bind to allfeedback;
            return allShift;
        }

        public volshift getShiftbyID(int _id)
        {
            var allShift = objShift.volshifts.SingleOrDefault(x => x.Id == _id);
            return allShift;
        }


        public bool commitInsert(volshift shift)//instance of Table Model
        {
            using (objShift)
            {
                //using Model to set tables columns to new values
                objShift.volshifts.InsertOnSubmit(shift);
                //commiting the insert against the Table
                objShift.SubmitChanges();
                return true;
            }
        }


        public bool commitUpdate(int _id, string _name, string _email,  string _day)
        {
            using (objShift)
            {
                var objUpShift = objShift.volshifts.Single(x => x.Id == _id);
                //setting table columns to new values being inserted
                objUpShift.name = _name;
                objUpShift.email = _email;
                objUpShift.shiftday = _day;
                
                //commiting Update
                objShift.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _id)
        {
            using (objShift)
            {
                var objDelShift = objShift.volshifts.Single(x => x.Id == _id);
                //the delete command
                objShift.volshifts.DeleteOnSubmit(objDelShift);
                //committing delete
                objShift.SubmitChanges();
                return true;
            }
        }
    }
}