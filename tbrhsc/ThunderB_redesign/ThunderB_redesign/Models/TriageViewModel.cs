using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class TriageViewModel
    {
        public List<triage> ERpatients { get; set; }
        public IQueryable<doctor> ERdoctors { get; set; }
        public doctor docInfo { get; set; }
        public triage SelectedERpatient { get; set; }
        public string DisplayMode { get; set; }

        

        public List<emergency_level> getEmergencyList()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                //Option for building a list of DISTINCT values - not used here
                //var allEmergencies = db.emergency_levels.GroupBy(x => x.em_description).Select(x => x.First());
               
                var allEmergencies = db.emergency_levels.Select(x => x);

                List<emergency_level> emegencyList = new List<emergency_level>();
                foreach (emergency_level emergency in allEmergencies)
                {
                    emegencyList.Add(emergency);

                }
                return emegencyList;
            }
        }

        // -- get alist of ER doctors
        public IQueryable<doctor> getERdoctors()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                //--get a list of all docors assigned to ER
                var ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
                return ERdoctors;
            }
        }

        public IQueryable<triage> getERPatients()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                var Patients = db.triages.OrderBy(x => x.case_id);
                return Patients;
            }
        }

        // --get a waiting time for the doctor based on doctor_id parameter
        // --returns 0 if doctor has no patients at th moment

        public TimeSpan getWaitTimes(int doctor_id)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                var doctWaitTime = (from x in db.triages
                                    where x.dr_id == doctor_id
                                    select (x.discharge - x.arrival).Ticks);
                if (doctWaitTime.Count()>0)
                {
                    var tt = doctWaitTime.Sum();
                    return new TimeSpan(tt);
                }
                else
                {
                    return new TimeSpan(0);
                }
                                   
            }
        }

        //--function to find the next available doctor to assign an ER patient to
        //--could be either doctor that is currently unoccupied or doctor with the shortest
        //--wait time (queu)

        public int getShortQueu(){
            using (LinqDataContext db = new LinqDataContext())
            {
                //--get a list of all docors assigned to ER
                var ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);

                //--https://msdn.microsoft.com/en-us/library/vstudio/bb386922%28v=vs.100%29.aspx
                // --get a list of current ER cases grouped by Doctor ID
                //---and showing total wait for each doctor's queu
                var doctQueuRes = from triage in db.triages
                                   group triage by triage.dr_id into grouping
                                   select new 
                                   {
                                        grouping.Key,
                                        TotalWait = grouping.Sum(x => ( x.discharge - x.arrival ).Ticks)
                                   };
                 if (doctQueuRes == null)
                 {
                     //--if there is no patients in the ER - we pick first doctor from
                     // doctors assigned to the ER
                     var dr_id = ERdoctors.First().dr_id;
                     return dr_id;
                 }
                 else if (ERdoctors.Count() > doctQueuRes.Count())
                 {
                     //--if number of patients in the ER is LESS then number of doctors
                     // we pick first of ER doctors that have no patients at the moment
                     var next_doc_id = 0;
                     foreach (doctor doc in ERdoctors)
                     {
                         if (!doctQueuRes.Any(x =>x.Key == doc.dr_id))
                         {
                             next_doc_id = doc.dr_id;
                             break;
                         }
                     }
                     return next_doc_id;
                 }
                 else
                 {
                     //--if all doctors are occupied we select the doctor with the shortest queu
                     var min_queu = doctQueuRes.OrderBy(m => m.TotalWait).FirstOrDefault();
                     return min_queu.Key;
                 }
             }
        }

    }
}