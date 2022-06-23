using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateInstructorHandler : RequestHandler<InstructorResponse, InstructorRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override InstructorResponse Process(InstructorRequest request)
        {
            var context = new EnrollmentEntities();

            Instructor _instructor = new Instructor() {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Mobile = request.Mobile
            };

            context.Instructors.Add(_instructor);
            context.SaveChanges();

            return new InstructorResponse();
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(request.FirstName))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "FirstName must have a value" });

            if (string.IsNullOrEmpty(request.LastName))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "LastName must have a value" });

            return validationErrors;
        }
    }
}