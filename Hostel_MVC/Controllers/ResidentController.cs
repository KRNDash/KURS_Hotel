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
    public class ResidentController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: Resident
        public ActionResult Index()
        {
            return View(db.Resident.ToList());
        }

        // GET: Resident/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resident resident = db.Resident.Find(id);
            if (resident == null)
            {
                return HttpNotFound();
            }
            return View(resident);
        }

        // GET: Resident/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resident/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "passport,surname,name,patronymic")] Resident resident)
        {
            if (ModelState.IsValid)
            {
                db.Resident.Add(resident);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resident);
        }

        // GET: Resident/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resident resident = db.Resident.Find(id);
            if (resident == null)
            {
                return HttpNotFound();
            }
            return View(resident);
        }

        // POST: Resident/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "passport,surname,name,patronymic")] Resident resident)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resident);
        }

        // GET: Resident/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resident resident = db.Resident.Find(id);
            if (resident == null)
            {
                return HttpNotFound();
            }
            return View(resident);
        }

        // POST: Resident/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Resident resident = db.Resident.Find(id);
            db.Resident.Remove(resident);
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
