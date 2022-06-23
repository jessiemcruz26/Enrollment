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
        model.Course GetCourse(string courseID);
        model.Course UpdateCourse(model.Course course, string id);
        model.Course CreateCourse(Course student);
    }

    public class CourseMediator : ICourseMediator
    {
        private readonly IEnrollmentService _service;

        public CourseMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public model.Course GetCourse(string courseID)
        {
            var _course = _service.GetCourse(new contract.CourseRequest() { CourseID = Convert.ToInt32(courseID) });

            return ConvertResponseToModel(_course);
        }

        public model.Course UpdateCourse(model.Course course, string id)
        {
            course.ID = id;

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
            contract.CourseRequest _studentRequest = new contract.CourseRequest()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
              
            };

            return _studentRequest;
        }

        private contract.CourseRequest ConvertModelToRequest(model.Course course)
        {
            contract.CourseRequest _courseRequest = new contract.CourseRequest()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Id = course.ID
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