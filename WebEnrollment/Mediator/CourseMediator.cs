using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService.Service;
using WebEnrollment.Models;
using contract = CommonService.Contracts;
using model = WebEnrollment.Models;

namespace WebEnrollment.Mediator
{
    public interface ICourseMediator
    {
        model.Course GetCourse(int courseID);
        List<model.Course> GetCourses();
        model.Course UpdateCourse(model.Course course);
        model.Course CreateCourse(Course course);
    }

    public class CourseMediator : ICourseMediator
    {
        private readonly IEnrollmentService _service;

        public CourseMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public model.Course GetCourse(int courseID)
        {
            var _course = _service.GetCourse(new contract.CourseRequest() { CourseID = courseID });

            return ConvertResponseToModel(_course);
        }

        public List<model.Course> GetCourses()
        {
            var _response = _service.GetCourse(new contract.CourseRequest());

            List<model.Course> _coursesList = new List<model.Course>();

            foreach (var item in _response.Courses)
            {
                var model = ConvertResponseToModel(item);

                _coursesList.Add(model);
            }

            return _coursesList;
        }

        public model.Course UpdateCourse(model.Course course)
        {
            var _courseRequest = ConvertModelToRequest(course);

            var _response = _service.UpdateCourse(_courseRequest);

            return ConvertResponseToModel(_response);
        }

        private static List<ValidationError> MapValidationErrors(List<contract.ValidationError> errorList)
        {
            List<ValidationError> modelErrorList = new List<ValidationError>();

            if (errorList != null && errorList.Count > 0)
            {
                errorList.ForEach(error => modelErrorList.Add(new ValidationError { Code = error.Code, Message = error.Message }));
            }

            return modelErrorList;
        }

        public model.Course CreateCourse(Course course)
        {
            var _courseRequest = ConvertWebToRequest(course);

            var _response = _service.CreateCourse(_courseRequest);

            return ConvertResponseToModel(_response);
        }

        private contract.CourseRequest ConvertWebToRequest(Course course)
        {
            contract.CourseRequest _courseRequest = new contract.CourseRequest()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
              
            };

            return _courseRequest;
        }
        private contract.CourseRequest ConvertModelToRequest(model.Course course)
        {
            contract.CourseRequest _courseRequest = new contract.CourseRequest()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                SelectedRow = course.SelectedRow
            };

            return _courseRequest;
        }

        private model.Course ConvertResponseToModel(contract.CourseResponse _response)
        {
            model.Course _course = new model.Course
            {
                CourseID = _response.CourseID,
                CourseName = _response.CourseName,
                CourseDescription = _response.CourseDescription,

                ValidationErrors = MapValidationErrors(_response.ValidationErrors)
            };

            return _course;
        }
    }
}