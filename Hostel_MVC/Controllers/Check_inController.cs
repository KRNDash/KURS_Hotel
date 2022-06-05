using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hostel_MVC;

namespace Hostel_MVC.Controllers
{
    public class Check_inController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: Check_in
        public ActionResult Index()
        {
            var check_in = db.Check_in.Include(c => c.Booking).Include(c => c.Resident);
            return View(check_in.ToList());
        }

        // GET: Check_in/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_in check_in = db.Check_in.Find(id);
            if (check_in == null)
            {
                return HttpNotFound();
            }
            return View(check_in);
        }

        // GET: Check_in/Create
        public ActionResult Create()
        {
            ViewBag.Booking_idBooking = new SelectList(db.Booking, "idBooking", "Resident_passport");
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname");
            return View();
        }

        // POST: Check_in/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCheck_in,Booking_idBooking,Resident_passport")] Check_in check_in)
        {
            if (ModelState.IsValid)
            {
                db.Check_in.Add(check_in);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Booking_idBooking = new SelectList(db.Booking, "idBooking", "Resident_passport", check_in.Booking_idBooking);
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname", check_in.Resident_passport);
            return View(check_in);
        }

        // GET: Check_in/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_in check_in = db.Check_in.Find(id);
            if (check_in == null)
            {
                return HttpNotFound();
            }
            ViewBag.Booking_idBooking = new SelectList(db.Booking, "idBooking", "Resident_passport", check_in.Booking_idBooking);
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname", check_in.Resident_passport);
            return View(check_in);
        }

        // POST: Check_in/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCheck_in,Booking_idBooking,Resident_passport")] Check_in check_in)
        {
            if (ModelState.IsValid)
            {
                db.Entry(check_in).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Booking_idBooking = new SelectList(db.Booking, "idBooking", "Resident_passport", check_in.Booking_idBooking);
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname", check_in.Resident_passport);
            return View(check_in);
        }

        // GET: Check_in/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_in check_in = db.Check_in.Find(id);
            if (check_in == null)
            {
                return HttpNotFound();
            }
            return View(check_in);
        }

        // POST: Check_in/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Check_in check_in = db.Check_in.Find(id);
            db.Check_in.Remove(check_in);
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
