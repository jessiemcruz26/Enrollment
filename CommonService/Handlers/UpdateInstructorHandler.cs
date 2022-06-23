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

            Instructor _instructor = null;

            if (string.IsNullOrEmpty(request.ID))
            {
                _instructor = context.Instructors.Where(x => x.InstructorID == request.InstructorID).FirstOrDefault();

                UpdateInstructor(request, _instructor, context);
            }
            else
            {
                int _id = Convert.ToInt32(request.ID);
                _instructor = context.Instructors.Where(x => x.InstructorID == _id).FirstOrDefault();

                DeleteInsrtuctor(_instructor, context);
            }

            var _instructorResponse = new InstructorResponse()
            {
                Email = _instructor.Email,
                FirstName = _instructor.FirstName,
                LastName = _instructor.LastName,
                Mobile = _instructor.Mobile,
            };

            return _instructorResponse;
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