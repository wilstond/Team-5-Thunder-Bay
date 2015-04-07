using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class AlertModelClass
    {
        //a class of the database, and an instance of that class
        LinqDataContext objAlert = new LinqDataContext();
        public IQueryable<AlertTable> getAlerts()
        {
            //entered the table and assign it to a variable
            //grabbing all the alerts 
            //selecting all alerts and their rows
            var alerts = objAlert.AlertTables.Select(x => x);
            return alerts;
        }




        //This is the activation method
        //bool is being used because what is being returned doesn't mattter, we are just changing the table
        //_id is the id associated with each drop down list item
        public bool activateAlert(int _id)
        {
            using (objAlert)
            {
                //using the object, we are going to the alert table and selecting everything and changing their values to 0
                var collection = objAlert.AlertTables.Where(x => x.alertNumber == 1);
                foreach (var x in collection)
                {
                    x.alertNumber = 0;
                }
               


                //this alert variable goes through the alert table and using singleordefault(which grabs the first matching value it finds) and matches the id to the alert id and sets the value to 1
                //and then submits the changes, which 'activates' the alert
                if (_id != 0)
                {
                    var alert = objAlert.AlertTables.SingleOrDefault(x => x.Id == _id);
                    alert.alertNumber = 1;
                }

                objAlert.SubmitChanges();
                return true;
                
            }
        }

        public AlertTable getAlert()
        {
            using (objAlert)
            {
                //using the same object as before
                //going into the table and selecting the alert number which has the value of 1, which is the one that is activated
                // var alert is the activated row
                //the variable id
                var alert = objAlert.AlertTables.SingleOrDefault(x => x.alertNumber == 1);
                //returning the id of the selected row
                if (alert != null)
                {
                    return alert;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}