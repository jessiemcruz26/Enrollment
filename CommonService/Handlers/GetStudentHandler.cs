using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetStudentHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentEntities();
            var _studentRow = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();

            //var respone = new StudentResponse();
            //var studentList = new List<Contracts.Student>();
            //foreach (var item in _studentRows)
            //{
            //    Contracts.Student student = new Contracts.Student()
            //    {
            //        StudentID = item.StudentID,
            //        Address = item.Address,
            //        Birthday = item.Birthday,
            //        Email = item.Email,
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        Level = item.Level,
            //        Mobile = item.Mobile,
            //        Program = item.Program,
            //        StudentNumber = item.StudentNumber
            //    };

            //    studentList.Add(student);
            //}

            //respone.Students = studentList;

            StudentResponse _response = new StudentResponse
            {
                FirstName = _studentRow.FirstName,
                LastName = _studentRow.LastName,
                Email = _studentRow.Email,
                Mobile = _studentRow.Mobile,
                Level = _studentRow.Level,
                Program = _studentRow.Program,
                StudentID = _studentRow.StudentID,
                StudentNumber = _studentRow.StudentNumber,
                Address = _studentRow.Address,
                Birthday = _studentRow?.Birthday,
            };

            return _response;
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}