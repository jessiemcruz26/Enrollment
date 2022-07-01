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
    public class ClassController : Controller
    {
        private readonly IClassMediator _classMediator;

        public ClassController(IClassMediator classMediator)
        {
            _classMediator = classMediator;
        }

        [HttpGet]
        public ActionResult GetClasses()
        {
            var _listClasses = _classMediator.GetClasses();

            return Json(new { rows = _listClasses }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            //if (string.IsNullOrEmpty(id))
            //{
            //    var _ClassListItems = new List<SelectListItem>();

            //    _ClassListItems.Add(new SelectListItem() { Text = "Northern Cape", Value = "NC" });
            //    _ClassListItems.Add(new SelectListItem() { Text = "Free State", Value = "FS" });
            //    _ClassListItems.Add(new SelectListItem() { Text = "Western Cape", Value = "WC" });

            //    return View(new model.Class() { ClassListItems = _ClassListItems });
            //}

            Class _class = _classMediator.GetClass(Convert.ToInt32(id));

            return Json(new { row = _class }, JsonRequestBehavior.AllowGet);
        }

        public string EditGrid(Class Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Class _class = new Class
                    {
                        ClassID = Model.ClassID.ToString(),
                        InstructorID = Model.InstructorID.ToString(),
                        CourseID = Model.CourseID.ToString(),
                        ClassDate = Model.ClassDate,
                        ClassTime = Model.ClassTime,
                        RoomNumber = Model.RoomNumber,
                        ClassCode = Model.ClassCode
                    };

                    _classMediator.UpdateClass(_class);
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
        public string Create([Bind(Exclude = "ClassId")] Class Model)
        {
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _Class = _classMediator.CreateClass(Model);

                    if (_Class.ValidationErrors.Any())
                    {
                        foreach (var item in _Class.ValidationErrors)
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
                    _classMediator.UpdateClass(new Class { SelectedRow = selectedRow });
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

        //private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Class> elements)
        //{
        //    var selectList = new List<SelectListItem>();
        //    foreach (var element in elements)
        //    {
        //        selectList.Add(new SelectListItem
        //        {
        //            Value = element.ClassID.ToString(),
        //            Text = element.ClassName
        //        });
        //    }
        //    return selectList;
        //}

        //Edit fields
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                Class _class = new Class
                {
                    ClassID = form["ClassID"],
                    InstructorID = form["InstructorID"],
                    CourseID = form["CourseID"],
                    ClassDate = form["ClassDate"],
                    ClassTime = form["ClassTime"],
                    RoomNumber = form["RoomNumber"],
                    ClassCode = form["RoomNumber"]
                };

                var response = _classMediator.UpdateClass(_class);

                return View(response);
            }
            catch
            {
                return View();
            }
        }
    }
}
