using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetInstructorHandler : RequestHandler<InstructorResponse, InstructorRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override InstructorResponse Process(InstructorRequest request)
        {
            var context = new EnrollmentEntities();

            //get list of courses
            if (request.ID == null)
            {
                return GetInstructors(context);
            }
            else // get course
            {
                return GetInstructor(request, context);
            }
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }

        private InstructorResponse GetInstructor(InstructorRequest request, EnrollmentEntities context)
        {
            int _id = Convert.ToInt32(request.ID);
            var _instructorRow = context.Instructors.Where(x => x.InstructorID == _id).FirstOrDefault();

            InstructorResponse _response = new InstructorResponse
            {
                InstructorID = _instructorRow.InstructorID,
                FirstName = _instructorRow.FirstName,
                LastName = _instructorRow.LastName,
                Email = _instructorRow.Email,
                Mobile = _instructorRow.Mobile,
            };

            return _response;
        }

        private InstructorResponse GetInstructors(EnrollmentEntities context)
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
                };

                response.Instructors.Add(_instructor);
            }

            return response;
        }
    }
}