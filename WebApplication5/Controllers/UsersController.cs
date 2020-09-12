using Facebook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class UsersController : Controller
    {
        private MyDB db = new MyDB();

        public object MessageBox { get; private set; }
        public object MessageBoxButtons { get; private set; }
        public object Alert { get; private set; }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,type,name,password,confirmpassword,bestFlightBoard")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                TempData["Message"] = "Success";
                return RedirectToAction("Create","Users");
            }
            TempData["Message"] = "Error";
            return RedirectToAction("Create", "Users");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,type,name,password,bestFlightBoard")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string name)
        {
            List<User> users = new List<User>();
            if (name == null || name == "")
                return RedirectToAction("Index", "Users");
            else
            {
                foreach (User c in db.User)
                {
                    if (c.name == name)
                    {
                        users.Add(c);
                    }
                }
                return View(users);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult setLoginDetails(String name,String password,String isAdmin)
        {
            Session["flights"] = db.Flight.Count();
            Session["users"] = db.User.Count();
            Session["companies"] = db.FlightBoard.Count();
            Session["messages"] = db.contact.Count();
            Boolean flag=false;
            if (isAdmin == "on")
                flag = true;
           

            if (db.User.Where(user => user.name == name && user.password == password && user.type == flag).FirstOrDefault() == null) /*https://www.youtube.com/watch?v=aWX77vN1e3U */
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Index", "Home");
            }
            else if (flag == true)
            {
                Session["name"] = name + " Admin";
                return RedirectToAction("Index", "FlightBoards");
            }
            else if (flag == false)
            {
                Session["User"] = db.User.Where(user => user.name == name && user.password == password && user.type == flag).FirstOrDefault() ;
                string temp = (Session["User"] as User).bestFlightBoard;
                Session["best"] =temp ;
                Session["name"] = name;
               
                return RedirectToAction("Index", "FlightBoards");
            }

            
            return View();

        }
        public ActionResult logOut()
        {
            string best;
            if ((Session["User"] as User).mapOFlightboards != null)
            {
                 best = (Session["User"] as User).mapOFlightboards.FirstOrDefault(x => x.Value == (Session["User"] as User).mapOFlightboards.Values.Max()).Key;
            }
            else {
               best = "ELAL";
            }

            (Session["User"] as User).bestFlightBoard = best;
            this.Edit((Session["User"] as User));
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
       



    }
}
