using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalAssignment2.Models;

namespace FinalAssignment2.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private FIT5032Model db = new FIT5032Model();

        [Authorize]
        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.School).Include(c => c.Tutor);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5\
        [Authorize(Roles = "Admin, Tutor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        public static Course c = new Course();


        // GET: Courses/Rate/5
        [Authorize(Roles = "Admin, Tutor")]
        public ActionResult Rate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c = db.Courses.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem() { Text = "1", Value = "1", Selected = false });
            select.Add(new SelectListItem() { Text = "2", Value = "2", Selected = false });
            select.Add(new SelectListItem() { Text = "3", Value = "3", Selected = false });
            select.Add(new SelectListItem() { Text = "4", Value = "4", Selected = false });
            select.Add(new SelectListItem() { Text = "5", Value = "5", Selected = false });
            select.Add(new SelectListItem() { Text = "6", Value = "6", Selected = false });
            select.Add(new SelectListItem() { Text = "7", Value = "7", Selected = false });
            select.Add(new SelectListItem() { Text = "8", Value = "8", Selected = false });
            select.Add(new SelectListItem() { Text = "9", Value = "9", Selected = false });
            select.Add(new SelectListItem() { Text = "10", Value = "10", Selected = false });

            ViewBag.Select = select;

            return View(c);
        }

        public ActionResult Chart()
        {
            var courses = db.Courses.Include(c => c.School).Include(c => c.Tutor);
            return View(courses.ToList());
        }

        [HttpPost]
        public ActionResult Rate(FormCollection form)
        {
            string rate = form["Select"];
            int rateScore = 0;
            int.TryParse(rate, out rateScore);
            double dou = double.Parse(c.Score);
            double finalScore = (dou * c.Score_num + rateScore) / (c.Score_num + 1);
            c.Score = String.Format("{0:F}", finalScore);
            c.Score_num++;


            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(c);
        }



        // GET: Courses/Create
        [ValidateInput(true)]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()    
        {

            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            ViewBag.TutorId = new SelectList(db.Tutors, "Id", "FirstName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Score,Score_num,Date,TutorId,SchoolId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", course.SchoolId);
            ViewBag.TutorId = new SelectList(db.Tutors, "Id", "FirstName", course.TutorId);
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", course.SchoolId);
            ViewBag.TutorId = new SelectList(db.Tutors, "Id", "FirstName", course.TutorId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Score,Score_num,Date,TutorId,SchoolId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", course.SchoolId);
            ViewBag.TutorId = new SelectList(db.Tutors, "Id", "FirstName", course.TutorId);
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
