using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(doctorClass))]
    public partial class doctor
    {
    }

    [Bind(Exclude = "dr_id")]
    public class doctorClass
    {
        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50, ErrorMessage = "Name cant be longer thatn 50 characters")]
        public string dr_name { get; set; }
        [DisplayName("Doctor Specialty")]
        [Required(ErrorMessage = "Please enter a specialty")]
        [StringLength(50, ErrorMessage = "Specialty cant be longer than 50 characters")]
        public string dr_specialty { get; set; }
        [DisplayName("Doctor Office Name")]
        [Required(ErrorMessage = "Please enter an office name")]
        [StringLength(50, ErrorMessage = "Specialty cant be longer than 50 characters")]
        public string dr_office_name { get; set; }
        [DisplayName("Doctor Office Address")]
        [Required(ErrorMessage = "Please enter office address")]
        [StringLength(200, ErrorMessage = "Address cant be longer than 200 characters")]
        public string dr_office_address { get; set; }
        [Required(ErrorMessage = "Please enter a phone number")]
        [StringLength(10, ErrorMessage = "Please enter phone number")]
        [DisplayName("Phone Number")]
        public string dr_office_phone { get; set; }
 
        [Required(ErrorMessage = "Please enter a phone number")]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be between 0 and 250")]
        [DisplayName("User Id")]
        public int user_id { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "Please enter a department")]
        public int dept_id { get; set; }


    }
}