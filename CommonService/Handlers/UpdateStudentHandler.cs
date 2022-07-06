using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public class UpdateStudentHandler : RequestHandler<StudentResponse, StudentRequest>
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override StudentResponse Process(StudentRequest request)
        {
            var context = new EnrollmentDB();

            Student _student = null;

            if (request.ClassIds != null && request.ClassIds.Any())
            {
                foreach (var item in request.ClassIds)
                {
                    int _classId = Convert.ToInt32(item);
                    var _studentClass = context.StudentClasses.Where(x => x.ClassID == _classId && x.StudentID == request.StudentID).First();
                    context.StudentClasses.Remove(_studentClass);
                }
            }

            _student = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();

            if (!string.IsNullOrEmpty(request.ClassSelection))
                context.StudentClasses.Add(new StudentClass
                {
                    ClassID = Convert.ToInt32(request.ClassSelection),
                    StudentID = request.StudentID
                });

            UpdateStudent(request, _student, context);
        
            var _studentRow = context.Students.Where(x => x.StudentNumber == request.StudentNumber).FirstOrDefault();
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

            var _studentResponse = new StudentResponse()
            {
                Address = _student.Address,
                Birthday = _student.Birthday,
                Email = _student.Email,
                FirstName = _student.FirstName,
                LastName = _student.LastName,
                Mobile = _student.Mobile,
                Level = _student.Level,
                Program = _student.Program,
                StudentID = _student.StudentID,
                StudentNumber = _student.StudentNumber,
                AssociatedClasses = _associatedClasslist,
                UnassociatedClasses = _unAssociatedClasslist
            };

            return _studentResponse;
        }

        protected override List<ValidationError> Validate(StudentRequest request)
        {
            var validationErrors = new List<ValidationError>();

            if (string.IsNullOrEmpty(request.SelectedRow))
            {
                if (string.IsNullOrEmpty(request.StudentNumber))
                    validationErrors.Add(new ValidationError { Code = "StudentNumber", Message = "Student Number must have a value" });

                if (string.IsNullOrEmpty(request.FirstName))
                    validationErrors.Add(new ValidationError { Code = "FirstName", Message = "First Name must have a value" });

                if (string.IsNullOrEmpty(request.LastName))
                    validationErrors.Add(new ValidationError { Code = "LastName", Message = "Last Name must have a value" });
            }

            return validationErrors;
        }

        private void UpdateStudent(StudentRequest request, Student student, EnrollmentDB context)
        {
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.Mobile = request.Mobile;
            student.Address = request.Address;
            student.Birthday = request.Birthday;

            context.SaveChanges();
        }

        private void DeleteStudent(Student student, EnrollmentDB context)
        {
            context.Students.Remove(student);

            context.SaveChanges();
        }
    }
}