using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class GetStudentHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentDB();

            //get list of courses
            if (request.StudentNumber == null)
            {
                return GetStudents(context);
            }
            else // get course
            {
                return GetStudent(request, context);
            }
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();
            return validationErrors;
        }

        private StudentResponse GetStudent(StudentRequest request, EnrollmentDB context)
        {
            var _studentRow = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();

            if (_studentRow == null)
                return new StudentResponse();

            var _associatedClasses = context.StudentClasses.Where(x => x.StudentID == _studentRow.StudentID).ToList();
            var _allClasses = context.Classes.ToList();

            List<Class> _associatedClasslist = new List<Class>();
            List<Class> _unAssociatedClasslist = new List<Class>();

            foreach (var item in _associatedClasses)
            {
                var _assoClass = context.Classes.Where(x => x.ClassID == item.ClassID).First();
                _associatedClasslist.Add(_assoClass);
            }
           
            _unAssociatedClasslist = _allClasses.Except(_associatedClasslist).ToList();

            StudentResponse _response = new StudentResponse
            {
                FirstName = _studentRow.FirstName,
                LastName = _studentRow.LastName,
                Email = _studentRow.Email,
                Mobile = _studentRow.Mobile,
                Level = _studentRow.Level,
                Program = _studentRow.Program,
                StudentID = _studentRow.StudentID,
                StudentNumber = _studentRow.StudentNumber,
                Address = _studentRow.Address,
                Birthday = _studentRow?.Birthday,
                AssociatedClasses = _associatedClasslist,
                UnassociatedClasses = _unAssociatedClasslist
            };

            return _response;
        }

        private StudentResponse GetStudents(EnrollmentDB context)
        {
            var _studentRow = context.Students.ToList();

            var response = new StudentResponse();
            foreach (var item in _studentRow)
            {
                StudentResponse _student = new StudentResponse
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    Level = item.Level,
                    Program = item.Program,
                    StudentID = item.StudentID,
                    StudentNumber = item.StudentNumber,
                    Address = item.Address,
                    Birthday = item?.Birthday,
                };

                response.Students.Add(_student);
            }

            return response;
        }
       
    }
}