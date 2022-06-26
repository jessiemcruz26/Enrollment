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
        model.Instructor GetInstructor(string instructorID);
        List<model.Instructor> GetInstructors();
        model.Instructor UpdateInstructor(model.Instructor instructor);
        model.Instructor CreateInstructor(Instructor instructor);
    }

    public class InstructorMediator : IInstructorMediator
    {
        private readonly IEnrollmentService _service;
        public InstructorMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public model.Instructor GetInstructor(string instructorID)
        {
            var _response = _service.GetInstructor(new contract.InstructorRequest() { ID = instructorID });

            return ConvertResponseToModel(_response);
        }

        public List<model.Instructor> GetInstructors()
        {
            var _response = _service.GetInstructor(new contract.InstructorRequest());

            List<model.Instructor> _instructorsList = new List<model.Instructor>();

            foreach (var item in _response.Instructors)
            {
                var model = ConvertResponseToModel(item);

                _instructorsList.Add(model);
            }

            return _instructorsList;
        }

        public model.Instructor CreateInstructor(Instructor instructor)
        {
            var _instructorRequest = ConvertWebToRequest(instructor);

            var _response = _service.CreateInstructor(_instructorRequest);

            return ConvertResponseToModel(_response);
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
                InstructorID = Convert.ToInt32(instructor.InstructorID),
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Mobile = instructor.Mobile,
                Email = instructor.Email,
                ID = instructor.ID,
            };

            return _studentRequest;
        }

        private contract.InstructorRequest ConvertWebToRequest(Instructor instructor)
        {
            contract.InstructorRequest _request = new contract.InstructorRequest()
            {
                InstructorID = instructor.InstructorID,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Mobile = instructor.Mobile,
                Email = instructor.Email,
            };

            return _request;
        }

        private model.Instructor ConvertResponseToModel(contract.InstructorResponse _response)
        {
            model.Instructor _instructor = new model.Instructor
            {
                InstructorID = _response.InstructorID.ToString(),
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