using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class TriageViewModel
    {
        LinqDataContext db = new LinqDataContext();
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
                //var doctWaitTime = (from x in db.triages
                //                    where x.dr_id == doctor_id
                //                    select (x.discharge - x.arrival).Ticks); 
                //var doctWaitTime = (from x in db.triages
                //                    where x.dr_id == doctor_id && x.discharge > DateTime.UtcNow.AddHours(-4)
                //                    select (x.discharge - DateTime.UtcNow.AddHours(-4)).Ticks);
                //if (doctWaitTime.Count() > 0)
                //{
                //    var tt = doctWaitTime.Sum();
                //    return new TimeSpan(tt);
                //}
                //else
                //{
                //    return new TimeSpan(0);
                //}

                var lastPatient = db.triages.Where(x => x.dr_id == doctor_id).OrderByDescending(x => x.discharge).FirstOrDefault();
                if (lastPatient == null)
                {
                    return new TimeSpan(0);
                }
                else
                {
                    return lastPatient.discharge - DateTime.UtcNow.AddHours(-4);
                }
                


            }
        }

        //---function determine current case for each doctor
        public triage getCurrentCaseByDoctor(int dr_id)
        {
            var current_case = db.triages.Where(x => x.dr_id == dr_id).OrderBy(x => x.case_id).FirstOrDefault();
            return current_case;
        }



        //--function to find the next available doctor to assign an ER patient to
        //--could be either doctor that is currently unoccupied or doctor with the shortest
        //--wait time (queu)

        public KeyValuePair<int, TimeSpan> getShortQueu()
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                //--get a list of all docors assigned to ER
                var ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
                var Allcases = db.triages.Select(x => x);


                var numPatients = db.triages.Count();
                if(numPatients == 0)
                {
                    //--if there is no patients in the ER - we pick first doctor from
                    // doctors assigned to the ER
                    var dr_id = ERdoctors.First().dr_id;
                    KeyValuePair<int, TimeSpan> docQueue = new KeyValuePair<int, TimeSpan>(dr_id, new TimeSpan(0));

                    return docQueue;
                }
                else if(numPatients < ERdoctors.Count())
                {
                    //--if number of patients in the ER is LESS then number of doctors
                    // we pick first of ER doctors that have no patients at the moment
                    //var next_doc_id = 0;
                    KeyValuePair<int, TimeSpan> docQueue = new KeyValuePair<int, TimeSpan>();

                    foreach (doctor doc in ERdoctors)
                    {
                        if (!Allcases.Any(x => x.dr_id == doc.dr_id))
                        {
                            docQueue = new KeyValuePair<int, TimeSpan>(doc.dr_id, new TimeSpan(0));
                            break;
                        }
                    }
                    return docQueue;
                    
                }
                else
                {
                    Dictionary<int, TimeSpan> docList = new Dictionary<int, TimeSpan>();
                    KeyValuePair<int, TimeSpan> docQueue = new KeyValuePair<int, TimeSpan>();

                    foreach (doctor doc in ERdoctors)
                    {
                        if (!Allcases.Any(x => x.dr_id == doc.dr_id))
                        {
                            docQueue = new KeyValuePair<int, TimeSpan>(doc.dr_id, new TimeSpan(0));
                            return docQueue;
                        }
                        var lastPatient = db.triages.Where(x => x.dr_id == doc.dr_id).OrderByDescending(x => x.discharge).FirstOrDefault();
                        docList.Add(doc.dr_id, (lastPatient.discharge - DateTime.UtcNow.AddHours(-4)));
                    }
                    var shortestQeueu = docList.OrderBy(Key => Key.Value).FirstOrDefault();
                    docQueue = new KeyValuePair<int, TimeSpan>(shortestQeueu.Key, shortestQeueu.Value);

                    return docQueue;
                }

            }
        }

    }
}