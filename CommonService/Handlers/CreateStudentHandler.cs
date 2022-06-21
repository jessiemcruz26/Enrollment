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

        protected StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentEntities();

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

            var _stud = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();

            StudentResponse _response = new StudentResponse();

            
            //var _studentRows = context.Students.ToList();

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

            return _response;
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if(string.IsNullOrEmpty(request.FirstName))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "FirstName must have a value" });

            if (string.IsNullOrEmpty(request.LastName))
                validationErrors.Add(new ValidationError { Code = "Field Empty", Message = "LastName must have a value" });

            return validationErrors;
        }
    }
}