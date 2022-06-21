﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEnrollment.Mediator;
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
            return View();
        }

        //public ActionResult Save(FormCollection form)
        //{
        //    return View("Index");
        //}

        public ActionResult GetStudents()
        {
            var _listStudents = _studentMediator.GetStudents();

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

        public string Edit(Student Model)
        {
            EnrollmentEntities db = new EnrollmentEntities();
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

        //public ActionResult About()
        //{
        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}