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

            Student _student = null;

            if (string.IsNullOrEmpty(request.Id))
            {
                _student = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();

                UpdateStudent(request, _student, context);
            }
            else
            {
                int _id = Convert.ToInt32(request.Id);
                _student = context.Students.Where(x => x.StudentID == _id).FirstOrDefault();

                DeleteStudent(_student, context);
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

            return _studentResponse;
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(request.Id))
            {
                if (string.IsNullOrEmpty(request.StudentNumber))
                    validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "StudentNumber must have a value" });

                if (string.IsNullOrEmpty(request.FirstName))
                    validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "FirstName must have a value" });

                if (string.IsNullOrEmpty(request.LastName))
                    validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "LastName must have a value" });
            }

            return validationErrors;
        }

        private void UpdateStudent(StudentRequest request, Student student, EnrollmentEntities context)
        {
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.Mobile = request.Mobile;
            student.Address = request.Address;
            student.Birthday = request.Birthday;

            context.SaveChanges();
        }

        private void DeleteStudent(Student student, EnrollmentEntities context)
        {
            context.Students.Remove(student);

            context.SaveChanges();
        }
    }
}