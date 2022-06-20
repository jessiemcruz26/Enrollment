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
            var _instructorRows = context.Instructors.ToList();

            var respone = new InstructorResponse();
            var instructortList = new List<Contracts.Instructor>();
            foreach (var item in _instructorRows)
            {
                Contracts.Instructor instructor = new Contracts.Instructor()
                {
                   InstructorID = item.InstructorID,
                   FirstName = item.FirstName,
                   LastName = item.LastName,
                   Email = item.Email,
                   Mobile = item.Mobile
                };

                instructortList.Add(instructor);
            }

            respone.Instructors = instructortList;

            return respone;
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}