using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShowBooking.Models;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using System.Web.Security;
using System.IO;
namespace MyShowBooking.Controllers
{
    public class LoginController : Controller
    {


        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.UserDetails userdetails)
        {
            MovieInformation movieinfo = new MovieInformation();
            movieinfo.RegisterUser(userdetails);
            return View("Registration");

        }


        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Email"] != null)
            {
                return View("MoviesRegistration");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(Models.Login model_login)
        {
                MovieInformation movieinfo = new MovieInformation();
                Models.UserDetails user = new Models.UserDetails();
                MovieRegistration movie = new MovieRegistration();
                string val = movieinfo.checkUser(model_login);
                if (ModelState.IsValid)
                {
                    if (val != "")
                    {
                        if (val == "Provider")
                        {

                            Session["Email"] = model_login.Email;
                           
                           
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
                            return View("../Registration/TheaterExistence",movie);
                        }
                        else if(val=="User")
                        {
                            Session["Email"] = model_login.Email;
                            MovieBooking moviebook = new MovieBooking();
                            TheaterDetails cinema = new TheaterDetails();
                            var StateList = movieinfo.StateNameList().ToList();
                            foreach (var item in StateList)
                            {
                                    moviebook.StateNameList.Add(item);
                            }

                            return View("MovieBooking", moviebook);
                         }
                        else
                        {
                            Session["Email"] = model_login.Email;
                            return View("../Registration/StateRegistration", movie);
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Email and Password";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
        }

        /*[HttpGet]
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
            MovieRegistration movie=new MovieRegistration();
            var state= movieinfo.StateNameList().ToList();
           var city= movieinfo.CityNameList().ToList();
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
        public ActionResult TheaterRegistration(MovieRegistration movie,int StateNameList, int CityNameList, int AreaNameList)
        {
            var theater = movie.Name;
            MovieInformation movieinfo = new MovieInformation();
            movieinfo.TheaterRegistration(theater,CityNameList,StateNameList,AreaNameList);
            return RedirectToAction("MoviesRegistration");
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
           // movieinfo.MovieRegister(movie);
            return View("MovieRegistrationDetails");

        }*/


        public ActionResult MovieRegistrationDetails()
        {
            MovieInformation movieinfo = new MovieInformation();
           
            return View();
        }



        /*[HttpGet]
        public ActionResult UploadMovieImages()
        {
            MovieInformation movieinfo = new MovieInformation();
            //var mov=movieinfo.GetImage();
            return View();
           
           
        }
        [HttpPost]
        public ActionResult UploadMovieImages(HttpPostedFileBase file)
        {
          
            MovieImages ImageObj = new MovieImages();
            MovieInformation movieinfo = new MovieInformation();
           
            if (file.ContentLength > 0)
            {
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("file", "Please upload image of type: " + string.Join(", ", AllowedFileExtensions));
                }

                else
                {
                    MovieInformation movie = new MovieInformation();
                    MovieImages Image = new MovieImages();
                    Image.MovieID= int.Parse(Request.Form["MovieList"].ToString());
                   
                    Image.ImageName = Path.GetFileName(file.FileName);
                    Image.Url = Path.Combine(Server.MapPath("~/Pictures"), Image.ImageName);
                    file.SaveAs(Image.Url);
                    movie.insertImage(Image);
                    ViewBag.Message = "File uploaded successfully. File Name : " + Image.ImageName;
                }
            }
            return RedirectToAction("OwnerRentProperty");
        }*/

        [HttpGet]
        public ActionResult MovieBooking()
        {
            MovieBooking moviebook = new MovieBooking();
            TheaterDetails cinema = new TheaterDetails();
            MovieInformation movieinfo = new MovieInformation();
            var StateList = movieinfo.StateNameList().ToList();
            foreach (var item in StateList)
            {
                moviebook.StateNameList.Add(item);
            }
            return View(moviebook);
        }


        [HttpPost]
        public ActionResult MovieBooking(int StateNameList,string CityNameList,string MovieNameList,string DateList)
        {
            MovieBooking moviebook = new MovieBooking();
            MovieInformation movieinfo = new MovieInformation();
            var theater=movieinfo.FindTheaterName(StateNameList, CityNameList, MovieNameList, DateList);
            Session["MovieName"] = MovieNameList;
            Session["DateList"] = DateList;
            foreach(var item in theater)
            {
                moviebook.TheaterList.Add(item);
            }
            return View("TheaterDetails",moviebook);
        }

        [HttpGet]
        public ActionResult  TheaterDetails()
        {

            return View();
        }

        [HttpPost]
        public ActionResult TheaterDetails(int TheaterList,string TimeList,string SeatList,int Quantity)
        {
            MovieBooking moviebook = new MovieBooking();
            MovieInformation movieinfo = new MovieInformation();
            var movie = (string)Session["MovieName"];
            var date = (string)Session["DateList"];
            var email=(string)Session["Email"];
            var theaterName=movieinfo.GetTheaterName(TheaterList);
            var movieid=movieinfo.GetMovieID(movie);
            var scheduleid=movieinfo.GetScheduleID(date);
            var userid = movieinfo.GetUserID(email);
            var amount = movieinfo.GetPrice(TheaterList,SeatList,movieid);
            var amountToPaid = amount * Quantity;
            movieinfo.InsertIntoBooking(movieid,scheduleid,TheaterList,TimeList,SeatList,Quantity,amountToPaid,userid);
            moviebook.MovieName = movie;
            moviebook.Name = theaterName;
            moviebook.Date =Convert.ToDateTime( date);
            moviebook.ShowTime = Convert.ToDateTime(TimeList);
            moviebook.Quantity = Quantity;
            moviebook.SeatName = SeatList;
            moviebook.Price = amountToPaid;

            return View("TicketSummary",moviebook);
        }


        public ActionResult TicketSummary()
        {
            return View();
        }



        public JsonResult TimeDropDown(int TheaterList)
        {
            MovieInformation movieinfo = new MovieInformation();
            MovieBooking moviebook = new MovieBooking();
            var movie = (string)Session["MovieName"];
            var date = (string)Session["DateList"];
            var Time = movieinfo.GetTimeDropDown(TheaterList,movie,date).ToList();
            foreach(var item in Time)
            {
                moviebook.TimeList.Add(item);
            }
             return Json(moviebook);
        }


        public JsonResult SeatDropDown(string TimeList)
        {
            MovieInformation movieinfo = new MovieInformation();
            MovieBooking moviebook = new MovieBooking();
            var movie = (string)Session["MovieName"];
            var date = (string)Session["DateList"];
            var timeid = movieinfo.GetTimeID(TimeList);
            var SeatList = movieinfo.GetSeatList(timeid,movie,date).ToList();
            foreach(var item in SeatList)
            {
                moviebook.SeatList.Add(item);
            }
            return Json(moviebook);

        }

       
        public JsonResult CityDropDown(int StateNameList)
        {
            MovieInformation movieinfo = new MovieInformation();
            MovieBooking moviebook = new MovieBooking();
            var CityName = movieinfo.GetCityDropDown(StateNameList).ToList();

            foreach (var item in CityName)
            {
                moviebook.CityNameList.Add(item);
            }
            return Json(moviebook);
        }


        public JsonResult MovieDropDown(string CityNameList, MovieBooking moviebook)
        {
            MovieInformation movieinfo = new MovieInformation();
            moviebook = new MovieBooking();
            var CityList = movieinfo.GetCityID(CityNameList);
            var MovieName = movieinfo.GetMovieDropdown(CityList).ToList();
            foreach (var item in MovieName)
            {
                moviebook.MovieNameList.Add(item);
            }
            return Json(moviebook);
        }


        public JsonResult DateDropDown(string MovieNameList, MovieBooking moviebook)
        {
            MovieInformation movieinfo = new MovieInformation();
            var movieID = movieinfo.GetMovieID(MovieNameList);
            var DateList = movieinfo.GetDateDropDown(movieID).ToList();
            foreach (var item in DateList)
            {
                moviebook.DateList.Add(item);
            }
            return Json(moviebook);
        }

       
        
        public ActionResult LogOut()
        {

            Session.Abandon();
            return View("Login");
        }

    }
}