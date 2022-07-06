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

        #region Grid

        [HttpPost]
        public string Create([Bind(Exclude = "InstructorId")] Instructor Model)
        {
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _response = _instructorMediator.CreateInstructor(Model);

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

        [HttpGet]
        public ActionResult GetInstructors()
        {
            try
            {
                var _listInstructors = _instructorMediator.GetInstructors();

                return Json(new { success = true, rows = _listInstructors }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
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

                    Instructor _response = _instructorMediator.UpdateInstructor(_instructor);

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
                    _instructorMediator.UpdateInstructor(new Instructor { SelectedRow = id });
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


        #region Instructor

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            try
            {
                string _instructorNumberSearch = form["InstructorNumberSearch"];

                Instructor instructor = _instructorMediator.GetInstructor(_instructorNumberSearch);

                return View("Edit", instructor);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error : " + e.Message);

                return View("Edit", new Instructor());
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
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
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error : " + e.Message);

                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View(new Instructor());
        }

        #endregion

    }
}
