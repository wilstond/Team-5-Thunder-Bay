using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class VolunteerViewModel
    {
         public List<Shift> shifList { get; set; }
        public IQueryable<Volunteer> volList{ get; set; }
        public Volunteer volInfo { get; set; }
        public Shift SelectedVolunteer { get; set; }
        public string DisplayMode { get; set; }



        public List<Shift> getVolunteerList()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                //Option for building a list of DISTINCT values - not used here
                //var allEmergencies = db.emergency_levels.GroupBy(x => x.em_description).Select(x => x.First());

                var allvolunteer_shift = db.Shifts.GroupBy(x => x.volunteer_id).Select(x => x);

                List<Shift> shiftList = new List<Shift>();
                foreach (Shift shift in allvolunteer_shift)
                {
                    shiftList.Add(shift);

                }
                return shiftList;
            }
        }

        // -- get alist of volunteers
        public IQueryable<Volunteer> getVollist()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
               //--get a list of all volunteers a
                var volList = db.Volunteers.Select(x => x);
                return volList;
            }
        }

        
        //public List<Shift> getShift()
        //{
        //    using (LinqDataContext db = new LinqDataContext())
        //    {

        //        var shiftlist = db.Shifts.GroupBy(X=>X.volunteer_id).Select(x => x);
        //        return shiftlist;
        //    }
        //}

        //// --get a waiting time for the doctor based on doctor_id parameter
        //// --returns 0 if doctor has no patients at th moment

        //public TimeSpan getWaitTimes(int doctor_id)
        //{
        //    using (LinqDataContext db = new LinqDataContext())
        //    {
        //        var doctWaitTime = (from x in db.triages
        //                            where x.dr_id == doctor_id
        //                            select (x.discharge - x.arrival).Ticks);
        //        if (doctWaitTime.Count()>0)
        //        {
        //            var tt = doctWaitTime.Sum();
        //            return new TimeSpan(tt);
        //        }
        //        else
        //        {
        //            return new TimeSpan(0);
        //        }
                                   
        //    }
        //}

        ////--function to find the next available doctor to assign an ER patient to
        ////--could be either doctor that is currently unoccupied or doctor with the shortest
        ////--wait time (queu)

        //public int getShortQueu(){
        //    using (LinqDataContext db = new LinqDataContext())
        //    {
        //        //--get a list of all docors assigned to ER
        //        var ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);

        //        //--https://msdn.microsoft.com/en-us/library/vstudio/bb386922%28v=vs.100%29.aspx
        //        // --get a list of current ER cases grouped by Doctor ID
        //        //---and showing total wait for each doctor's queu
        //        var doctQueuRes = from triage in db.triages
        //                           group triage by triage.dr_id into grouping
        //                           select new 
        //                           {
        //                                grouping.Key,
        //                                TotalWait = grouping.Sum(x => ( x.discharge - x.arrival ).Ticks)
        //                           };
        //         if (doctQueuRes == null)
        //         {
        //             //--if there is no patients in the ER - we pick first doctor from
        //             // doctors assigned to the ER
        //             var dr_id = ERdoctors.First().dr_id;
        //             return dr_id;
        //         }
        //         else if (ERdoctors.Count() > doctQueuRes.Count())
        //         {
        //             //--if number of patients in the ER is LESS then number of doctors
        //             // we pick first of ER doctors that have no patients at the moment
        //             var next_doc_id = 0;
        //             foreach (doctor doc in ERdoctors)
        //             {
        //                 if (!doctQueuRes.Any(x =>x.Key == doc.dr_id))
        //                 {
        //                     next_doc_id = doc.dr_id;
        //                     break;
        //                 }
        //             }
        //             return next_doc_id;
        //         }
        //         else
        //         {
        //             //--if all doctors are occupied we select the doctor with the shortest queu
        //             var min_queu = doctQueuRes.OrderBy(m => m.TotalWait).FirstOrDefault();
        //             return min_queu.Key;
        //         }
        //     }
        //}

    }
    
}