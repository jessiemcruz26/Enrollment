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

        /// <summary>
        /// Get list of classes for Grid 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetClasses()
        {
            var _listClasses = _classMediator.GetClasses();

            return Json(new { rows = _listClasses }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update class record in Grid
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
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

                    var _response = _classMediator.UpdateClass(_class);

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

        /// <summary>
        /// Create class record in Grid
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create([Bind(Exclude = "ClassId")] Class Model)
        {
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var _response = _classMediator.CreateClass(Model);

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

        /// <summary>
        /// Delete class record in Grid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete(string id)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _classMediator.UpdateClass(new Class { SelectedRow = id });
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Edit(string id)
        //{
        //    Class _class = _classMediator.GetClass(Convert.ToInt32(id));

        //    return Json(new { row = _class }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult Edit(FormCollection form)
        //{
        //    try
        //    {
        //        Class _class = new Class
        //        {
        //            ClassID = form["ClassID"],
        //            InstructorID = form["InstructorID"],
        //            CourseID = form["CourseID"],
        //            ClassDate = form["ClassDate"],
        //            ClassTime = form["ClassTime"],
        //            RoomNumber = form["RoomNumber"],
        //            ClassCode = form["RoomNumber"]
        //        };

        //        var response = _classMediator.UpdateClass(_class);

        //        return View(response);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
