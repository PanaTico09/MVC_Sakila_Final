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
    public class StoreController : Controller
    {
        private sakilaEntities1 db = new sakilaEntities1();

        // GET: Store
        public ActionResult Index()
        {
            var store = db.store.Include(s => s.staff1);
            return View(store.ToList());
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            ViewBag.manager_staff_id = new SelectList(db.staff, "staff_id", "first_name");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_id,manager_staff_id,address_id,last_update")] store store)
        {
            if (ModelState.IsValid)
            {
                db.store.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.manager_staff_id = new SelectList(db.staff, "staff_id", "first_name", store.manager_staff_id);
            return View(store);
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            ViewBag.manager_staff_id = new SelectList(db.staff, "staff_id", "first_name", store.manager_staff_id);
            return View(store);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_id,manager_staff_id,address_id,last_update")] store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manager_staff_id = new SelectList(db.staff, "staff_id", "first_name", store.manager_staff_id);
            return View(store);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            store store = db.store.Find(id);
            db.store.Remove(store);
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
