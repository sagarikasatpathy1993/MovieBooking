using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShowBooking.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int MovieID { get; set; }
        public DateTime Date { get; set; }
       // public DateTime ShowTime { get; set; }

    }
}