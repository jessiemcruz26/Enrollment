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
        List<model.Student> GetStudents();
        model.Student UpdateStudent(model.Student student, string id);
        model.Student CreateStudent(Student student);
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

        public List<model.Student> GetStudents()
        {
            EnrollmentService service = new EnrollmentService();

            var _service = service.GetStudents(new contract.StudentRequest());

            List<model.Student> _studentsList = new List<model.Student>();

            foreach (var item in _service.Students)
            {
                model.Student _student = new model.Student()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    Mobile = item.Mobile,
                    Birthday = item.Birthday != null ? item.Birthday.ToString() : null,
                    Email = item.Email,
                    Level = item.Level,
                    Program = item.Program,
                    StudentID = item.StudentID,
                    StudentNumber = item.StudentNumber
                };

                _studentsList.Add(_student);
            }
            
            return _studentsList;
        }

        public model.Student UpdateStudent(model.Student student, string id)
        {
            contract.StudentRequest _studentRequest = new contract.StudentRequest()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                Mobile = student.Mobile,
                Birthday = null,
                Email = student.Email,
                Program = student.Program,
                StudentNumber = student.StudentNumber,
                StudentID = student.StudentID,
                Level = student.Level,
                Id = id
            };

            var _response = _service.UpdateStudent(_studentRequest);

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
                Level = _response.Level,
            };

            return _student;
        }

        public model.Student CreateStudent(Student student)
        {
            contract.StudentRequest _studentRequest = new contract.StudentRequest()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                Mobile = student.Mobile,
                Birthday = null,
                Email = student.Email,
                Program = student.Program,
                StudentNumber = student.StudentNumber,
                StudentID = student.StudentID,
                Level = student.Level,
            };

            var _response = _service.CreateStudent(_studentRequest);

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
                Level = _response.Level,
            };

            return _student;
        }
    }
}