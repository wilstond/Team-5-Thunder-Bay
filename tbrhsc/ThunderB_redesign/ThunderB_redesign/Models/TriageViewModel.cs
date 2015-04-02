﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class TriageViewModel
    {
        public List<triage> ERpatients { get; set; }
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

    }
}