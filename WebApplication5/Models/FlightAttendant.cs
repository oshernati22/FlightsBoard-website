using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class FlightAttendant
    {
        public int flightAttendantID { get; set; }
        public String name { get; set; }
        public virtual ICollection<Flight> flightList { get; set; }

    }
}