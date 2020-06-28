using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class FlightBoard
    {
        public int flightBoardID { get; set; }
        public String boardName { get; set; }
        public virtual ICollection<Flight> flightsList { get; set; }
    }
}