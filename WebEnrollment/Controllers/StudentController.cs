using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEnrollment.Models;
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

        /// <summary>
        /// First load
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string studentNumber)
        {

            if (string.IsNullOrEmpty(studentNumber))
            {
                var _programListItems = new List<SelectListItem>();
                _programListItems.Add(new SelectListItem() { Text = "Electronics", Value = "Electronics", Selected = true });
                _programListItems.Add(new SelectListItem() { Text = "Civil", Value = "Civil" });
                _programListItems.Add(new SelectListItem() { Text = "Mechanical", Value = "Mechanical" });

                return View(new Student() { ProgramListItems = _programListItems, AssociatedClasses = new List<Class>(), UnassociatedClasses = new List<Class>() });
            }

            Student student = _studentMediator.GetStudent(studentNumber);

            return View(student);
        }

        public string EditGrid(Student Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Student _student = new Student
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

        public string Delete(string selectedRow)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _studentMediator.UpdateStudent(new Student { SelectedRow = selectedRow });
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
                string studentNumberSearch = form["StudentNumberSearch"];

                if (!string.IsNullOrEmpty(studentNumberSearch))
                {

                    Student _student = _studentMediator.GetStudent(studentNumberSearch);
                    return View(_student);
                }
              
                string[] _removeClassIds = string.IsNullOrEmpty(form["removeClassIds"]) ? new string[0] : form["removeClassIds"].Split(',');

                Student student = new Student
                {
                    StudentNumber = form["StudentNumber"],
                    FirstName = form["FirstName"],
                    LastName = form["LastName"],
                    Email = form["Email"],
                    Mobile = form["Mobile"],
                    Address = form["Address"],
                    Birthday = form["Birthday"],
                    Program = form["Program"],
                    StudentID = form["StudentID"],
                    ClassSelection = form["classSelection"],
                    ClassIds = _removeClassIds
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
