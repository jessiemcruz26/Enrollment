using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService.Service;
using WebEnrollment.Models;
using contract = CommonService.Contracts;
using model = WebEnrollment.Models;

namespace WebEnrollment.Mediator
{
    public interface IStudentMediator
    {
        model.Student GetStudent(string studentNumber);
        List<model.Student> GetStudents();
        model.Student UpdateStudent(model.Student student);
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

            return ConvertResponseToModel(_response);
        }

        public List<model.Student> GetStudents()
        {

            var _response = _service.GetStudent(new contract.StudentRequest());

            List<model.Student> _studentsList = new List<model.Student>();

            foreach (var item in _response.Students)
            {
                var model = ConvertResponseToModel(item);

                _studentsList.Add(model);
            }
            
            return _studentsList;
        }

        public model.Student UpdateStudent(model.Student student)
        {
            var _studentRequest = ConvertModelToRequest(student);

            var _response = _service.UpdateStudent(_studentRequest);

            return ConvertResponseToModel(_response);
        }

        private List<SelectListItem> GetProgramList(string program)
        {
            string[] _programs = { "Electronics", "Civil", "Mechanical" };

            var _programListItems = new List<SelectListItem>();
            foreach (var item in _programs)
            {
                _programListItems.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item,
                    Selected = item == program
                });
            }
            
            return _programListItems;
        }

        private static List<ValidationError> MapValidationErrors(List<contract.ValidationError> errorList)
        {
            List<ValidationError> modelErrorList = new List<ValidationError>();

            if (errorList != null && errorList.Count > 0)
            {
                errorList.ForEach(error => modelErrorList.Add(new ValidationError { Code = error.Code, Message = error.Message }));
            }

            return modelErrorList;
        }

        public model.Student CreateStudent(Student student)
        {
            var _studentRequest = ConvertWebToRequest(student);

            var _response = _service.CreateStudent(_studentRequest);

            return ConvertResponseToModel(_response);
        }

        private contract.StudentRequest ConvertWebToRequest(Student student)
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

            return _studentRequest;
        }

        private contract.StudentRequest ConvertModelToRequest(model.Student student)
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
                Id = student.ID
            };

            return _studentRequest;
        }

        private model.Student ConvertResponseToModel(contract.StudentResponse _response)
        {
            model.Student _student = new model.Student
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
                ProgramListItems = GetProgramList(_response.Program),
                ValidationErrors = MapValidationErrors(_response.ValidationErrors)
            };

            return _student;
        }
    }
}