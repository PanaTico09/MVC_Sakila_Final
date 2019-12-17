using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sakila;

namespace MVCScaffolding.Controllers
{
    public class RentalController : Controller
    {
        private sakilaEntities1 db = new sakilaEntities1();

        // GET: Rental
        public ActionResult Index()
        {
            var rental = db.rental.Include(r => r.staff);
            return View(rental.ToList());
        }

        // GET: Rental/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rental rental = db.rental.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rental/Create
        public ActionResult Create()
        {
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rental_id,rental_date,inventory_id,customer_id,return_date,staff_id,last_update")] rental rental)
        {
            if (ModelState.IsValid)
            {
                db.rental.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name", rental.staff_id);
            return View(rental);
        }

        // GET: Rental/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rental rental = db.rental.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name", rental.staff_id);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rental_id,rental_date,inventory_id,customer_id,return_date,staff_id,last_update")] rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name", rental.staff_id);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rental rental = db.rental.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rental rental = db.rental.Find(id);
            db.rental.Remove(rental);
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
