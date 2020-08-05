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




    }
}
