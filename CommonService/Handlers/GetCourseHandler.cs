using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetCourseHandler : RequestHandler<CourseResponse, CourseRequest>
    {
        private readonly EnrollmentDB _context;
        public GetCourseHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override CourseResponse Process(CourseRequest request)
        {
            //get list of courses
            if (request.SelectedRow == null)
            {
                return GetCourses(_context);
            }
            else // get course
            {
                return GetCourse(request, _context);
            }
        }

        private CourseResponse GetCourse(CourseRequest request, EnrollmentDB context)
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

        private CourseResponse GetCourses(EnrollmentDB context)
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