using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
  
        [MetadataType(typeof(jobClass))]
        public partial class Job
        {
        }

        [Bind(Exclude = "Id")]
        public class jobClass
        {

            [DisplayName("Job Title")]
            [StringLength(50, ErrorMessage = "title should be under 50 chatacters long")]
            [Required(ErrorMessage = "Please enter job title")]
            public string job_title { get; set; }
            [Required(ErrorMessage = "Please enter a closing date")]
            [DisplayName("Closing Date")]
            [DataType(DataType.Date)]
            public DateTime closing_date { get; set; }
            [Required(ErrorMessage = "Please enter job description")]
            [DisplayName("Job Description")]
            [AllowHtml]
            public string job_description { get; set; }
            [Required(ErrorMessage = "Please enter a date")]
            [DisplayName("Date Posted")]
            [DataType(DataType.Date)]
            public DateTime date_posted { get; set; }
            [Required(ErrorMessage = "Please enter category")]
            [DisplayName("Category")]
            public string category_id { get; set; }

        }
    }
