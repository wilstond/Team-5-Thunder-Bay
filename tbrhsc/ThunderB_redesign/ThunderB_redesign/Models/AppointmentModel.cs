using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(AppointmentModel))]
    public partial class appointment
    {

    }

    [Bind(Exclude = "apt_id")]
    public class AppointmentModel
    {

        [DisplayName("Doctor Id")]
        [Required(ErrorMessage = "Doctor Id is required")]
        public int dr_id { get; set; }

        [DisplayName("Date requested")]
        [Required(ErrorMessage = "*")]
        public System.DateTime date_req { get; set; }

        [DisplayName("Appointment booked date")]
        [Required(ErrorMessage = "*")]
        public System.DateTime? date_book { get; set; }

        [DisplayName("Appintment booked time")]
        [Required(ErrorMessage = "*")]
        public string time_book { get; set; }

        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Please enter patient name")]
        public string pat_name { get; set; }

        [DisplayName("Patient Phone, ex. 4161234567")]
        [Required(ErrorMessage = "Please enter patient phone")]
        [StringLength(10, ErrorMessage="10 digits phone numbers only are accepted")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage="10 digit phone number without hyphens, dots or brackets is expected.")]
        public string pat_phone { get; set; }

        [DisplayName("Patient Email")]
        [Required(ErrorMessage = "Please enter patient email")]
        [EmailAddress(ErrorMessage="Invalid email address")]
        public string pat_email { get; set; }

        [DisplayName("Patient Address")]
        [Required(ErrorMessage = "Please enter patient address")]
        public string pat_address { get; set; }

        [StringLength(12, ErrorMessage = "12 character OHIP card # is expected")]
        [DisplayName("Patient Health Card")]
        [Required(ErrorMessage = "Please enter patient OHIP #")]
        public string pat_ohip { get; set; }

        [DisplayName("Family Doctor Name")]
        [Required(ErrorMessage = "Please enter Family Doctor name")]
        public string fam_dr_name { get; set; }

        [StringLength(10, ErrorMessage = "10 digits phone numbers only are accepted")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "10 digit phone number without hyphens, dots or brackets is expected.")]
        [DisplayName("Family Doctor Phone,  ex. 4161234567")]
        [Required(ErrorMessage = "Please enter Family Doctor phone")]
        public string fan_dr_phone { get; set; }

        [DisplayName("Appt Status")]
        public string app_status { get; set; }
    }
}