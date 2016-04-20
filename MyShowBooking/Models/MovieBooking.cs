using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShowBooking.Models
{
    public class MovieBooking
    {

        public string Name { get; set; }
        public string Area { get; set; }
        public int TheaterID { get; set; }
        public string City { get; set; }



        public int StateID { get; set; }
        public string StateName { get; set; }

        public int MovieID { get; set; }
        public string MovieName { get; set; }

        public string ScheduleID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime ShowTime { get; set; }
        

        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }


        public int SeatTypeID { get; set; }
        public string SeatName { get; set; }

       
        public int Price { get; set; }

        public int Quantity { get; set; }

        public List<City> CityNameList = new List<City>();
        public List<Area> AreaNameList = new List<Area>();
        public List<Movie> MovieNameList = new List<Movie>();
        public List<Schedule> DateList = new List<Schedule>();
        public List<State> StateNameList = new List<State>();
        public List<TheaterDetails> TheaterList = new List<TheaterDetails>();
        public List<ShowTime> TimeList = new List<ShowTime>();
        public List<SeatType> SeatList = new List<SeatType>();
        
       

       
    }
}