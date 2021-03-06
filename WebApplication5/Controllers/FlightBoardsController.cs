﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class FlightBoardsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: FlightBoards
        public ActionResult Index()
        {
            return View(db.FlightBoard.ToList());
        }

        // GET: FlightBoards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBoard flightBoard = db.FlightBoard.Find(id);
            if (flightBoard == null)
            {
                return HttpNotFound();
            }
            return View(flightBoard);
        }

        // GET: FlightBoards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "flightBoardID,boardName,flightID")] FlightBoard flightBoard)
        {
            if (ModelState.IsValid)
            {
                db.FlightBoard.Add(flightBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flightBoard);
        }

        // GET: FlightBoards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBoard flightBoard = db.FlightBoard.Find(id);
            if (flightBoard == null)
            {
                return HttpNotFound();
            }
            return View(flightBoard);
        }

        // POST: FlightBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "flightBoardID,boardName,flightID")] FlightBoard flightBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flightBoard);
        }

        // GET: FlightBoards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBoard flightBoard = db.FlightBoard.Find(id);
            if (flightBoard == null)
            {
                return HttpNotFound();
            }
            return View(flightBoard);
        }

        // POST: FlightBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightBoard flightBoard = db.FlightBoard.Find(id);
            db.FlightBoard.Remove(flightBoard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string boardname)
        {
            List<FlightBoard> flightsboard = new List<FlightBoard>();
            if (boardname == null || boardname == "")
                return RedirectToAction("Index", "FlightBoards");
            else
            {
                foreach (FlightBoard c in db.FlightBoard)
                {
                    if (c.boardName == boardname)
                    {
                        flightsboard.Add(c);
                    }
                }
                return View(flightsboard);
            }
        }

        public ActionResult displayFlights(String names) // display the flights of the chosen flightboards of the client
        {
            List<String> boardnames = names.Split(',').ToList();
            List<Flight> flights = new List<Flight>();
            foreach (String boardname in boardnames)
            {
                foreach (Flight c in db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane))
                {
                    if (c.flightBoardID == Int32.Parse(boardname))
                        flights.Add(c);
                }
            }
            return View(flights);

        }
        public ActionResult displayFlight(String name) // display the flightboard flights of the suggestion functio
        {

            List<Flight> flights = new List<Flight>();
            foreach (Flight c in db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane))
            {
                if (c.flightBoard.boardName == name)
                    flights.Add(c);
            }

            return View(flights);

        }

        //public ActionResult SearchFlights(string from, string to, String flightId, String boardName)
        // {
        //     List<Flight> flights = new List<Flight>();
        //     if ((from == null || from == "") && (to == null || to == "") && (flightId == null || flightId == ""))
        //         return RedirectToAction("Index", "Flighs");
        //     else if (from == null || from == "")
        //     {
        //         foreach (Flight flight in db.Flight)
        //             //if ((to == flight.to || to == null || to == "") && (flightId == null || flightId == "" || Int32.Parse(flightId) == flight.flightId ))
        //             //{
        //             //    flights.Add(flight);
        //             //}
        //             if (to != null && to != "" && flightId != null && flightId != "")
        //             {
        //                 if (to == flight.to && Int32.Parse(flightId) == flight.flightId)
        //                     flights.Add(flight);

        //             }
        //             else if ((to == null || to == "") && flightId != null && flightId != "")
        //             {
        //                 if (Int32.Parse(flightId) == flight.flightId)
        //                     flights.Add(flight);

        //             }
        //             else if ((flightId == null || flightId == "") && to != null && to != "")
        //             {
        //                 if (to == flight.to)
        //                     flights.Add(flight);

        //             }

        //     }
        //     else if ((to == null || to == "") && (from != "") && (from != null))
        //     {
        //         foreach (Flight flight in db.Flight)
        //             if (flightId != null && flightId != "")
        //             {
        //                 if (from == flight.from && Int32.Parse(flightId) == flight.flightId)
        //                     flights.Add(flight);

        //             }

        //             else if (flightId == null || flightId == "")
        //             {
        //                 if (from == flight.from)
        //                     flights.Add(flight);

        //             }

        //     }
        //     else if ((flightId == null || flightId == "") && (from != "") && (from != null) && (to != "") && (to != null))
        //     {
        //         foreach (Flight flight in db.Flight)
        //             if ((from == flight.from) && (to == flight.to))
        //             {
        //                 flights.Add(flight);
        //             }

        //     }
        //     if (flights.Count > 0)
        //         return View(flights);
        //     else return RedirectToAction("Index", "Flighs");
        // }

        public ActionResult SearchFlights(string from, string to, String flightId, String boardName) //search
        {
            List<Flight> flights = new List<Flight>();
            if ((from == null || from == "") || (to == null || to == "") || (flightId == null || flightId == ""))
                return RedirectToAction("Index", "FlightAttendant");
            else if ((from == null || from == "") && (to == null || to == "") && (flightId == null || flightId == ""))
                return RedirectToAction("Index", "FlightAttendant");
            else
            {
                int intOFlightId = Int32.Parse(flightId);
                foreach (Flight c in db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane))
                {
                    if ((c.from == from) && (c.to == to) && (c.flightNumber == intOFlightId))
                    {
                        flights.Add(c);
                    }
                }
                return View(flights);
            }
        }
        public ActionResult SearchByPrice(int price) //return flights under the price of the client
        {
            List<Flight> flights = new List<Flight>();
            foreach (Flight flight in db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane))
            {
                if (flight.price < price || flight.price == price)
                    flights.Add(flight);
            }

            if (flights.Count > 0)
                return View(flights);
            else return RedirectToAction("Index", "Flighs");

        }


        public ActionResult groupBy()
        {
            var flightByID =
               from u in db.Flight
               group u by u.flightBoard into g

               select new { flightBoard = g.Key, count = g.Count(), g.FirstOrDefault().flightBoardID };
            var group = new List<Flight>();
            foreach (var t in flightByID)
            {
                group.Add(new Flight()
                {
                    flightBoard = t.flightBoard,
                    flightNumber = t.count
                });
            }

            ViewBag.boardnameELAL = group[0].flightBoard.boardName;
            ViewBag.sizeELAL = group[0].flightNumber;
            ViewBag.boardnameBRITISH = group[1].flightBoard.boardName;
            ViewBag.sizeBRITISH = group[1].flightNumber;
            ViewBag.boardnameAVIANCA = group[2].flightBoard.boardName;
            ViewBag.sizeAVIANCA = group[2].flightNumber;

            return View(group);

        }

        public ActionResult joinFlight()
        {

            var join = (
            from u in db.Flight
            join p in db.Flight on u.flightBoardID equals p.flightAttendantID

            select new { u.flightBoardID, u.flightAttendantID, p.flightBoard, u.flightNumber, u.flightAttendant }).Distinct();

            var UserList = new List<Flight>();
            foreach (var t in join)
            {
                UserList.Add(new Flight()
                {
                    flightBoard = t.flightBoard,
                    flightNumber = t.flightNumber,
                    flightAttendant = t.flightAttendant,

                });
            }
            return View(UserList);
        }

        public ActionResult joinPlane()
        {

            var join = (
            from u in db.Flight
            join p in db.Flight on u.flightBoardID equals p.planeID

            select new { u.flightBoardID, u.planeID, p.flightBoard, u.flightNumber, u.plane }).Distinct();

            var UserList = new List<Flight>();
            foreach (var t in join)
            {
                UserList.Add(new Flight()
                {
                    flightBoard = t.flightBoard,
                    flightNumber = t.flightNumber,
                    plane = t.plane,

                });
            }
            return View(UserList);
        }


        public ActionResult BestFilghts()
        {
            List<Flight> flights = new List<Flight>();
            Flight[] flightsTemp = db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane).ToArray();
            List<Flight> temp = flightsTemp.ToList();
            Flight[] q = temp.OrderBy(Flight => Flight.price).ToArray();
            for (int i = 0; i < 10; i++)
            {
                flights.Add(q[i]);
            }
            return View(flights);
        }


    }


   
}






