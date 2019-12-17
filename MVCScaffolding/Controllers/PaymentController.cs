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
    public class PaymentController : Controller
    {
        private sakilaEntities1 db = new sakilaEntities1();

        // GET: Payment
        public ActionResult Index(string sortOrder)
        {
            ViewBag.CustSortParm = String.IsNullOrEmpty(sortOrder) ? "customer_id" : "";
            ViewBag.AmountSortParm = String.IsNullOrEmpty(sortOrder) ? "amount" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "payment_date" : "Date";
            ViewBag.LastUpdateSortParm = sortOrder == "Date" ? "last_update": "Date";
            ViewBag.RentalIDSortParm = String.IsNullOrEmpty(sortOrder) ? "rental_id" : "";
            ViewBag.ID = String.IsNullOrEmpty(sortOrder) ? "payment_id" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name" : "";


            var payment = db.payment.Include(p => p.rental).Include(p => p.staff);

            switch (sortOrder)
            {
                case "customer_id":
                    payment = payment.OrderBy(p => p.customer_id);
                    break;
                case "amount":
                    payment = payment.OrderBy(p => p.amount);
                    break;
                case "payment_date":
                    payment = payment.OrderBy(p => p.payment_date);
                    break;
                case "last_update":
                    payment = payment.OrderBy(p => p.last_update);
                    break;
                case "rental_id":
                    payment = payment.OrderBy(p => p.rental_id);
                    break;
                case "first_name":
                    payment = payment.OrderBy(p => p.staff.first_name);
                    break;
                default:
                    payment = payment.OrderBy(p => p.payment_id);
                    break;
            }
            return View(payment.ToList());
        }

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            ViewBag.rental_id = new SelectList(db.rental, "rental_id", "rental_id");
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payment_id,customer_id,staff_id,rental_id,amount,payment_date,last_update")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.payment.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rental_id = new SelectList(db.rental, "rental_id", "rental_id", payment.rental_id);
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name", payment.staff_id);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.rental_id = new SelectList(db.rental, "rental_id", "rental_id", payment.rental_id);
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name", payment.staff_id);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payment_id,customer_id,staff_id,rental_id,amount,payment_date,last_update")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rental_id = new SelectList(db.rental, "rental_id", "rental_id", payment.rental_id);
            ViewBag.staff_id = new SelectList(db.staff, "staff_id", "first_name", payment.staff_id);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payment payment = db.payment.Find(id);
            db.payment.Remove(payment);
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
