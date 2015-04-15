using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(jobApplicationClass))]
    public partial class jobApplication
    {
    }

    [Bind(Exclude = "Id")]
    public abstract class jobApplicationClass
    {

        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "Maximum lenght is 50 characters")]
        [Required(ErrorMessage = "Please enter first name")]
        public string first_name { get; set; }
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Maximum lenght is 50 characters")]
        [Required(ErrorMessage = "Please enter last name")]
        public string last_name { get; set; }
        [DisplayName("Job ID")]
        [Required(ErrorMessage = "Please enter job id")]
        public int job_id { get; set; }
        [DisplayName("Email")]
        [StringLength(125, ErrorMessage = "Maximum lenght is 125 characters")]
        [EmailAddress(ErrorMessage = "Invalid email adress")]
        [Required(ErrorMessage = "Please enter email")]
        public string email { get; set; }
        [DisplayName("City")]
        [StringLength(50, ErrorMessage = "Maximum lenght is 50 characters")]
        [Required(ErrorMessage = "Please enter a city")]
        public string city { get; set; }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please enter phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid number")]
        public string phone { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Please enter an address")]
        [StringLength(200, ErrorMessage = "Maximum lenght is 125 characters")]
        public string address { get; set; }
        [DisplayName("Cover Letter")]
        [Required(ErrorMessage = "Please copy and paste your cover letter here")]
        [AllowHtml]
        public string cover_letter { get; set; }
        [AllowHtml]
        [DisplayName("Resume")]
        [Required(ErrorMessage = "Please copy and paste your resume here")]
        public string resume { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Date Posted")]
        public string date_posted { get; set; }


    }
}