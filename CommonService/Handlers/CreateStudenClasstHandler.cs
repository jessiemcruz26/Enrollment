using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateStudentClassHandler : RequestHandler<StudentClassResponse, StudentClassRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentClassResponse Process(StudentClassRequest request)
        {
            var context = new EnrollmentDB();

            StudentClass _studentClass = new StudentClass()
            {
                ClassID = request.ClassID,
                StudentID = request.StudentID,
            };
                
            context.StudentClasses.Add(_studentClass);
            context.SaveChanges();

            return new StudentClassResponse();
        }

        protected override List<ValidationError> Validate(StudentClassRequest request)
        {
            var validationErrors = new List<ValidationError>();

            return validationErrors;
        }
    }
}