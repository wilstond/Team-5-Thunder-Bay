using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class JobLinqClass
    {
        LinqDataContext objJob = new LinqDataContext();


        public IQueryable<Job> GetJobs()
        {
            var allJobs = objJob.Jobs.Select(x => x);
            return allJobs;
        }

        public IQueryable<jobCategory> GetCategories()
        {
            var categories = objJob.jobCategories.Select(x => x);
            return categories;
        }


        public Job getJobByID(int id)
        {
            var job = objJob.Jobs.SingleOrDefault(x => x.Id == id);
            return job;
        }

        public bool commitInsert(Job job)
        {
            using (objJob)
            {
                objJob.Jobs.InsertOnSubmit(job);
                objJob.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _id, string _jobtitle, DateTime _closingdate, string _jobdescription, int _categoryid)
        {
            using (objJob)
            {
                var objUpPro = objJob.Jobs.Single(x => x.Id == _id);
                objUpPro.job_title = _jobtitle;
                objUpPro.closing_date = _closingdate;
            
                objUpPro.job_description = _jobdescription;
                objUpPro.category_id = _categoryid;
                objJob.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _id)
        {
            using (objJob)
            {
                var objDelPro = objJob.Jobs.Single(x => x.Id == _id);
                objJob.Jobs.DeleteOnSubmit(objDelPro);
                objJob.SubmitChanges();
                return true;

            }
        }


    }
}