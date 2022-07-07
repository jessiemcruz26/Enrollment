using System;
using System.Collections.Generic;
using System.Linq;
using WebEnrollment.Models;
using CommonService.Service;
using contract = CommonService.Contracts;
using WebEnrollment.Common;

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

        /// <summary>
        /// Retrieve instructor using instructorNumber from service
        /// </summary>
        /// <param name="instructorNumber"></param>
        /// <returns></returns>
        public Instructor GetInstructor(string instructorNumber)
        {
            var _response = _service.GetInstructor(new contract.InstructorRequest() { InstructorNumber = instructorNumber });

            if (_response.InstructorID == 0)
                return new Instructor { IsInstructorFound = false };

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Get list of instructors from service
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Create instructor using service
        /// </summary>
        /// <param name="instructor"></param>
        /// <returns></returns>
        public Instructor CreateInstructor(Instructor instructor)
        {
            var _instructorRequest = ConvertModelToRequest(instructor);

            var _response = _service.CreateInstructor(_instructorRequest);

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Update instructor using service
        /// </summary>
        /// <param name="instructor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Map web model to service model
        /// </summary>
        /// <param name="instructor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Map service model to web model
        /// </summary>
        /// <param name="_response"></param>
        /// <returns></returns>
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
                ValidationErrors = EnrollmentHelper.MapValidationErrors(_response.ValidationErrors)
            };

            return _instructor;
        }
    }
}