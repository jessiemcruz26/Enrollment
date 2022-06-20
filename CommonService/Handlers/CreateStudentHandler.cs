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
            var context = new EnrollmentEntities();
            var _studentRows = context.Students.ToList();

            var respone = new StudentResponse();
            var studentList = new List<Contracts.Student>();
            foreach (var item in _studentRows)
            {
                Contracts.Student student = new Contracts.Student()
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

                studentList.Add(student);
            }

            respone.Students = studentList;

            return respone;
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}