using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyShowBooking.Models
{
    public class UserDetails
    {
        public int UserID { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name="Mobile Number")]
        public string MobileNumber { get; set; }
        public string UserType { get; set; }
        
        
    }
}