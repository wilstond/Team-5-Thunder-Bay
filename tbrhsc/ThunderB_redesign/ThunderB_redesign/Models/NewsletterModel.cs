using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Models
{
    public class NewsletterModel
    {

        [DisplayName("Newsletter Subject")]
        [Required(ErrorMessage="Please enter the subject")]
        public string subject {get; set; }

        [DisplayName("Newsletter Content")]
        [Required(ErrorMessage = "Please enter valid content for the newsletter")]
        public string message { get; set; }

    }
}