using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(volunteerShiftModel))]
    public partial class volshift
    {

    }

    [Bind(Exclude = "id")]
    public class volunteerShiftModel
    {
        [DisplayName("Name ")]
        [Required(ErrorMessage = "Please enter your name")]
        public string name { get; set; }

        [DisplayName("Email ")]
        [Required(ErrorMessage = "Please enter an email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }

       
       // [DisplayName("day")]
      // [Required(ErrorMessage = "Please enter your Free Day")]
       // public string shiftday { get; set; }
    }
    
}