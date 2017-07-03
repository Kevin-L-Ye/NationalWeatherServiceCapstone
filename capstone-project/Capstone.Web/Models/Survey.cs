using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class Survey
    {

        public int SurveyId { get; set; }
        [Required (ErrorMessage = "Please Select A Park.")]
        public string ParkCode { get; set; }
        // [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required (ErrorMessage = "A State Is Required.")]
        public string State { get; set; }
        public string ActivityLevel { get; set; }

        public List<SelectListItem> ValidParkCodes { get; set; }
    }
}