using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThunderB_redesign.Models
{
    
    public class DonationVM
    {
        public int dnr_id {get; set; }

        [DisplayName("Donor Email")]
        [Required(ErrorMessage = "Please enter your email")]
        public string dnr_email { get; set; }
        
        [DisplayName("Donor Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string dnr_name { get; set; }

        public System.Nullable<char> _dnr_anonymous { get; set; }

        public int dtn_id {get; set;}
        public int dtn_dnr_id {get; set;}
        [DisplayName("Donor Name")]
        [Required(ErrorMessage = "Please enter the amount")]
        public decimal dtn_amount { get; set; }

        public 

    }

}