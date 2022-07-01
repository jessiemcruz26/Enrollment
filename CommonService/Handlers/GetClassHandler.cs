using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetClassHandler : RequestHandler<ClassResponse, ClassRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override ClassResponse Process(ClassRequest request)
        {
            var context = new EnrollmentDB();

            //get list of Classes
            if (request.SelectedRow == null)
            {
                return GetClasses(context);
            }
            else // get Class
            {
                return GetClass(request, context);
            }
        }

        private ClassResponse GetClass(ClassRequest request, EnrollmentDB context)
        {
            var _classRow = context.Classes.Where(x => x.ClassID == request.ClassID).FirstOrDefault();
            var _courseRows = context.Courses.ToList();
            ClassResponse _response = new ClassResponse
            {
                ClassID = _classRow.ClassID,
                InstructorID = _classRow.InstructorID.ToString(),
                CourseID = _classRow.CourseID.ToString(),
                ClassDate = _classRow.ClassDate,
                ClassTime = _classRow.ClassTime,
                RoomNumber = _classRow.RoomNumber,
                Courses = _courseRows,
                ClassCode = _classRow.ClassCode,
            };

            return _response;
        }

        private ClassResponse GetClasses(EnrollmentDB context)
        {
            var _ClassRows = context.Classes.ToList();
            var _courseRows = context.Courses.ToList();
            var response = new ClassResponse();
            foreach (var item in _ClassRows)
            {
                ClassResponse _response = new ClassResponse
                {
                    ClassID = item.ClassID,
                    ClassDate = item.ClassDate,
                    ClassTime = item.ClassTime,
                    RoomNumber = item.RoomNumber,
                    Courses = _courseRows,
                    ClassCode= item.ClassCode,
                };

                var _instructor = context.Instructors.Where(x => x.InstructorID == item.InstructorID).FirstOrDefault();
                var _course = context.Courses.Where(x => x.CourseID == item.CourseID).FirstOrDefault();
                _response.InstructorID = _instructor.FirstName + " " + _instructor.LastName;
                _response.CourseID = _course.CourseName;
                
                response.Classes.Add(_response);
            }

            return response;
        }

        protected override List<ValidationError> Validate(ClassRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}