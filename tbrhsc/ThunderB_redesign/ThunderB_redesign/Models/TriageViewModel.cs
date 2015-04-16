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


        public emergency_level getEmergencyById(int em_id){

            emergency_level selectedEmLevel = db.emergency_levels.Single(e => e.em_id == em_id);
            return selectedEmLevel;
        }


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
            //using (LinqDataContext db = new LinqDataContext())
            //{
                //--get a list of all docors assigned to ER
                var ERdoctors = db.doctors.Where(X => X.dept_id == 2).Select(x => x);
                return ERdoctors;
            //}
        }


        //--get a list of patients in the ER
        public IQueryable<triage> getERPatients()
        {
            //using (LinqDataContext db = new LinqDataContext())
            //{
                var Patients = db.triages.OrderBy(x => x.case_id);
                return Patients;
            //}
        }

        //--get a selected patient by case_id
        public triage getERPatientById(int case_id)
        {
            var selPatient = db.triages.Single(x => x.case_id == case_id);
            return selPatient;
        }

        // --get a waiting time for the doctor based on doctor_id parameter
        // --returns 0 if doctor has no patients at the moment 

        public TimeSpan getWaitTimes(int doctor_id)
        {
            using (LinqDataContext db = new LinqDataContext())
            {
                var lastPatient = db.triages.Where(x => x.dr_id == doctor_id 
                                                && x.discharge > DateTime.UtcNow.AddHours(-4))
                                                .OrderByDescending(x => x.case_id)
                                                .FirstOrDefault();
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
                    //--if there are more patients in the ER than in doctors
                    Dictionary<int, TimeSpan> docList = new Dictionary<int, TimeSpan>();
                    KeyValuePair<int, TimeSpan> docQueue = new KeyValuePair<int, TimeSpan>();

                    foreach (doctor doc in ERdoctors)
                    {
                        //--chaecking for the case if there are several patients in the ER, but they are assigned to one doctor, and another 
                        // doctor is still unoccupied. In this case next case goes to this "free" doctor

                        if (!Allcases.Any(x => x.dr_id == doc.dr_id))
                        {
                            docQueue = new KeyValuePair<int, TimeSpan>(doc.dr_id, new TimeSpan(0));
                            return docQueue;
                        }

                        //--for each doctor we pick the last patient in the qeueu and add them to the list containing dr_id property and wait time for that doctor)

                        var lastPatient = db.triages.Where(x => x.dr_id == doc.dr_id && x.discharge > DateTime.UtcNow.AddHours(-4)).OrderByDescending(x => x.case_id).FirstOrDefault();
                        docList.Add(doc.dr_id, (lastPatient.discharge - DateTime.UtcNow.AddHours(-4)));
                    }

                    //--after the list is completed, we sort through it and pick the member(doctor) with the shortes wait tme

                    var shortestQeueu = docList.OrderBy(Key => Key.Value).FirstOrDefault();
                    docQueue = new KeyValuePair<int, TimeSpan>(shortestQeueu.Key, shortestQeueu.Value);

                    return docQueue;
                }

            }
        }

        //--function to pull triage patient based on dynamic Where clause

        public triage getSinglePatientOnCondition(Func<triage, bool> whereClause)
        {

                var Patient = db.triages.Where(whereClause).FirstOrDefault();
                return Patient;
            
        }

        //--function to pull a collection of triage patients based on dynamic Where clause

        public IEnumerable<triage> getPatientsOnCondition(Func<triage, bool> whereClause)
        {
            
                var Patients = db.triages.Where(whereClause).Select(x => x);
                return Patients;
           
        }

        //--function to commit insert

        public bool commitInsert(triage obj)
        {
         using (LinqDataContext db = new LinqDataContext())
            {
                db.triages.InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
        }

        
    }
}