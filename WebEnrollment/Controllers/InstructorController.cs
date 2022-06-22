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

        // GET: Instructor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instructor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Instructor/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

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

        public string Edit(Instructor Model)
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

        public string Delete(string Id)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _instructorMediator.UpdateInstructor(new model.Instructor { InstructorID = Convert.ToInt32(Id) });
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



        // GET: Instructor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instructor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
