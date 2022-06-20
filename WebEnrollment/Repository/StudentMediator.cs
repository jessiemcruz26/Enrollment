using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService.Service;
using contract = CommonService.Contracts;
using model = WebEnrollment.Models;

namespace WebEnrollment.Repository
{
    public interface IStudentMediator
    {
        model.Student GetStudent(string studentNumber);

        List<Student> GetStudents();

        contract.StudentResponse UpdateStudent(FormCollection form);
    }

    public class StudentMediator : IStudentMediator
    {
        private readonly IEnrollmentService _service;
        public StudentMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public model.Student GetStudent(string studentNumber)
        {
            var _response = _service.GetStudent(new contract.StudentRequest() { StudentNumber = studentNumber });

            model.Student _student = new model.Student()
            {
                FirstName = _response.FirstName,
                LastName = _response.LastName,
                Address = _response.Address,
                Mobile = _response.Mobile,  
                Birthday = _response.Birthday != null ? _response.Birthday.ToString() : null,
                Email = _response.Email,
                Program = _response.Program,
                StudentNumber = _response.StudentNumber,
                StudentID = _response.StudentID,
                Level   = _response.Level,
            };

            return _student;
        }

        public List<Student> GetStudents()
        {
            EnrollmentService service = new EnrollmentService();

            var _service = service.GetStudents(new contract.StudentRequest());

            List<Student> _studentsList = new List<Student>();

            foreach (var item in _service.Students)
            {
                Student _student = new Student()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    Mobile = item.Mobile,
                    Birthday = item.Birthday,
                    Email = item.Email
                };

                _studentsList.Add(_student);
            }
            
            return _studentsList;
        }

        public contract.StudentResponse UpdateStudent(FormCollection form)
        {

            contract.StudentRequest student = new contract.StudentRequest
            {
                StudentNumber = form["StudentNumber"],
                FirstName = form["FirstName"],
                LastName = form["LastName"],
                Email = form["Email"],
                Mobile = form["Mobile"],
                Address = form["Address"],
                Birthday = form["Birthday"] != null ? (DateTime?)Convert.ToDateTime(form["Birthday"]) : null,
            };

            var response = _service.UpdateStudent(student);
          
            return response;
        }
    }
}