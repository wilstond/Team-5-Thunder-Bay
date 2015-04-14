﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
    [MetadataType(typeof(newsValidation))]
    public partial class newsTable
    {

    }

    [Bind(Exclude="Id")]
    public partial class newsValidation
    {
        [DisplayName("News Story")]
        [Required]
        public string stories { get; set; }

        [DisplayName("News Headline")]
        [Required]
        public string headline { get; set; }

        [DisplayName("Date")]
        public string date { get; set; }

        [DisplayName("Author")]
        public string author { get; set; }
    }
}