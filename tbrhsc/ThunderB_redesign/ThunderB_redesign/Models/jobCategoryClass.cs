using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(jobCategoryClass))]
    public partial class jobCategory
    {

    }

    [Bind(Exclude = "Id")]
    public class jobCategoryClass
    {
        [DisplayName("Job Category")]
        [Required(ErrorMessage = "Please enter a category")]
        public string category { get; set; }

    }
}