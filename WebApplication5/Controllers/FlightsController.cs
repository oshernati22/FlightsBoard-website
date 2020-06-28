using System;
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
    public class FlightsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Flights
        public ActionResult Index()
        {
            var flight = db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane);
            return View(flight.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            ViewBag.flightAttendantID = new SelectList(db.FlightAttendant, "flightAttendantID", "name");
            ViewBag.flightBoardID = new SelectList(db.FlightBoard, "flightBoardID", "boardName");
            ViewBag.planeID = new SelectList(db.Plane, "planeID", "company");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "flightId,flightNumber,from,to,price,planeID,flightAttendantID,flightBoardID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flight.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.flightAttendantID = new SelectList(db.FlightAttendant, "flightAttendantID", "name", flight.flightAttendantID);
            ViewBag.flightBoardID = new SelectList(db.FlightBoard, "flightBoardID", "boardName", flight.flightBoardID);
            ViewBag.planeID = new SelectList(db.Plane, "planeID", "company", flight.planeID);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.flightAttendantID = new SelectList(db.FlightAttendant, "flightAttendantID", "name", flight.flightAttendantID);
            ViewBag.flightBoardID = new SelectList(db.FlightBoard, "flightBoardID", "boardName", flight.flightBoardID);
            ViewBag.planeID = new SelectList(db.Plane, "planeID", "company", flight.planeID);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "flightId,flightNumber,from,to,price,planeID,flightAttendantID,flightBoardID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.flightAttendantID = new SelectList(db.FlightAttendant, "flightAttendantID", "name", flight.flightAttendantID);
            ViewBag.flightBoardID = new SelectList(db.FlightBoard, "flightBoardID", "boardName", flight.flightBoardID);
            ViewBag.planeID = new SelectList(db.Plane, "planeID", "company", flight.planeID);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flight.Find(id);
            db.Flight.Remove(flight);
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
    }
}
