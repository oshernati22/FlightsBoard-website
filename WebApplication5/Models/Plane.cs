using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Plane
    {
       public int planeID { get; set; }
       public String company { get; set; }
       public virtual ICollection<Flight> flights { get; set; }
    }
}