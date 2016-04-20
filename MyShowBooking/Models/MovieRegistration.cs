using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShowBooking.Models
{
    public class MovieRegistration
    {
        public int TheaterID { get; set; }
        [Required]
        [Display(Name="Theater Name")]
        public string Name { get; set; }

        public int CityID { get; set; }
        [Required]
        [Display(Name="City")]
        public string CityName { get; set; }

        public int AreaID { get; set; }
        [Required]
        [Display(Name="Area")]
        public string AreaName { get; set; }

        public int StateID { get; set; }
        [Required]
        [Display(Name="State")]
        public string StateName { get; set; }

        

        public int ScreenID { get; set; }
        [Required]
        [Display(Name="Screen Name")]
        public string ScreenName { get; set; }
        [Required]
        [Display(Name="Movie Name")]
        public string MovieName { get; set; }
        [Required]
        [Display(Name="Seat Name")]
        public string SeatName { get; set; }
        [Required]
        [Display(Name="Total Number Of Seats")]
        public int NumberOfSeats { get; set; }
        public int Price { get; set; }


        public int MovieID { get; set; }

        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }

         [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int ScheduleID { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name="Start Time")]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name="End Time")]
        public DateTime EndTime { get; set; }

        public string FirstName { get; set; }
        public int UserID { get; set; }


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