using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateClassHandler : RequestHandler<ClassResponse, ClassRequest>
    {
        private readonly EnrollmentDB _context;

        public CreateClassHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override ClassResponse Process(ClassRequest request)
        {
            Class _class = new Class()
            {
                ClassID = request.ClassID,
                InstructorID = request.InstructorID,
                CourseID = request.CourseID,
                ClassDate = request.ClassDate,
                ClassTime = request.ClassTime,
                RoomNumber = request.RoomNumber,
                ClassCode = request.ClassCode,
            };

            _context.Classes.Add(_class);
            _context.SaveChanges();

            return new ClassResponse();
        }

        protected override List<ValidationError> Validate(ClassRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}