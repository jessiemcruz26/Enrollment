using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateClassHandler : RequestHandler<ClassResponse, ClassRequest>
    {
        private readonly EnrollmentDB _context;

        public UpdateClassHandler(EnrollmentDB context)
        {
            _context = context;
        }

        protected override ClassResponse Process(ClassRequest request)
        {
            if (request.ClassID == 0)
                request.ClassID = Convert.ToInt32(request.SelectedRow);

            var _class = _context.Classes.Where(x => x.ClassID == request.ClassID).FirstOrDefault();

            if (string.IsNullOrEmpty(request.SelectedRow))
            {
                UpdateClass(request, _class, _context);
            }
            else
            {
                DeleteClass(_class, _context);

                return new ClassResponse();
            }

            var _classResponse = new ClassResponse
            {
                ClassID = request.ClassID,
                InstructorID = request.InstructorID.ToString(),
                CourseID = request.CourseID.ToString(),
                ClassDate = request.ClassDate,
                ClassTime = request.ClassTime,
                RoomNumber = request.RoomNumber,
                ClassCode = request.ClassCode
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
            _class.ClassCode = request.ClassCode;

            context.SaveChanges();
        }

        private void DeleteClass(Class Class, EnrollmentDB context)
        {
            context.Classes.Remove(Class);

            context.SaveChanges();
        }
    }
}