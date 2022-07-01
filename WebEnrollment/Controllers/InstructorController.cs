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

namespace WebEnrollment.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorMediator _instructorMediator;

        public InstructorController(IInstructorMediator instructorMediator)
        {
            _instructorMediator = instructorMediator;
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


        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                string _instructorNumberSearch = form["InstructorNumberSearch"];

                if (!string.IsNullOrEmpty(_instructorNumberSearch))
                {
                    Instructor instructor = _instructorMediator.GetInstructor(_instructorNumberSearch);
                    return View(instructor);
                }

                Instructor _instructor = new Instructor
                {
                    InstructorID = form["InstructorID"],
                    FirstName = form["FirstName"],
                    LastName = form["LastName"],
                    Email = form["Email"],
                    Mobile = form["Mobile"],
                    InstructorNumber = form["InstructorNumber"]
                };

                var response = _instructorMediator.UpdateInstructor(_instructor);

                return View(response);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(string instructorNumber)
        {
            if (string.IsNullOrEmpty(instructorNumber))
            {
                //var _programListItems = new List<SelectListItem>();
                //_programListItems.Add(new SelectListItem() { Text = "Electronics", Value = "Electronics", Selected = true });
                //_programListItems.Add(new SelectListItem() { Text = "Civil", Value = "Civil" });
                //_programListItems.Add(new SelectListItem() { Text = "Mechanical", Value = "Mechanical" });

                return View(new Instructor() { });
            }

            Instructor _instructor = _instructorMediator.GetInstructor(instructorNumber);

            return Json(new { row = _instructor }, JsonRequestBehavior.AllowGet);
        }

        public string EditGrid(Instructor Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Instructor _instructor = new Instructor
                    {
                        InstructorID = Model.InstructorID.ToString(),
                        FirstName = Model.FirstName,
                        LastName = Model.LastName,
                        Mobile = Model.Mobile,
                        Email = Model.Email,
                        InstructorNumber = Model.InstructorNumber,
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

        public string Delete(string selectedRow)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _instructorMediator.UpdateInstructor(new Instructor { SelectedRow = selectedRow });
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
