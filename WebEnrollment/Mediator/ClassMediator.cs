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
        model.Class GetClass(int ClassID);
        List<model.Class> GetClasses();
        model.Class UpdateClass(model.Class Class);
        model.Class CreateClass(Class Class);
    }

    public class ClassMediator : IClassMediator
    {
        private readonly IEnrollmentService _service;

        public ClassMediator(IEnrollmentService service)
        {
            _service = service;
        }

        public model.Class GetClass(int ClassID)
        {
            var _Class = _service.GetClass(new contract.ClassRequest() { ClassID = ClassID });

            return ConvertResponseToModel(_Class);
        }

        public List<model.Class> GetClasses()
        {
            var _response = _service.GetClass(new contract.ClassRequest());

            List<model.Class> _classList = new List<model.Class>();

            foreach (var item in _response.Classes)
            {
                var model = ConvertResponseToModel(item);

                _classList.Add(model);
            }

            return _classList;
        }

        public model.Class UpdateClass(model.Class Class)
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

        public model.Class CreateClass(Class Class)
        {
            var _ClassRequest = ConvertWebToRequest(Class);

            var _response = _service.CreateClass(_ClassRequest);

            return ConvertResponseToModel(_response);
        }

        private contract.ClassRequest ConvertWebToRequest(Class _class)
        {
            contract.ClassRequest _classRequest = new contract.ClassRequest
            {
                ClassID = _class.ClassID,
                InstructorID = _class.InstructorID,
                CourseID = _class.CourseID,
                ClassDate = _class.ClassDate,
                ClassTime = _class.ClassTime,
                RoomNumber = _class.RoomNumber

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
                RoomNumber = _class.RoomNumber
            };

            return _classRequest;
        }

        private model.Class ConvertResponseToModel(contract.ClassResponse _response)
        {
            var _courses = GetSelectListItems(_response.Courses);

            model.Class _class = new model.Class
            {
                ClassID = _response.ClassID.ToString(),
                InstructorID = _response.InstructorID.ToString(),
                CourseID = _response.CourseID.ToString(),
                ClassDate = _response.ClassDate,
                ClassTime = _response.ClassTime,
                RoomNumber = _response.RoomNumber,
                Courses = GetSelectListItems(_response.Courses),
                ValidationErrors = MapValidationErrors(_response.ValidationErrors)
            };

            return _class;
        }

        //private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Class> elements)
        //{
        //    var selectList = new List<SelectListItem>();
        //    foreach (var element in elements)
        //    {
        //        selectList.Add(new SelectListItem
        //        {
        //            Value = element.ClassID.ToString(),
        //            Text = element.ClassName
        //        });
        //    }
        //    return selectList;
        //}

        private List<model.Course> GetSelectListItems(List<CommonService.Course> elements)
        {
            var _courses = new List<model.Course>();
            
            foreach (var element in elements)
            {
                var _course = new model.Course()
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