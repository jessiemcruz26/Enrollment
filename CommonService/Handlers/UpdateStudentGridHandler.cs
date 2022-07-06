using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateStudentGridHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentDB();

            Student _student = null;

            if (!string.IsNullOrEmpty(request.SelectedRow))
            {
                int _studentId = Convert.ToInt32(request.SelectedRow);

                _student = context.Students.Where(x => x.StudentID == _studentId).FirstOrDefault();

                DeleteStudent(_student, context);
            }
            else
            {
                _student = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();

                UpdateStudent(request, _student, context);
            }

            return new StudentResponse();
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(request.SelectedRow))
            {
                if (string.IsNullOrEmpty(request.StudentNumber))
                    validationErrors.Add(new ValidationError { Code = "StudentNumber", Message = "Student Number must have a value" });

                if (string.IsNullOrEmpty(request.FirstName))
                    validationErrors.Add(new ValidationError { Code = "FirstName", Message = "First Name must have a value" });

                if (string.IsNullOrEmpty(request.LastName))
                    validationErrors.Add(new ValidationError { Code = "LastName", Message = "Last Name must have a value" });
            }

            return validationErrors;
        }

        private void UpdateStudent(StudentRequest request, Student student, EnrollmentDB context)
        {
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.Mobile = request.Mobile;
            student.Address = request.Address;
            student.Birthday = request.Birthday;

            context.SaveChanges();
        }

        private void DeleteStudent(Student student, EnrollmentDB context)
        {
            context.Students.Remove(student);

            context.SaveChanges();
        }
    }
}