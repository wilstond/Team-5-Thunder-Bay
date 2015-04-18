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
        //---------user id------------

        [DisplayName("User Id")]
        [Required]
        public int user_id { get; set; }

        //----------page title------------

        [DisplayName("Page Title")]
        [Required]
        public string page_title { get; set; } //property names have to match column names

        //------------page content---------------

        [DisplayName("Page Content")]
        [AllowHtml]
        public MvcHtmlString page_content { get; set; }

        //------------page created date-----------

        [DisplayName("Page Created")]
        [Required]
        public DateTime page_created { get; set; }

        //-----------menu id ----------------------

        [DisplayName("Menu")]
        public int menu_id { get; set; }

        //------------page visibility----------------

        [DisplayName("Published or Draft")]
        [Required]
        public char page_visibility { get; set; }

        //------------page slug-------------------------
        //validated to allow only numbers letters or hyphens
        //also validated to prevent duplicate slugs using json

        [Key]
        [Required]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug can only contain letters, numbers or hyphens. No spaces or special charaters.")]
        [DisplayName("Page Slug (Friendly Url)")]
        [Remote("IsSlugAvailable", "PageAdmin", HttpMethod = "POST", ErrorMessage = "Slug Already Exist.")]
        public string page_slug { get; set; }

        //------------meta title-------------------------

        [DisplayName("Meta Title")]
        public string meta_title { get; set; }

        //------------meta description-------------------

        [DisplayName("Meta Description")]
        public string meta_desc { get; set; }

    }
}