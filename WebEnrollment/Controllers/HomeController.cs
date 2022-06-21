using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEnrollment.Repository;
using model = WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;
using CommonService.Service;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStudentMediator _studentMediator;

        public HomeController(IStudentMediator studentMediator)
        {
            _studentMediator = studentMediator;
        }

        public ActionResult Index()
        {
            //var students = _business.GetStudents();

            return View();
        }

        public ActionResult Save(FormCollection form)
        {
            //var a = form["StudentNumber"];
            //_business.Save();
            return View("Index");
        }

        public ActionResult GetStudents()
        {
            var _listStudents = _studentMediator.GetStudents();
            //EnrollmentService service = new EnrollmentService();

            //var _studentRows = service.GetStudent(new CommonService.Contracts.StudentRequest());

            return Json(new { rows = _listStudents }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInstructors()
        {
            var context = new EnrollmentEntities();
            var _instructorRows = context.Instructors.ToList();

            return Json(new {rows = _instructorRows }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourses()
        {
            var context = new EnrollmentEntities();
            var _coursetRows = context.Courses.ToList();

            return Json(new { rows = _coursetRows }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string Edit(Student Model)
        {
            //model.Student student = new model.Student
            //{
            //    StudentNumber = Model.StudentNumber,
            //    FirstName = Model.FirstName,
            //    LastName = Model.LastName,
            //    Email = Model.Email,
            //    Mobile = Model.Mobile,
            //    Address = Model.Address,
            //    Birthday = Model.Birthday.ToString(),
            //};

            //_studentMediator.UpdateStudent(student);

            //string msg = "Success";

            EnrollmentEntities db = new EnrollmentEntities();
            string msg;
            try
            {
                var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();

                if (ModelState.IsValid)
                {
                    model.Student _student = new model.Student
                    {
                        Address = Model.Address,
                        FirstName = Model.FirstName,
                        LastName = Model.LastName,
                        Mobile = Model.Mobile,
                        Email = Model.Email,
                        Level = Model.Level,
                        Program = Model.Program,
                        StudentNumber = Model.StudentNumber
                    };

                    _studentMediator.UpdateStudent(_student, null);
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

        [HttpPost]
        public string Create([Bind(Exclude = "StudentId")] Student Model)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _studentMediator.CreateStudent(Model);
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

        public string Delete(string Id)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            //StudentMaster student = db.Students.Find(Id);
            //db.Students.Remove(student);
            //db.SaveChanges();
            //return "Deleted successfully";
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
    }
}