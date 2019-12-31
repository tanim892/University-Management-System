using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BitmUniversityApp.Models;
using BitmUniversityApp.Context;

namespace BitmUniversityApp.Controllers
{
    public class CourseController : Controller
    {
        private BitmuniversityWebAppContext db = new BitmuniversityWebAppContext();

        // GET: /Course/
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }

        // GET: /Course/Details/5
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

        // GET: /Course/Create
        public ActionResult Create()
        {
            if (TempData["Massage"] != null)
            {
                ViewBag.Message = TempData["Massage"];
                ViewBag.MessageType = TempData["MassageType"];
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "SemesterName");
            return View();
        }

        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseCode,CourseName,CourseCredit,CourseDescription,CourseAssignTo,CourseStatus,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                if (await db.SaveChangesAsync() > 0)
                {
                    TempData["Massage"] = string.Format("Course <b> {0} </b> is Save Successfully", course.CourseName);
                    TempData["MassageType"] = "success";
                }
               // db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "SemesterName", course.SemesterId);
            return View(course);
        }

        // GET: /Course/Edit/5
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
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "SemesterName", course.SemesterId);
            return View(course);
        }

        // POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CourseCode,CourseName,CourseCredit,CourseDescription,CourseAssignTo,CourseStatus,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "SemesterName", course.SemesterId);
            return View(course);
        }





        // GET: /Course/Delete/5
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

        // POST: /Course/Delete/5
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




/*start */
        public JsonResult IsUniqueCode(string CourseCode)
        {
            return Json(!db.Courses.Any(m => m.CourseCode == CourseCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUniqueName(string CourseName)
        {
            return Json(!db.Courses.Any(m => m.CourseName == CourseName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnassignCourses()
        {
            return View();
        }


        public JsonResult UnassignAllCourses(bool decision)
        {
            var courses = db.Courses.Where(m => m.CourseStatus == true).ToList();
            if (courses.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var course in courses)
                {
                    course.CourseStatus = false;
                    course.CourseAssignTo = "";
                    db.Courses.AddOrUpdate(course);
                    db.SaveChanges();
                }
                return Json(true);

            }
        }

      /*We Also Can use this*/
        //public JsonResult IsCourseCodeExist(Course course)
        //{
        //    bool isExist = GetAllCourses().Where(u => u.Code.ToLowerInvariant().Equals(course.Code.ToLower())).FirstOrDefault() != null;
        //    return Json(!isExist, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult IsCourseNameExist(Course course)
        //{
        //    bool isExist = GetAllCourses().Where(u => u.Name.ToLowerInvariant().Equals(course.Name.ToLower())).FirstOrDefault() != null;
        //    return Json(!isExist, JsonRequestBehavior.AllowGet);
        //}


    }
}
