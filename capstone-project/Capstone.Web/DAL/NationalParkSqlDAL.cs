﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class NationalParkSqlDAL : INationalParkDAL
    {
        private string connectionString;
        private IWeatherDAL weatherDAL;
        private ISurveyDAL surveyDAL;

        private string SQL_GetAllParks = @"SELECT * FROM park;";
        private string SQL_GetParkCodes = "SELECT parkCode FROM park GROUP BY parkCode;";
        private string SQL_GetFavoriteParks = @"SELECT park.parkCode, park.parkName, COUNT(*) AS survey_count FROM park INNER JOIN survey_result ON park.parkCode = survey_result.parkCode GROUP BY park.parkName, park.parkCode ORDER BY survey_count DESC, park.parkName ASC;";

        public NationalParkSqlDAL (string connectionString, IWeatherDAL weatherDAL, ISurveyDAL surveyDAL)
        {
            this.connectionString = connectionString;
            this.weatherDAL = weatherDAL;
            this.surveyDAL = surveyDAL;
        }

        public List<NationalPark> GetAllParks()
        {
            List<NationalPark> parks = new List<NationalPark>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllParks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        parks.Add(PopulateParkObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return parks;
        }

        public List<string> GetParkCodes()
        {
            List<string> validParks = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetParkCodes, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        validParks.Add(Convert.ToString(reader["parkCode"]));
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return validParks;
        }

        public NationalPark PopulateParkObject(SqlDataReader reader)
        {
            List<Weather> weather = weatherDAL.GetWeather(Convert.ToString(reader["parkCode"]));

            return new NationalPark()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                State = Convert.ToString(reader["state"]),
                Acreage = Convert.ToInt32(reader["acreage"]),
                ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]),
                NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                Climate = Convert.ToString(reader["climate"]),
                YearFounded = Convert.ToInt32(reader["yearFounded"]),
                AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                ParkDescription = Convert.ToString(reader["parkDescription"]),
                EntryFee = Convert.ToInt32(reader["entryFee"]),
                NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]),
                NextFiveDayWeather = weather
            };
        }

        public Dictionary<List<string>, int> GetFavoriteParks()
        {
            Dictionary<List<string>, int> favorites = new Dictionary<List<string>, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetFavoriteParks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        List<string> parkInfo = new List<string>();
                        string code = Convert.ToString(reader["parkCode"]);
                        string name = Convert.ToString(reader["parkName"]);
                        int surveyCount = Convert.ToInt32(reader["survey_count"]);
                        parkInfo.Add(name);
                        parkInfo.Add(code);
                        favorites.Add(parkInfo, surveyCount);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }



            return (favorites);
        }

    }
}