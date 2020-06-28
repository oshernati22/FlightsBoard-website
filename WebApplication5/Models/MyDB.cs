using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MyDB : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Plane> Plane { get; set; }

        public DbSet<FlightBoard> FlightBoard { get; set; }

        public DbSet<FlightAttendant> FlightAttendant  { get; set; }

        public DbSet<Flight> Flight { get; set; }
    }
}