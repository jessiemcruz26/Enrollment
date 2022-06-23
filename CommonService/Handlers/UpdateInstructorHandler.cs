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
            var _student = context.Instructors.Where(x => x.InstructorID == request.InstructorID).FirstOrDefault();

            if (request.InstructorID != 0)
            {
                //Edit
                _student.FirstName = request.FirstName;
                _student.LastName = request.LastName;
                _student.Email = request.Email;
                _student.Mobile = request.Mobile;
             
                context.SaveChanges();
            }
            else
            {
                //Delete
                var _instructor = context.Instructors.Where(x => x.InstructorID == request.InstructorID).FirstOrDefault();
                context.Instructors.Remove(_instructor);
                context.SaveChanges();

                return new InstructorResponse();
            }

            var _instructorResponse = new InstructorResponse()
            {
                InstructorID = _student.InstructorID,
                Email = _student.Email,
                FirstName = _student.FirstName,
                LastName = _student.LastName,
                Mobile = _student.Mobile,
            };

            return new InstructorResponse();
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if (request.InstructorID != 0)
            {
                if (!string.IsNullOrEmpty(request.FirstName))
                {
                    if (string.IsNullOrEmpty(request.FirstName))
                        validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "FirstName must have a value" });
                }

                if (!string.IsNullOrEmpty(request.LastName))
                {
                    if (string.IsNullOrEmpty(request.LastName))
                        validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "LastName must have a value" });
                }
            }

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