using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateCourseHandler : RequestHandler<CourseResponse, CourseRequest>
    {
        private readonly EnrollmentDB _context;
        public UpdateCourseHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override CourseResponse Process(CourseRequest request)
        {
            if (request.CourseID == 0)
                request.CourseID = Convert.ToInt32(request.SelectedRow);

            var _course = _context.Courses.Where(x => x.CourseID == request.CourseID).FirstOrDefault();

            if (string.IsNullOrEmpty(request.SelectedRow))
            {
                UpdateCourse(request, _course, _context);
            }
            else
            {
                DeleteCourse(_course, _context);
            }

            var _courseResponse = new CourseResponse
            {
                CourseID = request.CourseID,
                CourseName = request.CourseName,
                CourseDescription = request.CourseDescription
            };

            return _courseResponse;
        }

        protected override List<ValidationError> Validate(CourseRequest request)
        {
            var validationErrors = new List<ValidationError>();

            return validationErrors;
        }

        private void UpdateCourse(CourseRequest request, Course course, EnrollmentDB context)
        {
            course.CourseID = request.CourseID;
            course.CourseName = request.CourseName;
            course.CourseDescription = request.CourseDescription;

            context.SaveChanges();
        }

        private void DeleteCourse(Course course, EnrollmentDB context)
        {
            context.Courses.Remove(course);

            context.SaveChanges();
        }
    }
}