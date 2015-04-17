using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{

    [MetadataType(typeof(FeedbackModel))]
    public partial class feedback
    {

    }

    //Validation for the feedback fields
    [Bind(Exclude = "id")]
    public class FeedbackModel
    {
        [DisplayName("Name ")]
        [Required(ErrorMessage = "Please enter your name")]
        public string name { get; set; }

        [DisplayName("Email ")]
        [Required(ErrorMessage = "Please enter an email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }

        [DisplayName("Topic ")]
        [Required(ErrorMessage = "Please enter a topic")]
        public string topic { get; set; }

        [DisplayName("Feedback ")]
        [Required(ErrorMessage = "Please enter feedback")]
        public string fbcontent { get; set; }
    }
}