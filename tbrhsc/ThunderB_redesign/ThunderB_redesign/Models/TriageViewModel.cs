using System;
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
    }
}