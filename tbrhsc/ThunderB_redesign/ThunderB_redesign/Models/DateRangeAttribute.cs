using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Models
{
    public class DateRangeAttribute : ValidationAttribute
    {

//---Custom validation attribute built using tutorial below
//---http://www.codeproject.com/Articles/260177/Custom-Validation-Attribute-in-ASP-NET-MVC

        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            DateTime discharge = (DateTime)value;
            if (discharge < DateTime.Now)
            {
                string errorMessage = "Discharge could not accept past date/time. If case was discharged in the past, use Discharge button";
                return new ValidationResult(errorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}