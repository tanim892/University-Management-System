﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitmUniversityApp.Models;
using BitmUniversityApp.Context;

namespace BitmUniversityApp.Controllers
{
    public class EnrollCourseController : Controller
    {
        private BitmuniversityWebAppContext db = new BitmuniversityWebAppContext();

        // GET: /EnrollCourse/
        public ActionResult Index()
        {
            var enrollcourse = db.EnrollCourse.Include(e => e.Course);
            return View(enrollcourse.ToList());
        }

        // GET: /EnrollCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollcourse = db.EnrollCourse.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollcourse);
        }

        // GET: /EnrollCourse/Create
        public ActionResult Create()
        {
            ViewBag.StudentList = db.Students.ToList();
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode");
            return View();
        }

        // POST: /EnrollCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,RegistrationNo,CourseId,EnrollDate,CourseGrade")] EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                db.EnrollCourse.Add(enrollcourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        // GET: /EnrollCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollcourse = db.EnrollCourse.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        // POST: /EnrollCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RegistrationNo,CourseId,EnrollDate,CourseGrade")] EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollcourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        // GET: /EnrollCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollcourse = db.EnrollCourse.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollcourse);
        }

        // POST: /EnrollCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollCourse enrollcourse = db.EnrollCourse.Find(id);
            db.EnrollCourse.Remove(enrollcourse);
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
/*Start*/


        public JsonResult GetStudentById(string studentRegNo)
        {
            var students = db.Students.Where(m => m.StudentRegNo == studentRegNo).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesbyDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.Department.Id == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAlreadyEnrolled(string regNo, int courseId)
        {
            var enrollCourses = db.EnrollCourse.Where(m => m.RegistrationNo == regNo && m.CourseId == courseId);

            if (enrollCourses.Count() == 0)
            {
                return Json(false);
            }
            return Json(true);
        }
        public JsonResult EnrollStudentToCourse(EnrollCourse enrollCourse)
        {

            var enrollCourses = db.EnrollCourse.Where(m => m.RegistrationNo == enrollCourse.RegistrationNo && m.CourseId == enrollCourse.CourseId).ToList();

            if (enrollCourses.Count() == 1)
            {
                var id = enrollCourses[0].Id;
                var date = enrollCourses[0].EnrollDate;
                enrollCourse.Id = id;
                enrollCourse.EnrollDate = date;
                db.EnrollCourse.AddOrUpdate(enrollCourse);
            }
            else
            {
                db.EnrollCourse.Add(enrollCourse);
            }

            db.SaveChanges();
            return Json(true);
        }


        public ActionResult SaveResult()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.StudentList = db.Students.ToList();
            ViewBag.GradeList = db.Grades.ToList();
            return View();
        }

        public JsonResult GetCoursesbyRegNo(string regNo)
        {
            var courses = db.EnrollCourse.Where(m => m.RegistrationNo == regNo).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewResult()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.StudentList = db.Students.ToList();
            return View();
        }



    }
}
