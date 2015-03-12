using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestDynamicMenu.Models
{
    [MetadataType(typeof(menuModel))]
    public partial class menu_category {

    }
    
    [Bind(Exclude="id")]
    public class menuModel
    {

        public string link_name { get; set; }

        public string link_url { get; set; }

    }
}