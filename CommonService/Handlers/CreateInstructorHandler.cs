using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateInstructorHandler : RequestHandler<InstructorResponse, InstructorRequest>
    {
        private readonly EnrollmentDB _context;
        public CreateInstructorHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override InstructorResponse Process(InstructorRequest request)
        {
            Instructor _instructor = new Instructor() {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Mobile = request.Mobile,
                InstructorNumber = request.InstructorNumber
            };

            _context.Instructors.Add(_instructor);
            _context.SaveChanges();

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