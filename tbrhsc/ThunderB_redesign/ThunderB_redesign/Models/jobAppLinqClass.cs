using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class jobAppLinqClass
    {
        LinqDataContext objApp = new LinqDataContext();

        public bool commitInsert(jobApplication app)
        {
            using (objApp)
            {
                objApp.jobApplications.InsertOnSubmit(app);
                objApp.SubmitChanges();
                return true;
            }
        }


    }
}