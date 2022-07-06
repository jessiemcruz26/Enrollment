using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService.Service;
using WebEnrollment.Models;
using contract = CommonService.Contracts;
using model = WebEnrollment.Models;

namespace WebEnrollment.Mediator
{
    public interface IClassMediator
    {
        Class GetClass(int ClassID);
        List<Class> GetClasses();
        Class UpdateClass(Class Class);
        Class CreateClass(Class Class);
    }

    public class ClassMediator : IClassMediator
    {
        private readonly IEnrollmentService _service;

        public ClassMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public Class GetClass(int ClassID)
        {
            var _class = _service.GetClass(new contract.ClassRequest() { ClassID = ClassID });

            return ConvertResponseToModel(_class);
        }

        public List<Class> GetClasses()
        {
            var _response = _service.GetClass(new contract.ClassRequest());

            List<Class> _classList = new List<Class>();

            foreach (var item in _response.Classes)
            {
                var model = ConvertResponseToModel(item);

                _classList.Add(model);
            }

            return _classList;
        }

        public Class UpdateClass(Class Class)
        {
            var _ClassRequest = ConvertModelToRequest(Class);

            var _response = _service.UpdateClass(_ClassRequest);

            return ConvertResponseToModel(_response);
        }

        private static List<ValidationError> MapValidationErrors(List<contract.ValidationError> errorList)
        {
            List<ValidationError> modelErrorList = new List<ValidationError>();

            if (errorList != null && errorList.Count > 0)
            {
                errorList.ForEach(error => modelErrorList.Add(new ValidationError { Code = error.Code, Message = error.Message }));
            }

            return modelErrorList;
        }

        public Class CreateClass(Class Class)
        {
            var _ClassRequest = ConvertWebToRequest(Class);

            var _response = _service.CreateClass(_ClassRequest);

            return ConvertResponseToModel(_response);
        }

        private contract.ClassRequest ConvertWebToRequest(Class _class)
        {
            contract.ClassRequest _classRequest = new contract.ClassRequest
            {
                ClassID = Convert.ToInt32(_class.ClassID),
                InstructorID = Convert.ToInt32(_class.InstructorID),
                CourseID = Convert.ToInt32(_class.CourseID),
                ClassDate = _class.ClassDate,
                ClassTime = _class.ClassTime,
                RoomNumber = _class.RoomNumber,
                ClassCode = _class.ClassCode,
                
            };

            return _classRequest;
        }

        private contract.ClassRequest ConvertModelToRequest(model.Class _class)
        {
            contract.ClassRequest _classRequest = new contract.ClassRequest()
            {
                ClassID = Convert.ToInt32(_class.ClassID),
                InstructorID = Convert.ToInt32(_class.InstructorID),
                CourseID = Convert.ToInt32(_class.CourseID),
                ClassDate = _class.ClassDate,
                ClassTime = _class.ClassTime,
                RoomNumber = _class.RoomNumber,
                ClassCode = _class.ClassCode,
            };

            return _classRequest;
        }

        private Class ConvertResponseToModel(contract.ClassResponse _response)
        {
            var _courses = GetSelectListItems(_response.Courses);

            Class _class = new Class
            {
                ClassID = _response.ClassID != 0 ? _response.ClassID.ToString() : string.Empty,
                InstructorID = _response.InstructorID,
                CourseID = _response.CourseID,
                ClassDate = _response.ClassDate,
                ClassTime = _response.ClassTime,
                RoomNumber = _response.RoomNumber,
                ClassCode = _response.ClassCode,
                Courses = GetSelectListItems(_response.Courses),
                ValidationErrors = MapValidationErrors(_response.ValidationErrors)
            };

            return _class;
        }

        private List<Course> GetSelectListItems(List<CommonService.Course> elements)
        {
            var _courses = new List<Course>();
            
            foreach (var element in elements)
            {
                var _course = new Course()
                {
                    CourseID = element.CourseID,
                    CourseName = element.CourseName
                };

                _courses.Add(_course);
            }

            return _courses;
        }
    }
}