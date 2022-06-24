using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetCourseHandler : RequestHandler<CourseResponse, CourseRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override CourseResponse Process(CourseRequest request)
        {
            var context = new EnrollmentEntities();

            //get list of courses
            if (request.Id == null)
            {
                return GetCourses(context);
            }
            else // get course
            {
                return GetCourse(request, context);
            }
        }

        private CourseResponse GetCourse(CourseRequest request, EnrollmentEntities context)
        {
            var _courseRow = context.Courses.Where(x => x.CourseID == request.CourseID).FirstOrDefault();

            CourseResponse _response = new CourseResponse
            {
                CourseID = _courseRow.CourseID,
                CourseName = _courseRow.CourseName,
                CourseDescription = _courseRow.CourseDescription
            };

            return _response;
        }

        private CourseResponse GetCourses(EnrollmentEntities context)
        {
            var _courseRows = context.Courses.ToList();

            var response = new CourseResponse();
            foreach (var item in _courseRows)
            {
                CourseResponse course = new CourseResponse()
                {
                    CourseID = item.CourseID,
                    CourseName = item.CourseName,
                    CourseDescription = item.CourseDescription
                };

                response.Courses.Add(course);
            }

            return response;
        }

        protected override List<ValidationError> Validate(CourseRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}