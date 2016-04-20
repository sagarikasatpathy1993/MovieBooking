using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShowBooking.Models
{
    public class SeatType
    {
        public int SeatTypeID { get; set; }
        public string SeatName { get; set; }
        public int NumberOfSeats { get; set; }
        public int Price { get; set; }
        public int ScreenID { get; set; }
    }
}