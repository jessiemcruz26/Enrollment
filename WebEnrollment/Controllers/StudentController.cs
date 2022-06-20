using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models = WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;
using CommonService.Service;

namespace WebApplication6.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string studentNumber)
        {
            if (studentNumber == null)
                return View();

            var context = new EnrollmentEntities();
            var _student = context.Students.Where(x => x.StudentNumber == studentNumber).FirstOrDefault();

            if (_student == null)
                return View();
            else
            {
                var student = new Models.Student
                {
                    StudentNumber = _student.StudentNumber,
                    Program = _student.Program,
                    Level = _student.Level,
                    FirstName = _student.FirstName,
                    LastName = _student.LastName,
                    Birthday = _student.Birthday?.ToString(),
                    Email = _student.Email,
                    Mobile = _student.Mobile,
                    Address = _student.Address,
                };

                return RedirectToAction("Edit", "Student", new { id = 1 });
            }
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(string studentNumber)
        {
            var context = new EnrollmentEntities();
            var student = new Models.Student
            {
                CourseListItems = new List<SelectListItem>()
            };
            if (string.IsNullOrEmpty(studentNumber))
                return View(new Models.Student() { CourseListItems = new List<SelectListItem>() });

            var _student = context.Students.Where(x => x.StudentNumber == studentNumber).FirstOrDefault();

            var _courseList = context.Courses.ToList();

            var courses = context.Courses.ToList();
            var bb = GetSelectListItems(courses);

            student = new Models.Student
            {
                StudentNumber = _student.StudentNumber,
                Program = _student.Program,
                Level = _student.Level,
                FirstName = _student.FirstName,
                LastName = _student.LastName,
                Birthday = _student.Birthday.HasValue ? _student.Birthday.Value.ToShortDateString() : null,
                Email = _student.Email,
                Mobile = _student.Mobile,
                Address = _student.Address,
                CourseListItems = bb,
                CourseID = "1"
            };

            return Json(new { row = student }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Course> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.CourseID.ToString(),
                    Text = element.CourseName
                });
            }
            return selectList;
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {

                var context = new EnrollmentEntities();
                string sa = form["StudentNumber"];
                var _student = context.Students.Where(x => x.StudentNumber == sa).FirstOrDefault();
                _student.FirstName = form["FirstName"];
                _student.LastName = form["LastName"];
                _student.Email = form["Email"];
                _student.Mobile = form["Mobile"];
                _student.Address = form["Address"];
                _student.Birthday = (DateTime?)Convert.ToDateTime(form["Birthday"]);
                
                context.SaveChanges();
                var student = new Models.Student
                {
                    StudentNumber = _student.StudentNumber,
                    Program = _student.Program,
                    Level = _student.Level,
                    FirstName = _student.FirstName,
                    LastName = _student.LastName,
                    Birthday = _student.Birthday?.ToString(),
                    Email = _student.Email,
                    Mobile = _student.Mobile,
                    Address = _student.Address,
                };
                return View(student);
            }
            catch(Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        public string Save(Student Model)
        {
            EnrollmentEntities db = new EnrollmentEntities();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(Model).State = EntityState.Modified;
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
