using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using contract = CommonService.Contracts;
using WebEnrollment.Models;

namespace WebEnrollment.Common
{
    public static class EnrollmentHelper
    {
        public static string[] GetStudentPrograms()
        {
            string[] _programs = new string[] { "Electronics", "Mechanical", "Civil" };

            return _programs;
        }

        public static List<ValidationError> MapValidationErrors(List<contract.ValidationError> errorList)
        {
            List<ValidationError> modelErrorList = new List<ValidationError>();

            if (errorList != null && errorList.Count > 0)
            {
                errorList.ForEach(error => modelErrorList.Add(new ValidationError { Code = error.Code, Message = error.Message }));
            }

            return modelErrorList;
        }
    }
}