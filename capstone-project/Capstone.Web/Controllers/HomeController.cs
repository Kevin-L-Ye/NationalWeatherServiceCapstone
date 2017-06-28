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
        public HomeController(INationalParkDAL parkDAL)
        {
            this.parkDAL = parkDAL;
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
    }
}