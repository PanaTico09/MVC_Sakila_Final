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
    public class StaffController : Controller
    {
        private sakilaEntities1 db = new sakilaEntities1();

        // GET: Staff
        public ActionResult Index(string sortOrder)
        {
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "staff_id" : "";
            ViewBag.FirstSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name" : "";
            ViewBag.LastSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email" : "";
            ViewBag.StoreID = String.IsNullOrEmpty(sortOrder) ? "store_id" : "";
            ViewBag.ActiveSortParm = String.IsNullOrEmpty(sortOrder) ? "active" : "";
            ViewBag.UsernameSortParm = String.IsNullOrEmpty(sortOrder) ? "username" : "";
            ViewBag.LastUpdateSortParm = sortOrder == "Date" ? "last_update" : "Date";

            var staff = db.staff.Include(p => p.store);

            switch (sortOrder)
            {
                case "last_name":
                    staff = staff.OrderBy(p => p.last_name);
                    break;
                case "email":
                    staff = staff.OrderBy(p => p.email);
                    break;
                case "store_id":
                    staff = staff.OrderBy(p => p.store_id);
                    break;
                case "active":
                    staff = staff.OrderBy(p => p.active);
                    break;
                case "username":
                    staff = staff.OrderBy(p => p.username);
                    break;
                case "last_update":
                    staff = staff.OrderBy(p => p.last_update);
                    break;
                default:
                    staff = staff.OrderBy(p => p.first_name);
                    break;
            }
            return View(staff.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff staff = db.staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            ViewBag.store_id = new SelectList(db.store, "store_id", "store_id");
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "staff_id,first_name,last_name,address_id,picture,email,store_id,active,username,password,last_update")] staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.last_update = DateTime.Now;
                db.staff.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.store_id = new SelectList(db.store, "store_id", "store_id", staff.store_id);
            return View(staff);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff staff = db.staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.store_id = new SelectList(db.store, "store_id", "store_id", staff.store_id);
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staff_id,first_name,last_name,address_id,picture,email,store_id,active,username,password,last_update")] staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.last_update = DateTime.Now;
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.store_id = new SelectList(db.store, "store_id", "store_id", staff.store_id);
            return View(staff);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff staff = db.staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            staff staff = db.staff.Find(id);
            db.staff.Remove(staff);
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
