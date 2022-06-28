using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateStudentHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentDB();

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
                
            context.Students.Add(_student);
            context.SaveChanges();

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