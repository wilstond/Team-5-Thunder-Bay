using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(TriageModel))]
    public partial class triage
    {

    }

    [Bind(Exclude = "case_id")]
    public class TriageModel
    {
        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Pleaase enter patient name")]
        public string patient_name { get; set; }

        public System.DateTime arrival { get; set; }

        public System.DateTime discharge { get; set; }

        public int em_id { get; set; }

        public Nullable<int> dr_id { get; set; }
    }
}
