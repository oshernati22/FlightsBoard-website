using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class User
    {
        public int userID { get; set; }
        [DisplayName("I'm Admin")]
        public Boolean type { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        public String name { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public String password { get; set; }
        [DisplayName("confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(password), ErrorMessage = "The Password didn't match.")]
        public String confirmpassword { get; set; }
        public Dictionary<string, int> mapOFlightboards;
        public String bestFlightBoard { get; set; }

    }
}