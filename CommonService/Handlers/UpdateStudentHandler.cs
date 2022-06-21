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

            if (string.IsNullOrEmpty(request.Id))
            {
                _student.FirstName = request.FirstName;
                _student.LastName = request.LastName;
                _student.Email = request.Email;
                _student.Mobile = request.Mobile;
                _student.Address = request.Address;
                _student.Birthday = request.Birthday;
                context.SaveChanges();
            }
            else
            {
                int _id = Convert.ToInt32(request.Id);
                var _stud = context.Students.Where(x => x.StudentID == _id).FirstOrDefault();
                context.Students.Remove(_stud);
                context.SaveChanges();

                return new StudentResponse();
            }

            var _studentResponse = new StudentResponse()
            {
                Address = _student.Address,
                Birthday = _student.Birthday,
                Email = _student.Email,
                FirstName = _student.FirstName,
                LastName = _student.LastName,
                Mobile = _student.Mobile,
                Level = _student.Level,
                Program = _student.Program,
                StudentID = _student.StudentID,
                StudentNumber = _student.StudentNumber
            };

            return new StudentResponse();
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();
       
            return validationErrors;
        }
    }
}