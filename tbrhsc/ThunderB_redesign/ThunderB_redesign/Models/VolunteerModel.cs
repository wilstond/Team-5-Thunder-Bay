using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThunderB_redesign.Models
{
   [MetadataType(typeof(VolunteerModel))]
    public partial class Shift
    {

    }

    [Bind(Exclude = "shift_id")]
    public class VolunteerModel
    {

        public string[] days { get; set; }
        //public List<DayOfWeek> days { get; set; }
      /* public enum shifts
        {
            morning,
            evening,
            night,
            anytime,
            none
        }
        //[DisplayName("Monday ")]
/*
        public string monday { get; set; }

        public string monday { get; set; }
        public string tuesday { get; set; }
        public string wednesday { get; set; }

        */

    }

}