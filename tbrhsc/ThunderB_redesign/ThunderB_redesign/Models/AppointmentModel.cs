using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    public class AppointmentModel
    {
        public int apt_id { get; set; }

        [DisplayName("Doctor Id")]
        [Required(ErrorMessage = "Doctor Id is required")]
        public int dr_id { get; set; }

        [DisplayName("Date requested")]
        [Required(ErrorMessage = "*")]
        public System.DateTime date_req { get; set; }

        [DisplayName("Appointment booked date")]
        [Required(ErrorMessage = "*")]
        public System.DateTime date_book { get; set; }

        [DisplayName("Appintment booked time")]
        [Required(ErrorMessage = "*")]
        public string time_book { get; set; }

        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Please enter patient name")]
        public string pat_name { get; set; }

        [DisplayName("Patient Phone")]
        [Required(ErrorMessage = "Please enter patient phone")]
        public string pat_phone { get; set; }

        [DisplayName("Patient Email")]
        [Required(ErrorMessage = "Please enter patient email")]
        public string pat_email { get; set; }

        [DisplayName("Patient Address")]
        [Required(ErrorMessage = "Please enter patient address")]
        public string pat_address { get; set; }

        [DisplayName("Patient Health Card")]
        [Required(ErrorMessage = "Please enter patient OHIP #")]
        public string pat_ohip { get; set; }

        [DisplayName("Family Doctor Name")]
        [Required(ErrorMessage = "Please enter Family Doctor name")]
        public string fam_dr_name { get; set; }

        [DisplayName("Family Doctor Phone")]
        [Required(ErrorMessage = "Please enter Family Doctor phone")]
        public string fan_dr_phone { get; set; }

        [DisplayName("Appt Status")]
        public string app_status { get; set; }
    }
}