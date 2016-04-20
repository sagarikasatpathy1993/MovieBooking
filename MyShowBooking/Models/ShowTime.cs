using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShowBooking.Models
{
    public class ShowTime
    {
        public int TimeID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}