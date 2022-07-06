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

namespace WebApplication.Controllers
{
    [HandleError]
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

        #region Grid

        [HttpGet]
        public ActionResult GetStudents()
        {
            try
            {
                var _listStudents = _studentMediator.GetStudents();

                return Json(new { rows = _listStudents, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public string EditGrid(Student Model)
        {
            string msg = "";
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

                    Student _response = _studentMediator.UpdateStudentGrid(_student);

                    if (_response.ValidationErrors.Any())
                    {
                        msg = "Data not saved. \n";
                        foreach (var item in _response.ValidationErrors)
                        {
                            msg += item.Message + "\n";
                        }
                    }
                    else 
                    {
                        msg = "Saved Successfully";
                    }
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
                    var _response = _studentMediator.CreateStudent(Model);

                    if (_response.ValidationErrors.Any())
                    {
                        msg = "Data not saved. \n";
                        foreach (var item in _response.ValidationErrors)
                        {
                            msg += item.Message + "\n";
                        }
                    }
                    else
                    {
                        msg = "Saved Successfully";
                    }
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

        public string Delete(string id)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _studentMediator.UpdateStudentGrid(new Student { SelectedRow = id });
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

        #endregion

        #region Student

        [HttpGet]
        public ActionResult Edit()
        {
            return View(InitializeStudent());
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            try
            {
                string studentNumberSearch = form["StudentNumberSearch"];

                Student _student = _studentMediator.GetStudent(studentNumberSearch);

                if (!_student.IsStudentFound)
                {
                    var _emptyStudent = InitializeStudent(studentNumberSearch);
                    _emptyStudent.IsStudentFound = false;

                    return View("Edit", _emptyStudent);
                }

                return View("Edit", _student);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error : " + e.Message);

                return View("Edit", InitializeStudent());
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
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

                var _response = _studentMediator.UpdateStudent(student);

                return View(_response);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error : " + e.Message);

                return View(InitializeStudent());
            }
        }

        #endregion

        private static Student InitializeStudent(string _entry = null)
        {
            var _programListItems = new List<SelectListItem>();
            _programListItems.Add(new SelectListItem() { Text = "Electronics", Value = "Electronics", Selected = true });
            _programListItems.Add(new SelectListItem() { Text = "Civil", Value = "Civil" });
            _programListItems.Add(new SelectListItem() { Text = "Mechanical", Value = "Mechanical" });

            return new Student {StudentNumberSearch = _entry, 
                ProgramListItems = _programListItems, 
                AssociatedClasses = new List<Class>(), 
                UnassociatedClasses = new List<Class>() };
        }
    }
}
