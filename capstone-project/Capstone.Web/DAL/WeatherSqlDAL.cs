using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherDAL
    {
        private string connectionString;
        public string SQL_GetWeather = @"SELECT * FROM weather ORDER BY parkCode ASC, fiveDayForecastValue ASC;";
        public string SQL_GetWeatherForPark = "SELECT * FROM weather WHERE parkCode = @parkCode ORDER BY parkCode ASC, fiveDayForecastValue ASC;";

        public WeatherSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Weather> GetWeather()
        {
            List<Weather> weather = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetWeather, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        weather.Add(PopulateWeatherObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return weather;
        }

        public List<Weather> GetWeather(string parkCode)
        {
            List<Weather> weather = new List<Weather>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetWeatherForPark, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        weather.Add(PopulateWeatherObject(reader));
                    }
                }

                return weather;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Weather PopulateWeatherObject(SqlDataReader reader)
        {
            return new Weather()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
                Low = Convert.ToInt32(reader["low"]),
                High = Convert.ToInt32(reader["high"]),
                Forecast = Convert.ToString(reader["forecast"])
            };
        }
    }
}