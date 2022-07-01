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
    public interface IStudentClassMediator
    {
        StudentClass CreateStudentClass(StudentClass studentClass);
    }

    public class StudentClassMediator : IStudentClassMediator
    {
        private readonly IEnrollmentService _service;

        public StudentClassMediator(IEnrollmentService service)
        {
            _service = service;
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

        public StudentClass CreateStudentClass(StudentClass studentClass)
        {
            var _studentClassRequest = ConvertWebToRequest(studentClass);

            var _response = _service.CreateStudentClass(_studentClassRequest);

            return new StudentClass(); // ConvertResponseToModel(_response);
            //return ConvertResponseToModel(_response);
        }

        private contract.StudentClassRequest ConvertWebToRequest(StudentClass _class)
        {
            contract.StudentClassRequest _classRequest = new contract.StudentClassRequest
            {
                StudentID = _class.StudentID,
                ClassID = _class.ClassID,
            };

            return _classRequest;
        }
    }
}