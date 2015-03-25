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
    }
}