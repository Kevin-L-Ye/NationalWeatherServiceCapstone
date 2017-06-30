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
            return (fTemp - 32) * 5 / 9;
        }

        public string GetWeatherAdvisory(string weatherType)
        {
            if (weatherType == "snow") return "Be sure to pack some snowshoes!";
            else if (weatherType == "rain") return "Be sure to pack some rain gear and that you're wearing some waterproof shoes!";
            else if (weatherType == "thunderstorms") return "Seek shelter and avoid hiking on exposed ridges!";
            else if (weatherType == "sun") return "Be sure to pack some sunblock!";
            else return "Enjoy your day!";
        }

        public string GetTempAdvisory(int high, int low)
        {
            if (high > 75) return "Also consider bringing an extra gallon of water!";
            else if (low < 20) return "Caution: Exposure to frigid temperatures could result in illness or injury.";
            else if ((high - low) > 20) return "Also consider wearing breathable layers!";
            else return "";
        }
    }
}