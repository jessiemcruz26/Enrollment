using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model = WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;
using CommonService.Service;
using contract = CommonService.Contracts;
using WebEnrollment.Mediator;

namespace WebApplication6.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentMediator _studentMediator;

        public StudentController(IStudentMediator studentMediator) 
        {
            _studentMediator = studentMediator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStudents()
        {
            var _listStudents = _studentMediator.GetStudents();

            return Json(new { rows = _listStudents }, JsonRequestBehavior.AllowGet);
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
                var student = new model.Student
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
            if (string.IsNullOrEmpty(studentNumber))
            {
                var _CourseListItems = new List<SelectListItem>();

                _CourseListItems.Add(new SelectListItem() { Text = "Northern Cape", Value = "NC" });
                _CourseListItems.Add(new SelectListItem() { Text = "Free State", Value = "FS" });
                _CourseListItems.Add(new SelectListItem() { Text = "Western Cape", Value = "WC" });

                return View(new model.Student() { CourseListItems = _CourseListItems });
            }

            model.Student student = _studentMediator.GetStudent(studentNumber);

            return Json(new { row = student }, JsonRequestBehavior.AllowGet);
        }

        

        //public string Edit(Student Model)
        //{
        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            model.Student _student = new model.Student
        //            {
        //                Address = Model.Address,
        //                FirstName = Model.FirstName,
        //                LastName = Model.LastName,
        //                Mobile = Model.Mobile,
        //                Email = Model.Email,
        //                Level = Model.Level,
        //                Program = Model.Program,
        //                StudentNumber = Model.StudentNumber
        //            };
        //            if (string.IsNullOrEmpty(Model.StudentNumberstudentNumber))
        //            {
        //                var _CourseListItems = new List<SelectListItem>();

        //                _CourseListItems.Add(new SelectListItem() { Text = "Northern Cape", Value = "NC" });
        //                _CourseListItems.Add(new SelectListItem() { Text = "Free State", Value = "FS" });
        //                _CourseListItems.Add(new SelectListItem() { Text = "Western Cape", Value = "WC" });


        //                return View(new model.Student() { CourseListItems = _CourseListItems });
        //            }

        //            _studentMediator.UpdateStudent(_student, null);
        //            msg = "Saved Successfully";
        //        }
        //        else
        //        {
        //            msg = "Validation data not successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Error occured:" + ex.Message;
        //    }
        //    return msg;
        //}

        public string Delete(string Id)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _studentMediator.UpdateStudent(new model.Student(), Id);
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

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                model.Student student = new model.Student
                {
                    StudentNumber = form["StudentNumber"],
                    FirstName = form["FirstName"],
                    LastName = form["LastName"],
                    Email = form["Email"],
                    Mobile = form["Mobile"],
                    Address = form["Address"],
                    Birthday = form["Birthday"],
                };

                var response = _studentMediator.UpdateStudent(student, string.Empty);

                return View(response);
            }
            catch
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
