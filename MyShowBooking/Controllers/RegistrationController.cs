using MyShowBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShowBooking.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public ActionResult StateRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StateRegistration(MovieRegistration movie)
        {
            MovieInformation movieinfo = new MovieInformation();
            movieinfo.StateRegistration(movie);
            return View("Success");
        }

        [HttpGet]
        public ActionResult TheaterRegistration()
        {
            MovieInformation movieinfo = new MovieInformation();
            MovieRegistration movie = new MovieRegistration();
            var state = movieinfo.StateNameList().ToList();
            var city = movieinfo.CityNameList().ToList();
            var area = movieinfo.AreaNameList().ToList();

            foreach (var item in state)
            {
                movie.StateNameList.Add(item);
            }
            foreach (var item in city)
            {
                movie.CityNameList.Add(item);
            }
            foreach (var item in area)
            {
                movie.AreaNameList.Add(item);
            }

            return View(movie);
        }


        [HttpPost]
        public ActionResult TheaterRegistration(MovieRegistration movie, int StateNameList, int CityNameList, int AreaNameList)
        {
            var theater = movie.Name;
            MovieInformation movieinfo = new MovieInformation();
            movieinfo.TheaterRegistration(theater, CityNameList, StateNameList, AreaNameList);
            return View();
        }

        [HttpGet]
         public ActionResult TheaterExistence()
        {
            MovieInformation movieinfo = new MovieInformation();
            MovieRegistration movie = new MovieRegistration();
            var state = movieinfo.StateNameList().ToList();
            var city = movieinfo.CityNameList().ToList();
            var area = movieinfo.AreaNameList().ToList();

            foreach (var item in state)
            {
                movie.StateNameList.Add(item);
            }
            foreach (var item in city)
            {
                movie.CityNameList.Add(item);
            }
            foreach (var item in area)
            {
                movie.AreaNameList.Add(item);
            }
            return View(movie);
        }

        [HttpPost]
        public ActionResult TheaterExistence(MovieRegistration movie,int StateNameList,int CityNameList,int AreaNameList)
        {
            MovieInformation movieinfo = new MovieInformation();
            var theater = movie.Name;
            var state = movieinfo.StateName(StateNameList);
            var city = movieinfo.CityName(CityNameList);
            var area = movieinfo.AreaName(AreaNameList);
            var existTheater= movieinfo.IsTheaterExist(theater,StateNameList,CityNameList,AreaNameList);
            if(existTheater==true)
            {
                Session["StateName"] = state;
                Session["CityName"] = city;
                Session["AreaName"] = area;
                Session["Name"] = theater;
                return View("MoviesRegistration", movie);
            }
            else
            {
                ViewBag.Message = "You are not registered";
                return View();
            }
        }

        

        [HttpGet]
        public ActionResult MoviesRegistration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult MoviesRegistration(MovieRegistration movie)
        {

            MovieInformation movieinfo = new MovieInformation();
            
            //movieinfo.StateRegistration(movie);
            var state=(string)Session["StateName"];
            var city = (string)Session["CityName"];
            var area = (string)Session["AreaName"];
            var theatername = (string)Session["Name"];
             movieinfo.MovieRegistration(theatername,city,area,state,movie);
            return View("MovieRegistrationDetails");

        }



    }
}