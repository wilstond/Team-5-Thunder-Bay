using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{   
    [MetadataType(typeof(SubscriberModel))]
    public partial class subscriber
    {

    }

    [Bind(Exclude = "sub_id")]
    public class SubscriberModel
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string sub_name { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage="Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string sub_email { get; set; }

    }

}