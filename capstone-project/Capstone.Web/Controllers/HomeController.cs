using System;
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
            List <NationalPark> parks = parkDAL.GetAllParks();
            return View("HomePage", parks);
        }

        public ActionResult ParkDetail(string code)
        {
            List<NationalPark> parks = parkDAL.GetAllParks();
            NationalPark model = parks.FirstOrDefault(p => p.ParkCode == code);
            model.ValidParkCodes = ConvertListToSelectList(parks);

            return View("ParkDetail", model);
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
    }
}