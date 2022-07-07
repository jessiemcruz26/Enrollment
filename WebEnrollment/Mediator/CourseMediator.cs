using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService.Service;
using WebEnrollment.Models;
using contract = CommonService.Contracts;
using WebEnrollment.Common;

namespace WebEnrollment.Mediator
{
    public interface ICourseMediator
    {
        Course GetCourse(int courseID);
        List<Course> GetCourses();
        Course UpdateCourse(Course course);
        Course CreateCourse(Course course);
    }

    public class CourseMediator : ICourseMediator
    {
        private readonly IEnrollmentService _service;

        public CourseMediator(IEnrollmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieve a course from enrollment service using course id
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public Course GetCourse(int courseID)
        {
            var _course = _service.GetCourse(new contract.CourseRequest() { CourseID = courseID });

            return ConvertResponseToModel(_course);
        }

        /// <summary>
        /// Retrieve list of courses from enrollment service
        /// </summary>
        /// <returns></returns>
        public List<Course> GetCourses()
        {
            var _response = _service.GetCourse(new contract.CourseRequest());

            List<Course> _coursesList = new List<Course>();

            foreach (var item in _response.Courses)
            {
                var model = ConvertResponseToModel(item);

                _coursesList.Add(model);
            }

            return _coursesList;
        }

        /// <summary>
        /// Update a course using enrollment service
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Course UpdateCourse(Course course)
        {
            var _courseRequest = ConvertModelToRequest(course);

            var _response = _service.UpdateCourse(_courseRequest);

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Create course record using enrollment service
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Course CreateCourse(Course course)
        {
            var _courseRequest = ConvertModelToRequest(course);

            var _response = _service.CreateCourse(_courseRequest);

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Map web model to service model
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        private contract.CourseRequest ConvertModelToRequest(Course course)
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

        /// <summary>
        /// Map service model to web model
        /// </summary>
        /// <param name="_response"></param>
        /// <returns></returns>
        private Course ConvertResponseToModel(contract.CourseResponse _response)
        {
            Course _course = new Course
            {
                CourseID = _response.CourseID,
                CourseName = _response.CourseName,
                CourseDescription = _response.CourseDescription,

                ValidationErrors = EnrollmentHelper.MapValidationErrors(_response.ValidationErrors)
            };

            return _course;
        }
    }
}