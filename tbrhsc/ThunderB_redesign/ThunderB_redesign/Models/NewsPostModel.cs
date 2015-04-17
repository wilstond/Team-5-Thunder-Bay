using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{

    [MetadataType(typeof(NewsPostModel))]
    public partial class newsTable
    {

    }

    //Validation for the News form in the CMS
    [Bind(Exclude = "Id")]
    public partial class NewsPostModel
    {
        [DisplayName("News Story")]
        [Required(ErrorMessage = "Story Required")]
        public string stories { get; set; }

        [DisplayName("News Headline")]
        [Required(ErrorMessage = "Headline Required")]
        public string headline { get; set; }

        //the date can be listed either mm/dd/yyyy or with single digits when necessary, m/d/yyyy
        [DisplayName("Date")]
        [Required(ErrorMessage = "Date Required")]
        [RegularExpression(@"^((0?[13578]|10|12)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$", ErrorMessage = "mm/dd/yyy Format Required")]
        public string date { get; set; }

        [DisplayName("Author")]
        [Required(ErrorMessage = "Required Author")]
        public string author { get; set; }
    }

}