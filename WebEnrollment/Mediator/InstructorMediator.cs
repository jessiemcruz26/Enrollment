using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEnrollment.Models;
using System.Data.Entity;
using WebEnrollment;
using CommonService.Service;
using contract = CommonService.Contracts;

namespace WebEnrollment.Mediator
{
    public interface IInstructorMediator
    {
        Instructor GetInstructor(string instructorNumber);
        List<Instructor> GetInstructors();
        Instructor UpdateInstructor(Instructor instructor);
        Instructor CreateInstructor(Instructor instructor);
    }

    public class InstructorMediator : IInstructorMediator
    {
        private readonly IEnrollmentService _service;
        public InstructorMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public Instructor GetInstructor(string instructorNumber)
        {
            var _response = _service.GetInstructor(new contract.InstructorRequest() { InstructorNumber = instructorNumber });

            if (_response.InstructorID == 0)
                return new Instructor { IsInstructorFound = false };

            return ConvertResponseToModel(_response);
        }

        public List<Instructor> GetInstructors()
        {
            var _response = _service.GetInstructor(new contract.InstructorRequest());

            List<Instructor> _instructorsList = new List<Instructor>();

            foreach (var item in _response.Instructors)
            {
                var model = ConvertResponseToModel(item);

                _instructorsList.Add(model);
            }

            return _instructorsList;
        }

        public Instructor CreateInstructor(Instructor instructor)
        {
            var _instructorRequest = ConvertWebToRequest(instructor);

            var _response = _service.CreateInstructor(_instructorRequest);

            return ConvertResponseToModel(_response);
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            var _request = ConvertModelToRequest(instructor);

            var _response = _service.UpdateInstructor(_request);

            if (_response.ValidationErrors.Any())
            {
                var _responseGet = _service.GetInstructor(new contract.InstructorRequest { InstructorNumber = instructor.InstructorNumber });

                foreach (var item in _response.ValidationErrors)
                {
                    switch (item.Code)
                    {
                        case "FirstName":
                            _responseGet.FirstName = string.Empty;
                            break;
                        case "LastName":
                            _responseGet.LastName = string.Empty;
                            break;
                        case "InstructorNumber":
                            _responseGet.InstructorNumber = string.Empty;
                            break;
                    }
                }

                _responseGet.ValidationErrors = _response.ValidationErrors;

                return ConvertResponseToModel(_responseGet);
            }

            return ConvertResponseToModel(_response);
        }

        private contract.InstructorRequest ConvertModelToRequest(Instructor instructor)
        {
            contract.InstructorRequest _studentRequest = new contract.InstructorRequest()
            {
                InstructorID = !string.IsNullOrEmpty(instructor.InstructorID) ? Convert.ToInt32(instructor.InstructorID) : 0,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Mobile = instructor.Mobile,
                Email = instructor.Email,
                SelectedRow = instructor.SelectedRow,
                InstructorNumber = instructor.InstructorNumber
            };

            return _studentRequest;
        }

        private contract.InstructorRequest ConvertWebToRequest(Instructor instructor)
        {
            contract.InstructorRequest _request = new contract.InstructorRequest()
            {
                InstructorID = !string.IsNullOrEmpty(instructor.InstructorID) ? Convert.ToInt32(instructor.InstructorID) : 0,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Mobile = instructor.Mobile,
                Email = instructor.Email,
                InstructorNumber = instructor.InstructorNumber
            };

            return _request;
        }

        private Instructor ConvertResponseToModel(contract.InstructorResponse _response)
        {
            Instructor _instructor = new Instructor
            {
                InstructorID = _response.InstructorID.ToString(),
                FirstName = _response.FirstName,
                LastName = _response.LastName,
                Email = _response.Email,
                Mobile = _response.Mobile,
                InstructorNumber = _response.InstructorNumber,
                ValidationErrors = MapValidationErrors(_response.ValidationErrors)
            };

            return _instructor;
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

    }
}