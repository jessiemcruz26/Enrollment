using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateCourseHandler : RequestHandler<CourseResponse, CourseRequest>
    {
        private readonly EnrollmentDB _context;
        public CreateCourseHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override CourseResponse Process(CourseRequest request)
        {
            Course _course = new Course()
            {
                CourseID = request.CourseID,
                CourseName = request.CourseName,
                CourseDescription = request.CourseDescription
            };

            _context.Courses.Add(_course);
            _context.SaveChanges();

            return new CourseResponse();
        }

        protected override List<ValidationError> Validate(CourseRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}