using Facebook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
        // GET: Payment
        public ActionResult payment()
        {
            return View();
        }


        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var flights = db.Flight.Include(f => f.flightAttendant).Include(f => f.flightBoard).Include(f => f.plane);
            Flight fl = null;
            foreach (Flight flight in flights)
                if (flight.flightId == id)
                {
                    fl = flight;
                }

            if (fl == null)
            {
                return HttpNotFound();
            }
            return View(fl);
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
                facebook(flight.from,flight.to);
                return RedirectToAction("Index");
            }

            ViewBag.flightAttendantID = new SelectList(db.FlightAttendant, "flightAttendantID", "name", flight.flightAttendantID);
            ViewBag.flightBoardID = new SelectList(db.FlightBoard, "flightBoardID", "boardName", flight.flightBoardID);
            ViewBag.planeID = new SelectList(db.Plane, "planeID", "company", flight.planeID);

            return View(flight);
        }
        public void facebook(String from ,String to )
        {
            dynamic messagePost = new ExpandoObject();
            messagePost.message = "Ladies and Gentlemen New Flight upload to our site from" + from +"to" + to + "Hurry up to sign up";
            
            string acccessToken = "EAAJqxzH0lIkBADIZCUcNoXJNv3dluvX7QHbilPOAfLfts0yoyWSpI6EihmFvPXRrgy5T0PExsxlZBT4WvKjGdkw6WlMXQbkpjbE4jtUMGrYEaNA5CDD3nhh08tjLYX5dkdii5pkOZBWMiIDMc0qHehA1Q4mdURsILECSeoNVTnGa4x0xdVxpmw5Iv6FZBgsTNJHfbpEVbAZDZD";
            FacebookClient appp = new FacebookClient(acccessToken); try
            {
                var postId = appp.Post("116014096899657" + "/feed", messagePost);
            }
            catch (FacebookOAuthException ex)
            { //handle oauth exception } catch (FacebookApiException ex) { //handle facebook exception
            }
            
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

        public ActionResult Search(string from, string to, String flightId)
        {
            List<Flight> flights = new List<Flight>();
            if ((from == null || from == "") || (to == null || to == "") || (flightId == null || flightId == ""))
                return RedirectToAction("Index", "FlightAttendant");
            else if ((from == null || from == "") && (to == null || to == "") && (flightId == null || flightId == ""))
                return RedirectToAction("Index", "FlightAttendant");
            else
            {
                 int intOFlightId = Int32.Parse(flightId);
                foreach (Flight c in db.Flight)
                {
                    if ((c.from == from) && ( c.to == to) && (c.flightNumber == intOFlightId))
                    {
                        flights.Add(c);
                    }
                }
                return View(flights);
            }
        }
        
        public JsonResult sendEmail(string email)
        {
            bool result = false;
            result = sendMail(email, "Your order has been received by the system", "<p> Hi Igor <br /> Your order has been received by the system !! <br /> Regards easyFlights team !</p>");
            return Json(result, JsonRequestBehavior.AllowGet);
           
        }

        public bool sendMail(string toEmail,string subject,string body)
        {
            try
            {
                string senderEmail = "easypeasyflights@gmail.com";
                string senderPass = "abcdefG1";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail,senderPass);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, body);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
