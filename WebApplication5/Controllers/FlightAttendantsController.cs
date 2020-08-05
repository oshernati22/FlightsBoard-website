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
    public class FlightAttendantsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: FlightAttendants
        public ActionResult Index()
        {
            return View(db.FlightAttendant.ToList());
        }

        // GET: FlightAttendants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightAttendant flightAttendant = db.FlightAttendant.Find(id);
            if (flightAttendant == null)
            {
                return HttpNotFound();
            }
            return View(flightAttendant);
        }

        // GET: FlightAttendants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightAttendants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "flightAttendantID,name")] FlightAttendant flightAttendant)
        {
            if (ModelState.IsValid)
            {
                db.FlightAttendant.Add(flightAttendant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flightAttendant);
        }

        // GET: FlightAttendants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightAttendant flightAttendant = db.FlightAttendant.Find(id);
            if (flightAttendant == null)
            {
                return HttpNotFound();
            }
            return View(flightAttendant);
        }

        // POST: FlightAttendants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "flightAttendantID,name")] FlightAttendant flightAttendant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightAttendant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flightAttendant);
        }

        // GET: FlightAttendants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightAttendant flightAttendant = db.FlightAttendant.Find(id);
            if (flightAttendant == null)
            {
                return HttpNotFound();
            }
            return View(flightAttendant);
        }

        // POST: FlightAttendants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightAttendant flightAttendant = db.FlightAttendant.Find(id);
            db.FlightAttendant.Remove(flightAttendant);
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

        public ActionResult Search(string name)
        {
            List<FlightAttendant> flightsatt = new List<FlightAttendant>();
            if (name == null || name == "")
                return RedirectToAction("Index", "FlightAttendant");
            else
            {
                foreach (FlightAttendant c in db.FlightAttendant)
                {
                    if (c.name == name)
                    {
                        flightsatt.Add(c);
                    }
                }
                return View(flightsatt);
            }
        }
    }
}
