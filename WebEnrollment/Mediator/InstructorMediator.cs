using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model = WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;
using CommonService.Service;
using contract = CommonService.Contracts;
using WebEnrollment.Mediator;

namespace WebEnrollment.Mediator
{
    public interface IInstructorMediator
    {
        model.Instructor GetInstructor(int instructorID);
        List<model.Instructor> GetInstructors();
        model.Instructor UpdateInstructor(model.Instructor instructor);
        //model.Student CreateInstructor(Student student);
    }

    public class InstructorMediator : IInstructorMediator
    {
        //private readonly IInstructorMediator _mediator;
        private readonly IEnrollmentService _service;
        public InstructorMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public model.Instructor GetInstructor(int instructorID)
        {
            var _response = _service.GetInstructor(new contract.InstructorRequest() { InstructorID = instructorID });

            return ConvertResponseToModel(_response);

            //model.Student _student = new model.Student()
            //{
            //    FirstName = _response.FirstName,
            //    LastName = _response.LastName,
            //    Address = _response.Address,
            //    Mobile = _response.Mobile,  
            //    Birthday = _response.Birthday != null ? _response.Birthday.ToString() : null,
            //    Email = _response.Email,
            //    Program = _response.Program,
            //    StudentNumber = _response.StudentNumber,
            //    StudentID = _response.StudentID,
            //    Level   = _response.Level,
            //};
            //return _student;
        }

        public List<model.Instructor> GetInstructors()
        {
            EnrollmentService service = new EnrollmentService();

            var _service = service.GetInstructors(new contract.InstructorRequest());

            List<model.Instructor> _instructorsList = new List<model.Instructor>();

            foreach (var item in _service.Instructors)
            {
                var model = ConvertResponseToModel(item);
                //model.Student _student = new model.Student()
                //{
                //    FirstName = item.FirstName,
                //    LastName = item.LastName,
                //    Address = item.Address,
                //    Mobile = item.Mobile,
                //    Birthday = item.Birthday != null ? item.Birthday.ToString() : null,
                //    Email = item.Email,
                //    Level = item.Level,
                //    Program = item.Program,
                //    StudentID = item.StudentID,
                //    StudentNumber = item.StudentNumber
                //};

                _instructorsList.Add(model);
            }

            return _instructorsList;
        }

        public model.Instructor UpdateInstructor(model.Instructor instructor)
        {
            var _request = ConvertModelToRequest(instructor);

            var _response = _service.UpdateInstructor(_request);

            return ConvertResponseToModel(_response);
        }

        private contract.InstructorRequest ConvertModelToRequest(model.Instructor instructor)
        {
            contract.InstructorRequest _studentRequest = new contract.InstructorRequest()
            {
                InstructorID = instructor.InstructorID,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Mobile = instructor.Mobile,
                Email = instructor.Email,
            };

            return _studentRequest;
        }

        private model.Instructor ConvertResponseToModel(contract.InstructorResponse _response)
        {
            model.Instructor _instructor = new model.Instructor
            {
                InstructorID = _response.InstructorID,
                FirstName = _response.FirstName,
                LastName = _response.LastName,
                Email = _response.Email,
                Mobile = _response.Mobile,
                ValidationErrors = MapValidationErrors(_response.ValidationErrors)
            };

            return _instructor;
        }

        private static List<model.ValidationError> MapValidationErrors(List<contract.ValidationError> errorList)
        {
            List<model.ValidationError> modelErrorList = new List<model.ValidationError>();

            if (errorList != null && errorList.Count > 0)
            {
                errorList.ForEach(error => modelErrorList.Add(new model.ValidationError { Code = error.Code, Message = error.Message }));
            }

            return modelErrorList;
        }

    }
}