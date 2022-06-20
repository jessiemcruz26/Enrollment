using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetStudentsHandler : RequestHandler<StudentsResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentsResponse Process(StudentRequest request)
        {
            var context = new EnrollmentEntities();
            var _studentRows = context.Students.ToList();

            var response = new StudentsResponse();
            foreach (var item in _studentRows)
            {
                StudentResponse student = new StudentResponse()
                {
                    StudentID = item.StudentID,
                    Address = item.Address,
                    Birthday = item.Birthday,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Level = item.Level,
                    Mobile = item.Mobile,
                    Program = item.Program,
                    StudentNumber = item.StudentNumber
                };

                response.Students.Add(student);
            }

            return response;
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}