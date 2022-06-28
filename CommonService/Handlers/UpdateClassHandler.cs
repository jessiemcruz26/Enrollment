using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateClassHandler : RequestHandler<ClassResponse, ClassRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override ClassResponse Process(ClassRequest request)
        {
            var context = new EnrollmentDB();

            var _Class = context.Classes.Where(x => x.ClassID == request.ClassID).FirstOrDefault();

            if (string.IsNullOrEmpty(request.SelectedRow))
            {
                UpdateClass(request, _Class, context);
            }
            else
            {
                DeleteClass(_Class, context);

                return new ClassResponse();
            }

            var _classResponse = new ClassResponse
            {
                ClassID = request.ClassID,
                InstructorID = request.InstructorID.ToString(),
                CourseID = request.CourseID.ToString(),
                ClassDate = request.ClassDate,
                ClassTime = request.ClassTime,
                RoomNumber = request.RoomNumber
            };

            return _classResponse;
        }

        protected override List<ValidationError> Validate(ClassRequest request)
        {
            var validationErrors = new List<ValidationError>();

            return validationErrors;
        }

        private void UpdateClass(ClassRequest request, Class _class, EnrollmentDB context)
        {
            _class.ClassID = request.ClassID;
            _class.InstructorID = request.InstructorID;
            _class.CourseID = request.CourseID;
            _class.ClassDate = request.ClassDate;
            _class.ClassTime = request.ClassTime;
            _class.RoomNumber = request.RoomNumber;

            context.SaveChanges();
        }

        private void DeleteClass(Class Class, EnrollmentDB context)
        {
            context.Classes.Remove(Class);

            context.SaveChanges();
        }
    }
}