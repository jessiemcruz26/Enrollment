using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateInstructorHandler : RequestHandler<InstructorResponse, InstructorRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override InstructorResponse Process(InstructorRequest request)
        {
            var context = new EnrollmentEntities();

            var _instructor = context.Instructors.Where(x => x.InstructorID == request.InstructorID).FirstOrDefault();

            if (string.IsNullOrEmpty(request.ID))
            {
                UpdateInstructor(request, _instructor, context);
            }
            else
            {
                DeleteInsrtuctor(_instructor, context);

                return new InstructorResponse();
            }

            var _courseResponse = new CourseResponse
            {
                CourseID = request.CourseID,
                CourseName = request.CourseName,
                CourseDescription = request.CourseDescription
            };

            return _courseResponse;
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }

        private void UpdateInstructor(InstructorRequest request, Instructor instructor, EnrollmentEntities context)
        {
            instructor.InstructorID = request.InstructorID;
            instructor.FirstName = request.FirstName;
            instructor.LastName = request.LastName;
            instructor.Mobile = request.Mobile;
            instructor.Email = request.Email;
            context.SaveChanges();
        }

        private void DeleteInsrtuctor(Instructor instructor, EnrollmentEntities context)
        {
            context.Instructors.Remove(instructor);

            context.SaveChanges();
        }
    }
}