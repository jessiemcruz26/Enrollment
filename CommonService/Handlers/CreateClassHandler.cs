using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class CreateClassHandler : RequestHandler<ClassResponse, ClassRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override ClassResponse Process(ClassRequest request)
        {
            var context = new EnrollmentDB();

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

            context.Classes.Add(_class);
            context.SaveChanges();

            return new ClassResponse();
        }

        protected override List<ValidationError> Validate(ClassRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }
    }
}