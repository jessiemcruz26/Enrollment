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

        [HttpGet]
        public ActionResult Edit(string studentNumber)
        {
            if (string.IsNullOrEmpty(studentNumber))
            {
                var _programListItems = new List<SelectListItem>();
                _programListItems.Add(new SelectListItem() { Text = "Electronics", Value = "Electronics", Selected = true });
                _programListItems.Add(new SelectListItem() { Text = "Civil", Value = "Civil" });
                _programListItems.Add(new SelectListItem() { Text = "Mechanical", Value = "Mechanical" });

                return View(new model.Student() { ProgramListItems = _programListItems });
            }

            model.Student student = _studentMediator.GetStudent(studentNumber);

            return Json(new { row = student }, JsonRequestBehavior.AllowGet);
        }

        public string EditGrid(Student Model)
        {
            string msg;
            try
            {
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

                    _studentMediator.UpdateStudent(_student);
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
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _student = _studentMediator.CreateStudent(Model);

                    if (_student.ValidationErrors.Any())
                    {
                        foreach (var item in _student.ValidationErrors)
                        {
                            msg = msg + Environment.NewLine + item.Code + " : " + item.Message;
                        }
                    }
                    else
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
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _studentMediator.UpdateStudent(new model.Student { ID = Id });
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

        //Edit fields
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
                    Program = form["Program"],
                };

                var response = _studentMediator.UpdateStudent(student);

                return View(response);
            }
            catch
            {
                return View();
            }
        }
    }
}
