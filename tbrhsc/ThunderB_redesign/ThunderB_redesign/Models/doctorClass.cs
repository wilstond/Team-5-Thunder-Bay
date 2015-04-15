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
        public string dr_name { get; set; }
        [DisplayName("Doctor Specialty")]
        public string dr_specialty { get; set; }
        [DisplayName("Doctor Office Name")]
        public string dr_office_name { get; set; }
        [DisplayName("Doctor Office Address")]
        public string dr_office_address { get; set; }
        [DisplayName("Phone Number")]
        public string dr_office_phone { get; set; }
        [DisplayName("Photo")]
        public string dr_photo { get; set; }
        [DisplayName("User Id")]
        public int user_id { get; set; }
        [DisplayName("Department")]
        public int dept_id { get; set; }


    }
}