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
            var _courseRows = context.Courses.ToList();

            var respone = new CourseResponse();
            var coursetList = new List<Contracts.Course>();
            foreach (var item in _courseRows)
            {
                Contracts.Course course = new Contracts.Course()
                {
                    CourseID = item.CourseID,
                    CourseName = item.CourseName,
                    CourseDescription = item.CourseDescription
                };

                coursetList.Add(course);
            }

            respone.Courses = coursetList;

            return respone;
        }

        protected override List<ValidationError> Validate(CourseRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}