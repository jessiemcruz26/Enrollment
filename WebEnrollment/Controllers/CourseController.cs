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

        [HttpGet]
        public ActionResult GetCourses()
        {
            var _listCourses = _courseMediator.GetCourses();

            return Json(new { rows = _listCourses }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Course course = _courseMediator.GetCourse(Convert.ToInt32(id));

            return Json(new { row = course }, JsonRequestBehavior.AllowGet);
        }

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

                    _courseMediator.UpdateCourse(_course);
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
        public string Create([Bind(Exclude = "CourseId")] Course Model)
        {
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _course = _courseMediator.CreateCourse(Model);

                    if (_course.ValidationErrors.Any())
                    {
                        foreach (var item in _course.ValidationErrors)
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
                    _courseMediator.UpdateCourse(new Course { SelectedRow = selectedRow });
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

        //Edit fields
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                Course _course = new Course
                {
                    CourseID = Convert.ToInt32(form["CourseID"]),
                    CourseName = form["CourseName"],
                    CourseDescription = form["CourseDescription"]
                };

                var response = _courseMediator.UpdateCourse(_course);

                return View(response);
            }
            catch
            {
                return View();
            }
        }
    }
}
