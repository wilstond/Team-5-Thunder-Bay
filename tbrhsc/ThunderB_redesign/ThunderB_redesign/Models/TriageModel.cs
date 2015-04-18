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
        
        //------patient name----------------

        [DisplayName("Patient/Case Name")]
        [Required(ErrorMessage = "Please enter patient/case name")]
        public string patient_name { get; set; }

        //------arrival datetime----------------

        [DisplayName("Arrival")]
        [Required(ErrorMessage = "Arrival date & time are required")]
        public System.DateTime arrival { get; set; }


        // this property uses custom validation attribute Models/DateRangeAttribute.cs to check if discharge time that 
        // user enters through edit form is not in the past

        [DisplayName("Discharge")]
        [Required(ErrorMessage = "Discharge date & time are required")]
        [DateRange]
        public System.DateTime discharge { get; set; }

        //-------type of emergency--------------------------

        [DisplayName("Complaint")]
        [Required(ErrorMessage = "Select type of complaint")]
        public int em_id { get; set; }

        //-------doctor id of the doctor assigned to the case---------------

        public Nullable<int> dr_id { get; set; }

       
    }
}
