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

            var _instructorRow = context.Instructors.Where(x => x.InstructorID == request.InstructorID).FirstOrDefault();

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

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}