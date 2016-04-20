using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyShowBooking.Models
{
    public class TheaterDetails
    {
        public int TheaterID { get; set; }
        public int AreaID { get; set; }
        public string Name { get; set; }
       // public string Address { get; set; }
        //public string City { get; set; }
      
       // public List<Cinema> CityList = new List<Cinema>();
        //public SelectList AreaList { get; set; }

    }
}