using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryTracker.Models;

namespace InventoryTracker.Controllers
{
    public class DropDownsTController : Controller
    {
        private InventoryTrackerEntities db = new InventoryTrackerEntities();

        // GET: DropDownsT
        public ActionResult Index()
        {
            return View(db.DropDowns.ToList());
        }

        // GET: DropDownsT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDown dropDown = db.DropDowns.Find(id);
            if (dropDown == null)
            {
                return HttpNotFound();
            }
            return View(dropDown);
        }

        // GET: DropDownsT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DropDownsT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DropDownID,Name")] DropDown dropDown)
        {
            if (ModelState.IsValid)
            {
                db.DropDowns.Add(dropDown);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dropDown);
        }

        // GET: DropDownsT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDown dropDown = db.DropDowns.Find(id);
            if (dropDown == null)
            {
                return HttpNotFound();
            }
            return View(dropDown);
        }

        // POST: DropDownsT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DropDownID,Name")] DropDown dropDown)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dropDown).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dropDown);
        }

        // GET: DropDownsT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDown dropDown = db.DropDowns.Find(id);
            if (dropDown == null)
            {
                return HttpNotFound();
            }
            return View(dropDown);
        }

        // POST: DropDownsT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DropDown dropDown = db.DropDowns.Find(id);
            db.DropDowns.Remove(dropDown);
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
