using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkhourDemo;

namespace WorkhourDemo.Controllers
{
    public class WorkhoursController : Controller
    {
        private Demo01Entities db = new Demo01Entities();

        // GET: Workhours
        public ActionResult Index()
        {
            var workhours = db.Workhours.Include(w => w.Employee).Include(w => w.Project);
            return View(workhours.ToList());
        }

        // GET: Workhours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workhour workhour = db.Workhours.Find(id);
            if (workhour == null)
            {
                return HttpNotFound();
            }
            return View(workhour);
        }

        // GET: Workhours/Create
        public ActionResult Create()
        {
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name");
            ViewBag.Project_ID = new SelectList(db.Projects, "Project_ID", "Project_Name");
            return View();
        }

        // POST: Workhours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Workhour_ID,Employee_ID,Project_ID,Date,Hours")] Workhour workhour)
        {
            if (ModelState.IsValid)
            {
                db.Workhours.Add(workhour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", workhour.Employee_ID);
            ViewBag.Project_ID = new SelectList(db.Projects, "Project_ID", "Project_Name", workhour.Project_ID);
            return View(workhour);
        }

        // GET: Workhours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workhour workhour = db.Workhours.Find(id);
            if (workhour == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", workhour.Employee_ID);
            ViewBag.Project_ID = new SelectList(db.Projects, "Project_ID", "Project_Name", workhour.Project_ID);
            return View(workhour);
        }

        // POST: Workhours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Workhour_ID,Employee_ID,Project_ID,Date,Hours")] Workhour workhour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workhour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", workhour.Employee_ID);
            ViewBag.Project_ID = new SelectList(db.Projects, "Project_ID", "Project_Name", workhour.Project_ID);
            return View(workhour);
        }

        // GET: Workhours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workhour workhour = db.Workhours.Find(id);
            if (workhour == null)
            {
                return HttpNotFound();
            }
            return View(workhour);
        }

        // POST: Workhours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workhour workhour = db.Workhours.Find(id);
            db.Workhours.Remove(workhour);
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
