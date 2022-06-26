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
            //if (string.IsNullOrEmpty(id))
            //{
            //    var _CourseListItems = new List<SelectListItem>();

            //    _CourseListItems.Add(new SelectListItem() { Text = "Northern Cape", Value = "NC" });
            //    _CourseListItems.Add(new SelectListItem() { Text = "Free State", Value = "FS" });
            //    _CourseListItems.Add(new SelectListItem() { Text = "Western Cape", Value = "WC" });

            //    return View(new model.Course() { CourseListItems = _CourseListItems });
            //}

            model.Course course = _courseMediator.GetCourse(Convert.ToInt32(id));

            return Json(new { row = course }, JsonRequestBehavior.AllowGet);
        }

        public string EditGrid(Course Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    model.Course _course = new model.Course
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

        public string Delete(string Id)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _courseMediator.UpdateCourse(new model.Course { ID = Id });
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
                model.Course _course = new model.Course
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
