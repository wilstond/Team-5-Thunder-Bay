using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(DonorModel))]
    public partial class donor
    {

    }

    [Bind(Exclude="dnr_id")]
    public class DonorModel
    {
        [DisplayName("Donor Email")]
        [Required(ErrorMessage = "Please enter your email")]
        public string dnr_email { get; set; }

        [DisplayName("Donor Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string dnr_name { get; set; }

        [DisplayName("Donor Phone")]
        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid phone number")]
        public string dnr_phone { get; set; }


        [DisplayName("Donor Apartment Number")]
        [Required(ErrorMessage = "Please enter your apartment number")]
        public string dnr_apt_no { get; set; }


        [DisplayName("Donor street name")]
        [Required(ErrorMessage = "Please enter your street name")]
        public string dnr_street { get; set; }


        [DisplayName("Donor City")]
        [Required(ErrorMessage = "Please enter your city")]
        public string dnr_city { get; set; }

        [DisplayName("Donor Province")]
        [Required(ErrorMessage = "Please enter your province")]
        public string dnr_province { get; set; }


        [DisplayName("Donor Country")]
        [Required(ErrorMessage = "Please enter your country")]
        public string dnr_country { get; set; }


        [DisplayName("Donor Postal Code")]
        [Required(ErrorMessage = "Please enter your postal code")]
        public string dnr_postal_code { get; set; }
    }
}