using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public int FahrenheitToCelsius(int fTemp)
        {
            return (fTemp - 32) * (5 / 9);
        }

        public int CelsiusToFarenheit(int cTemp)
        {
            return (cTemp * (9 / 5) + 32);
        }
    }
}