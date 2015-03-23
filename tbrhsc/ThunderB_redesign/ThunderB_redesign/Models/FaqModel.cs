using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{

    [MetadataType(typeof(FaqModel))]
    public partial class faq
    {

    }

    [Bind(Exclude = "id")]
    public class FaqModel
    {
        [DisplayName("Question")]
        [Required(ErrorMessage = "Please enter a question")]
        public string question { get; set; }
        [DisplayName("Answer")]
        [Required(ErrorMessage = "Please enter an answer")]
        public string answer { get; set; }
        [DisplayName("Category")]
        public string category { get; set; }
    }
}