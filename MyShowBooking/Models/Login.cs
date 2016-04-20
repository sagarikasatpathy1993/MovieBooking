using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyShowBooking.Models
{
    public class Login
    {
        public int UserID { get; set; }
        [Required]
        [Display(Name="Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage="Email is not valid")]
        public string Email { get; set; }
        [Required]
        [Display(Name="Password")]
        public string Password { get; set; }
        public string UserType { get; set; }
        
       
       
      

        
    }
}