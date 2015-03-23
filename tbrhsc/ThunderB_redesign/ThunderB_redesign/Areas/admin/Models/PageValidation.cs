using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ThunderB_redesign.Areas.admin.Models
{
    [MetadataType(typeof(PageValidation))]
    public partial class page
    {

    }

    [Bind(Exclude = "page_id")]
    public class PageValidation
    {
        [DisplayName("Page Title")]
        [Required]
        public string page_title { get; set; } //property names have to match column names

        [DisplayName("Page Content")]
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public MvcHtmlString page_content { get; set; }

        [DisplayName("Page Created")]
        [Required]
        public DateTime page_created { get; set; }

        [DisplayName("Menu")]
        [Required]
        public int menu_id { get; set; }
    }
}