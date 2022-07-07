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
    public class CourseController : Controller
    {
        private readonly ICourseMediator _courseMediator;

        public CourseController(ICourseMediator courseMediator) 
        {
            _courseMediator = courseMediator;
        }

        /// <summary>
        /// Retrieve list of courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCourses()
        {
            var _listCourses = _courseMediator.GetCourses();

            return Json(new { success = true, rows = _listCourses }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update course record from Grid
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public string EditGrid(Course Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Course _course = new Course
                    {
                        CourseID = Model.CourseID,
                        CourseName = Model.CourseName,
                        CourseDescription = Model.CourseDescription,
                    };

                    Course _response = _courseMediator.UpdateCourse(_course);
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
        /// Create course record from Grid
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create([Bind(Exclude = "CourseId")] Course Model)
        {
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _response = _courseMediator.CreateCourse(Model);

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
        /// Delete course record from Grid
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
                    _courseMediator.UpdateCourse(new Course { SelectedRow = id });
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
