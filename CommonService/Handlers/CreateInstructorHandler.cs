using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateInstructorHandler : RequestHandler<InstructorResponse, InstructorRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override InstructorResponse Process(InstructorRequest request)
        {
            var context = new EnrollmentEntities();
            var _courseRows = context.Instructors.ToList();

            var respone = new InstructorResponse();
            var instructorList = new List<Contracts.Instructor>();
            foreach (var item in _courseRows)
            {
                Contracts.Instructor instructor = new Contracts.Instructor()
                {
                   InstructorID = item.InstructorID,
                   FirstName = item.FirstName,
                   LastName = item.LastName,
                   Mobile = item.Mobile,
                   Email = item.Email,
                };

                instructorList.Add(instructor);
            }

            respone.Instructors = instructorList;

            return respone;
        }

        protected override List<ValidationError> Validate(InstructorRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}