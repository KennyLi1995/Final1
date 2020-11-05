using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalAssignment2.Models;
using Microsoft.AspNet.Identity;

namespace FinalAssignment2.Controllers
{
    public class EnrolmentsController : Controller
    {
        private FIT5032Model db = new FIT5032Model();
        private string userId;

        // GET: Enrolments
        public ActionResult Index()
        {
            userId = User.Identity.GetUserId();
            var enrolments = db.Enrolments.Include(s => s.Course).Where(s => s.UserId == userId).ToList();
            if (User.IsInRole("Admin")) {
                enrolments = db.Enrolments.ToList();
            }
            return View(enrolments);
        }

        // GET: Enrolments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment = db.Enrolments.Find(id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        public ActionResult ErrorInforDate()
        {
            return View();
        }

        public ActionResult ErrorInforConstraint()
        {
            return View();
        }

        public ActionResult ErrorInforCourseId()
        {
            return View();
        }

        // GET: Enrolments/Create
        public ActionResult Create(String Id, String date)
        {
            if (null == date)
                return RedirectToAction("Index");
            Enrolment e = new Enrolment();
            DateTime convertedDate = DateTime.Parse(date);
            e.Start = convertedDate;
            e.CourseId = int.Parse(Id);
            return View(e);
        }

        // POST: Enrolments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,Start")] Enrolment enrolment)
        {


            foreach (Enrolment enl in db.Enrolments.ToList())
            {
                var c = User.Identity.GetUserId();
                if (enl.Start == enrolment.Start && enl.UserId == c)
                {
                    return RedirectToAction("ErrorInforConstraint");
                }
            }

            Boolean courseCheck = false;
            Boolean courseDateCheck = false;
            foreach (Course cou in db.Courses.ToList()) {
                if (cou.Id == enrolment.CourseId) {
                    courseCheck = true;
                    if (cou.Date == enrolment.Start) {
                        courseDateCheck = true;
                    }
                }
            }
            if (!courseCheck) {
                return RedirectToAction("ErrorInforCourseId");
            }
            if (!courseDateCheck)
            {
                return RedirectToAction("ErrorInforDate");
            }

            enrolment.UserId = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(enrolment);


            if (ModelState.IsValid)
                {
                    db.Enrolments.Add(enrolment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            

                return View(enrolment);

        }

        

        // GET: Enrolments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment = db.Enrolments.Find(id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // POST: Enrolments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,Start")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrolment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enrolment);
        }

        // GET: Enrolments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment = db.Enrolments.Find(id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrolment enrolment = db.Enrolments.Find(id);
            db.Enrolments.Remove(enrolment);
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
