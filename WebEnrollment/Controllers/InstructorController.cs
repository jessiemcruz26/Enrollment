﻿using System;
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

namespace WebEnrollment.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorMediator _instructorMediator;

        public InstructorController(IInstructorMediator instructorMediator)
        {
            _instructorMediator = instructorMediator;
        }

        // GET: Instructor
        public ActionResult Index()
        {
            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        public string Create([Bind(Exclude = "InstructorId")] Instructor Model)
        {
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _student = _instructorMediator.CreateInstructor(Model);

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

        [HttpGet]
        public ActionResult GetInstructors()
        {
            var _listInstructors = _instructorMediator.GetInstructors();

            return Json(new { rows = _listInstructors }, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult Edit(string firstName, string lastName)
        //{
        //    if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
        //        return View(new model.Instructor() { InstructorListItems = new List<SelectListItem>() });

        //    model.Student student = _studentMediator.GetStudent(studentNumber);

        //    return Json(new { row = student }, JsonRequestBehavior.AllowGet);
        //}


        //public string Edit(Student Model)
        //{
        //    string msg;
        //    try
        //    {
        //        msg = "Validation data not successfully";
        //        //if (ModelState.IsValid)
        //        //{
        //        //    model.Instructor _instructor = new model.Instructor
        //        //    {
        //        //        InstructorID = Model.InstructorID,
        //        //        FirstName = Model.FirstName,
        //        //        LastName = Model.LastName,
        //        //        Mobile = Model.Mobile,
        //        //        Email = Model.Email,
        //        //    };

        //        //    _instructorMediator.GetInstructor(_instructor.InstructorID);
        //        //    msg = "Saved Successfully";
        //        //}
        //        //else
        //        //{
        //        //    msg = "Validation data not successfully";
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Error occured:" + ex.Message;
        //    }
        //    return msg;
        //}

        public string EditGrid(Instructor Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    model.Instructor _instructor = new model.Instructor
                    {
                       InstructorID = Model.InstructorID,
                        FirstName = Model.FirstName,
                        LastName = Model.LastName,
                        Mobile = Model.Mobile,
                        Email = Model.Email,
                    };

                    _instructorMediator.UpdateInstructor(_instructor);
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

        //public string EditGrid(Instructor Model)
        //{
        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            model.Instructor _instructor = new model.Instructor
        //            {
        //                InstructorID = Model.InstructorID,
        //                FirstName = Model.FirstName,
        //                LastName = Model.LastName,
        //                Mobile = Model.Mobile,
        //                Email = Model.Email
        //            };

        //            _instructorMediator.UpdateInstructor(_instructor);
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
                    _instructorMediator.UpdateInstructor(new model.Instructor { ID = Id });
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
