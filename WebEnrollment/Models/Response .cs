using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEnrollment.Models
{
    public class Response : IResponse
    {
        public Response()
        {
            ValidationErrors = new List<ValidationError>();
        }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}