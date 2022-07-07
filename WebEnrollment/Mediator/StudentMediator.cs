using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CommonService.Service;
using WebEnrollment.Models;
using contract = CommonService.Contracts;
using WebEnrollment.Common;

namespace WebEnrollment.Mediator
{
    public interface IStudentMediator
    {
        Student GetStudent(string studentNumber);
        List<Student> GetStudents();
        Student UpdateStudent(Student student);
        Student UpdateStudentGrid(Student student);
        Student CreateStudent(Student student);
    }

    public class StudentMediator : IStudentMediator
    {
        private readonly IEnrollmentService _service;
        public StudentMediator(IEnrollmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get student using studentNumber from enrollment service
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns></returns>
        public Student GetStudent(string studentNumber)
        {
            var _response = _service.GetStudent(new contract.StudentRequest() { StudentNumber = studentNumber });

            if (_response.StudentID == 0)
                return new Student { IsStudentFound = false };

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Get list of students from service
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            var _response = _service.GetStudent(new contract.StudentRequest());

            List<Student> _studentsList = new List<Student>();

            foreach (var item in _response.Students)
            {
                var model = ConvertResponseToModel(item);

                _studentsList.Add(model);
            }

            return _studentsList;
        }

        /// <summary>
        /// Update student record using service
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student UpdateStudent(Student student)
        {
            var _studentRequest = ConvertModelToRequest(student);

            contract.StudentResponse _response = _service.UpdateStudent(_studentRequest);

            if (_response.ValidationErrors.Any())
            {
                var _responseGet = _service.GetStudent(new contract.StudentRequest { StudentNumber = student.StudentNumber });

                foreach (var item in _response.ValidationErrors)
                {
                    switch (item.Code)
                    {
                        case "FirstName":
                            _responseGet.FirstName = string.Empty;
                            break;
                        case "LastName":
                            _responseGet.LastName = string.Empty;
                            break;
                        case "StudentNumber":
                            _responseGet.StudentNumber = string.Empty;
                            break;
                    }
                }

                _responseGet.ValidationErrors = _response.ValidationErrors;
                return ConvertResponseToModel(_responseGet);
            }

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Update student record in Grid using service
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student UpdateStudentGrid(Student student)
        {
            var _studentRequest = ConvertModelToRequest(student);

            contract.StudentResponse _response = _service.UpdateStudentGrid(_studentRequest);

            return ConvertResponseToModel(_response);
        }

        /// <summary>
        /// Get program list 
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        private List<SelectListItem> GetProgramList(string program)
        {
            string[] _programs = EnrollmentHelper.GetStudentPrograms();

            var _programListItems = new List<SelectListItem>();

            foreach (var item in _programs)
            {
                _programListItems.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item,
                    Selected = item == program
                });
            }

            return _programListItems;
        }

        /// <summary>
        /// Create student using enrollment service
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student CreateStudent(Student student)
        {
            var _studentRequest = ConvertModelToRequest(student);

            var _response = _service.CreateStudent(_studentRequest);



            return ConvertResponseToModel(_response);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="student"></param>
        ///// <returns></returns>
        //private contract.StudentRequest ConvertWebToRequest(Student student)
        //{
        //    contract.StudentRequest _studentRequest = new contract.StudentRequest()
        //    {
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        Address = student.Address,
        //        Mobile = student.Mobile,
        //        Birthday = null,
        //        Email = student.Email,
        //        Program = student.Program,
        //        StudentNumber = student.StudentNumber,
        //        StudentID = student.StudentID != null ? Convert.ToInt32(student.StudentID) : 0,
        //        Level = student.Level,
        //    };

        //    return _studentRequest;
        //}

        /// <summary>
        /// Map web model to service model
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private contract.StudentRequest ConvertModelToRequest(Student student)
        {
            contract.StudentRequest _studentRequest = new contract.StudentRequest()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                Mobile = student.Mobile,
                Birthday = !string.IsNullOrEmpty(student.Birthday) ? (DateTime?)(DateTime.Parse((student.Birthday))) : null,
                Email = student.Email,
                Program = student.Program,
                StudentNumber = student.StudentNumber,
                StudentID = !string.IsNullOrEmpty(student.StudentID) ? Convert.ToInt32(student.StudentID) : 0,
                Level = student.Level,
                SelectedRow = student.SelectedRow,
                ClassIds = student.ClassIds,
                ClassSelection = student.ClassSelection,
            };

            return _studentRequest;
        }

        /// <summary>
        /// Map service model to web model
        /// </summary>
        /// <param name="_response"></param>
        /// <returns></returns>
        private Student ConvertResponseToModel(contract.StudentResponse _response)
        {
            Student _student = new Student
            {
                FirstName = _response.FirstName,
                LastName = _response.LastName,
                Address = _response.Address,
                Mobile = _response.Mobile,
                Birthday = _response.Birthday != null ? Convert.ToDateTime(_response.Birthday).ToShortDateString() : null,
                Email = _response.Email,
                Program = _response.Program,
                StudentNumber = _response.StudentNumber,
                StudentID = _response.StudentID.ToString(),
                Level = _response.Level,
                AssociatedClasses = MapCommonClasstoClass(_response.AssociatedClasses),
                UnassociatedClasses = MapCommonClasstoClass(_response.UnassociatedClasses),

                ProgramListItems = GetProgramList(_response.Program),
                ValidationErrors = EnrollmentHelper.MapValidationErrors(_response.ValidationErrors),
            };

            return _student;
        }

        /// <summary>
        /// Map common classes to classes 
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        private List<Class> MapCommonClasstoClass(List<CommonService.Class> classes)
        { 
            List<Class> _classList = new List<Class>();
            foreach (var item in classes)
            {
                Class _class = new Class()
                {
                    ClassCode = item.ClassCode,
                    ClassDate = item.ClassDate,
                    ClassTime = item.ClassTime,
                    RoomNumber = item.RoomNumber,
                    ClassID = item.ClassID.ToString(),
                    CourseName = item.Course.CourseName,
                };

                _classList.Add(_class);
            }

            return _classList;
        }
    }
}