using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateStudentHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        private readonly EnrollmentDB _context;
        public CreateStudentHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            Student _student = new Student()
            {
                StudentNumber = request.StudentNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Mobile = request.Mobile,
                Email = request.Email,
                Level = request.Level,
                Program = request.Program,
                Address = request.Address
            };

            _context.Students.Add(_student);
            _context.SaveChanges();

            return new StudentResponse();
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(request.StudentNumber))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "StudentNumber must have a value" });

            if (string.IsNullOrEmpty(request.FirstName))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "FirstName must have a value" });

            if (string.IsNullOrEmpty(request.LastName))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "LastName must have a value" });

            return validationErrors;
        }
    }
}