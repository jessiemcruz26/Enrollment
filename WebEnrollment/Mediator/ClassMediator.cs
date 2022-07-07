using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService.Service;
using WebEnrollment.Models;
using contract = CommonService.Contracts;
using model = WebEnrollment.Models;
using WebEnrollment.Common;

namespace WebEnrollment.Mediator
{
    public interface IClassMediator
    {
        Class GetClass(int ClassID);
        List<Class> GetClasses();
        Class UpdateClass(Class Class);
        Class CreateClass(Class Class);
    }

    public class ClassMediator : IClassMediator
    {
        private readonly IEnrollmentService _service;

        public ClassMediator(IEnrollmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get class using class id from enrollment service
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public Class GetClass(int ClassID)
        {
            var _class = _service.GetClass(new contract.ClassRequest() { ClassID = ClassID });

            return ConvertResponseToModel(_class);
        }

        /// <summary>
        /// Get list of classes from enrollment service
        /// </summary>
        /// <returns></returns>
        public List<Class> GetClasses()
        {
            var _response = _service.GetClass(new contract.ClassRequest());

            List<Class> _classList = new List<Class>();

            foreach (var item in _response.Classes)
            {
                var model = ConvertResponseToModel(item);

                _classList.Add(model);
            }

            return _classList;
        }

        /// <summary>
        /// Update class from enrollment service
        /// </summary>
        /// <param name="Class"></param>
        /// <returns></returns>
        public Class UpdateClass(Class Class)
        {
            var _ClassRequest = ConvertModelToRequest(Class);

            var _response = _service.UpdateClass(_ClassRequest);

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Create class using enrollment service
        /// </summary>
        /// <param name="Class"></param>
        /// <returns></returns>
        public Class CreateClass(Class Class)
        {
            var _ClassRequest = ConvertModelToRequest(Class);

            var _response = _service.CreateClass(_ClassRequest);

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Map web model to service model
        /// </summary>
        /// <param name="_class"></param>
        /// <returns></returns>
        private contract.ClassRequest ConvertModelToRequest(model.Class _class)
        {
            contract.ClassRequest _classRequest = new contract.ClassRequest()
            {
                ClassID = Convert.ToInt32(_class.ClassID),
                InstructorID = Convert.ToInt32(_class.InstructorID),
                CourseID = Convert.ToInt32(_class.CourseID),
                ClassDate = _class.ClassDate,
                ClassTime = _class.ClassTime,
                RoomNumber = _class.RoomNumber,
                ClassCode = _class.ClassCode,
                SelectedRow = _class.SelectedRow
            };

            return _classRequest;
        }

        /// <summary>
        /// Map service model to web model
        /// </summary>
        /// <param name="_response"></param>
        /// <returns></returns>
        private Class ConvertResponseToModel(contract.ClassResponse _response)
        {
            Class _class = new Class
            {
                ClassID = _response.ClassID != 0 ? _response.ClassID.ToString() : string.Empty,
                InstructorID = _response.InstructorID,
                CourseID = _response.CourseID,
                ClassDate = _response.ClassDate,
                ClassTime = _response.ClassTime,
                RoomNumber = _response.RoomNumber,
                ClassCode = _response.ClassCode,
                Courses = GetSelectListItems(_response.Courses),
                ValidationErrors = EnrollmentHelper.MapValidationErrors(_response.ValidationErrors)
            };

            return _class;
        }

        /// <summary>
        /// Map list of service course to model course
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private List<Course> GetSelectListItems(List<CommonService.Course> elements)
        {
            var _courses = new List<Course>();
            
            foreach (var element in elements)
            {
                var _course = new Course()
                {
                    CourseID = element.CourseID,
                    CourseName = element.CourseName
                };

                _courses.Add(_course);
            }

            return _courses;
        }
    }
}