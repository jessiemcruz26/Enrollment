using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetInstructorHandler : RequestHandler<InstructorResponse, InstructorRequest>
    {
        private readonly EnrollmentDB _context;
        public GetInstructorHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override InstructorResponse Process(InstructorRequest request)
        {
            //get list of courses
            if (request.InstructorNumber == null)
            {
                return GetInstructors(_context);
            }
            else // get course
            {
                return GetInstructor(request, _context);
            }
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }

        private InstructorResponse GetInstructor(InstructorRequest request, EnrollmentDB context)
        {
            var _instructorRow = context.Instructors.Where(x => x.InstructorNumber == request.InstructorNumber).FirstOrDefault();

            if (_instructorRow == null)
                return new InstructorResponse();

            InstructorResponse _response = new InstructorResponse
            {
                InstructorID = _instructorRow.InstructorID,
                FirstName = _instructorRow.FirstName,
                LastName = _instructorRow.LastName,
                Email = _instructorRow.Email,
                Mobile = _instructorRow.Mobile,
                InstructorNumber = _instructorRow.InstructorNumber
            };

            return _response;
        }

        private InstructorResponse GetInstructors(EnrollmentDB context)
        {
            var _instructorRows = context.Instructors.ToList();

            var response = new InstructorResponse();
            foreach (var item in _instructorRows)
            {
                InstructorResponse _instructor = new InstructorResponse
                {
                    InstructorID = item.InstructorID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    InstructorNumber = item.InstructorNumber
                };

                response.Instructors.Add(_instructor);
            }

            return response;
        }
    }
}