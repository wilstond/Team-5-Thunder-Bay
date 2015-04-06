using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(PageModel))]
    public partial class page
    {

    }

    [Bind(Exclude = "page_id")]
    public class PageModel
    {
        [DisplayName("User Id")]
        [Required]
        public int user_id { get; set; }

        [DisplayName("Page Title")]
        [Required]
        public string page_title { get; set; } //property names have to match column names

        [DisplayName("Page Content")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public MvcHtmlString page_content { get; set; }

        [DisplayName("Page Created")]
        [Required]
        public DateTime page_created { get; set; }

        [DisplayName("Menu")]
        public int menu_id { get; set; }

        [DisplayName("Published or Draft")]
        [Required]
        public char page_visibility { get; set; }

        [Key]
        [Required]
        [DisplayName("Page Slug (Friendly Url)")]
        [Remote("IsSlugAvailable", "PageAdmin", HttpMethod = "POST", ErrorMessage = "Slug Already Exist.")]
        public string page_slug { get; set; }

        [DisplayName("Meta Title")]
        public string meta_title { get; set; }

        [DisplayName("Meta Description")]
        public string meta_desc { get; set; }

    }
}