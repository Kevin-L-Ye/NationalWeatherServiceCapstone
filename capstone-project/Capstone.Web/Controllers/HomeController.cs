﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private INationalParkDAL parkDAL;
        private ISurveyDAL surveyDAL;
        public HomeController(INationalParkDAL parkDAL, ISurveyDAL surveyDAL)
        {
            this.parkDAL = parkDAL;
            this.surveyDAL = surveyDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult HomePage()
        {
            List<NationalPark> parks = parkDAL.GetAllParks();
            return View("HomePage", parks);
        }
        [HttpGet]
        public ActionResult ParkDetail()
        {
            List<NationalPark> parks = parkDAL.GetAllParks();
            NationalPark model = parks.FirstOrDefault(p => p.ParkCode == code);
            model.ValidParkCodes = ConvertListToSelectList(parks);

            string unitOfTemperature = GetUnitOfTemperature();


            return View("ParkDetail", model);
        }

        [HttpPost]
        public ActionResult ParkDetail(NationalPark park)
        {
            Session["unitOfTemperature"] = park.UnitOfTemperature;
            return SurveyView("ParkDetail", park);
        }

        public string GetUnitOfTemperature()
        {
            string tempScale = Session["unitOfTemperature"];
            if (tempScale == null)
            {
                tempScale = "Fahrenheit";
                Session["unitOfTemperature"] = tempScale;
            }

            return tempScale;
        }



        public ActionResult SurveyView()
        {
            List<NationalPark> parks = parkDAL.GetAllParks();
            Survey model = new Survey();
            model.ValidParkCodes = ConvertListToSelectList(parks);

            return View("SurveyView", model);
        }

        [HttpPost]
        public ActionResult SurveyView(Survey newSurvey)
        {
            surveyDAL.SaveSurvey(newSurvey);
            return RedirectToAction("FavoriteParks");
        }

        public List<SelectListItem> ConvertListToSelectList(List<NationalPark> parkCodes)
        {
            List<SelectListItem> dropdownlist = new List<SelectListItem>();

            foreach (NationalPark n in parkCodes)
            {
                SelectListItem choice = new SelectListItem() { Text = n.ParkName, Value = n.ParkCode };
                dropdownlist.Add(choice);
            }

            return dropdownlist;
        }

        public ActionResult FavoriteParks()
        {
            Dictionary<string, string> favorites = parkDAL.GetFavoriteParks();
            return View("FavoriteParks", favorites);
        }

        public string 
    }
}