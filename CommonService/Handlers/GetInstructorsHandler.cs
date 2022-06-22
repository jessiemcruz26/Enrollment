using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetInstructorsHandler : RequestHandler<InstructorsResponse, InstructorRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override InstructorsResponse Process(InstructorRequest request)
        {
            var context = new EnrollmentEntities();
            var _instructorRows = context.Instructors.ToList();

            var response = new InstructorsResponse();
            foreach (var item in _instructorRows)
            {
                InstructorResponse instructor = new InstructorResponse()
                {
                    InstructorID = item.InstructorID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Mobile = item.Mobile,
                    Email= item.Email
                };

                response.Instructors.Add(instructor);
            }

            return response;
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}