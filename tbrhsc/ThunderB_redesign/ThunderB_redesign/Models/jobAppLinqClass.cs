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

        public IQueryable<jobApplication> GetJobApplicationsByJobId(int job_id)
        {
            var applications = objApp.jobApplications.Select(x => x).Where(x => x.job_id == job_id);
            return applications;
        }

        public jobApplication getAppByID(int id)
        {
            var application = objApp.jobApplications.SingleOrDefault(x => x.Id == id);
            return application;
        }

        public bool commitDelete(int _id)
        {
            using (objApp)
            {
                var objDelApp = objApp.jobApplications.Single(x => x.Id == _id);
                objApp.jobApplications.DeleteOnSubmit(objDelApp);
                objApp.SubmitChanges();
                return true;

            }
        }



    }
}