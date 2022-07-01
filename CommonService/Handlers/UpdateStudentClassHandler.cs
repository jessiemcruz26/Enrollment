using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateStudentClassHandler : RequestHandler<StudentClassResponse, StudentClassRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentClassResponse Process(StudentClassRequest request)
        {
            var context = new EnrollmentDB();

            context.StudentClasses.Add(new StudentClass() { ClassID = request.ClassID, StudentID = request.StudentID });
            context.SaveChanges();
            
            var _studentClassResponse = new StudentClassResponse()
            {
                //Address = _student.Address,
                //Birthday = _student.Birthday,
                //Email = _student.Email,
                //FirstName = _student.FirstName,
                //LastName = _student.LastName,
                //Mobile = _student.Mobile,
                //Level = _student.Level,
                //Program = _student.Program,
                //StudentID = _student.StudentID,
                //StudentNumber = _student.StudentNumber


            };

            return _studentClassResponse;
        }

        protected override List<ValidationError> Validate(StudentClassRequest request)
        {
            var validationErrors = new List<ValidationError>();

            //if (string.IsNullOrEmpty(request.SelectedRow))
            //{
            //    if (string.IsNullOrEmpty(request.StudentNumber))
            //        validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "StudentNumber must have a value" });

            //    if (string.IsNullOrEmpty(request.FirstName))
            //        validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "FirstName must have a value" });

            //    if (string.IsNullOrEmpty(request.LastName))
            //        validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "LastName must have a value" });
            //}

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