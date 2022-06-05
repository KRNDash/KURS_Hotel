using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hostel_MVC.Models;

namespace Hostel_MVC.Controllers
{
    public class BookingController : Controller
    {
        private HostelEntities db = new HostelEntities();

        //GET: Booking
        //public ActionResult Index()
        //{
        //    var booking = db.Booking.Include(b => b.Resident).Include(b => b.Room);
        //    return View(booking.ToList());
        //}

        public ActionResult Index( int pg = 1)
        {
            List<Booking> bookings = db.Booking.ToList();

            const int pageSize = 20;
            if (pg < 1)
                pg = 1;

            int rescCount = bookings.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = bookings.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);

        }


        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname");
            ViewBag.Room_roomNum = new SelectList(db.Room, "roomNum", "roomStatus");
            return View();
        }

        // POST: Booking/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idBooking,Resident_passport,Room_roomNum,count,dateIn,dateOut")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Booking.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname", booking.Resident_passport);
            ViewBag.Room_roomNum = new SelectList(db.Room, "roomNum", "roomStatus", booking.Room_roomNum);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname", booking.Resident_passport);
            ViewBag.Room_roomNum = new SelectList(db.Room, "roomNum", "roomStatus", booking.Room_roomNum);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBooking,Resident_passport,Room_roomNum,count,dateIn,dateOut")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Resident_passport = new SelectList(db.Resident, "passport", "surname", booking.Resident_passport);
            ViewBag.Room_roomNum = new SelectList(db.Room, "roomNum", "roomStatus", booking.Room_roomNum);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Booking.Find(id);
            db.Booking.Remove(booking);
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
