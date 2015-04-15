using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{

    [MetadataType(typeof(departmentClass))]
    public partial class department
    {

    }

     [Bind(Exclude = "id")]
    public class departmentClass
    {
         [Required]
         public string dept_name { get; set; }
         [Required]
         public string dept_description { get; set; }

    }
}