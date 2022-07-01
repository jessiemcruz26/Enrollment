using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetStudentClassByStudentIDHandler : RequestHandler<StudentClassResponse, StudentClassRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentClassResponse Process(StudentClassRequest request)
        {
            var context = new EnrollmentDB();

            var _studentClassRows = context.StudentClasses.Where(x => x.StudentID == request.StudentID).ToList();

            var response = new StudentClassResponse()
            {
                StudentClassList = _studentClassRows
            };

            return response;
        }

        protected override List<ValidationError> Validate(StudentClassRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}