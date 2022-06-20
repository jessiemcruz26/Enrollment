using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEnrollment.Repository;
using Model = WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;
using CommonService.Service;

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
            var students = _business.GetStudents();

            return View();
        }

        public ActionResult Save(FormCollection form)
        {
            var a = form["StudentNumber"];
            _business.Save();
            return View("Index");
        }

        public ActionResult GetStudents()
        {
            EnrollmentService service = new EnrollmentService();

            var _studentRows = service.GetStudent(new CommonService.Contracts.StudentRequest());

            return Json(new { rows = _studentRows.Students }, JsonRequestBehavior.AllowGet);
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