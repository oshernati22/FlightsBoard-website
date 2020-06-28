using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Flight
    {
        public int flightId { get; set; }
        public int flightNumber { get; set; }
        public String from { get; set; }
        public String to { get; set; }
        public double price { get; set; }
        public int planeID { get; set; }
        public Plane plane { get; set; }
        public int flightAttendantID { get; set; }
        public FlightAttendant flightAttendant { get; set; }
        public int flightBoardID { get; set; }
        public FlightBoard flightBoard { get; set; }


    }
}