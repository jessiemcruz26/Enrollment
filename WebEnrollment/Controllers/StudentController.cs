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
using WebEnrollment.Common;

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


        /// <summary>
        /// Get list of students for Grid
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Update student record in Grid
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create student record in Grid
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete student record in Grid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edit landing page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit()
        {
            return View(InitializeStudentPrograms());
        }

        /// <summary>
        /// Search Student Number
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            try
            {
                string studentNumberSearch = form["StudentNumberSearch"];

                Student _student = _studentMediator.GetStudent(studentNumberSearch);

                if (!_student.IsStudentFound)
                {
                    var _emptyStudent = InitializeStudentPrograms(studentNumberSearch);
                    _emptyStudent.IsStudentFound = false;

                    return View("Edit", _emptyStudent);
                }

                return View("Edit", _student);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error : " + e.Message);

                return View("Edit", InitializeStudentPrograms());
            }
        }

        /// <summary>
        /// Update student record
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
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

                return View(InitializeStudentPrograms());
            }
        }

        #endregion

        /// <summary>
        /// Initialized student programs
        /// </summary>
        /// <param name="_entry"></param>
        /// <returns></returns>
        private static Student InitializeStudentPrograms(string _entry = null)
        {
            var _programs = EnrollmentHelper.GetStudentPrograms();
            var _programListItems = new List<SelectListItem>();

            foreach (var program in _programs)
            {
                _programListItems.Add(new SelectListItem() { Text = program, Value = program });
            }

            return new Student {StudentNumberSearch = _entry, 
                ProgramListItems = _programListItems, 
                AssociatedClasses = new List<Class>(), 
                UnassociatedClasses = new List<Class>() };
        }
    }
}
