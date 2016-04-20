using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShowBooking.Models
{
    public class MovieImages
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public List<Movie> MovieList = new List<Movie>();
    }
}