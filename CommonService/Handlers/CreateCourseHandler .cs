using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateCourseHandler : RequestHandler<CourseResponse, CourseRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override CourseResponse Process(CourseRequest request)
        {
            var context = new EnrollmentDB();

            Course _course = new Course()
            {
                CourseID = request.CourseID,
                CourseName = request.CourseName,
                CourseDescription = request.CourseDescription
            };

            context.Courses.Add(_course);
            context.SaveChanges();

            return new CourseResponse();
        }

        protected override List<ValidationError> Validate(CourseRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}