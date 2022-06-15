using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEnrollment.Repository;
using Model = WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusiness _business;

        public HomeController(IBusiness business)
        {
            _business = business;
        }

        public ActionResult Index()
        {
            var users = _business.GetUsers();
            return View();
        }

        public ActionResult Save(FormCollection form)
        {
            var a = form["StudentNumber"];
            _business.Save();
            return View("Index");
            ;
        }

        public ActionResult GetStudents()
        {
            var context = new EnrollmentEntities();
            var _studentRows = context.Students.ToList();

            return Json(new { rows = _studentRows}, JsonRequestBehavior.AllowGet);
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


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string Edit(Model.Student Model)
        {
            EnrollmentEntities db = new EnrollmentEntities();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(Model).State = EntityState.Modified;
                    db.SaveChanges();
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