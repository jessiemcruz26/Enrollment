using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateStudentHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentEntities();
           
            var _student = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();
            _student.FirstName = request.FirstName;
            _student.LastName = request.FirstName;
            _student.Email = request.Email;
            _student.Mobile = request.Mobile;
            _student.Address = request.Address;
            _student.Birthday = request.Birthday;

            context.SaveChanges();

            return new StudentResponse();
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}