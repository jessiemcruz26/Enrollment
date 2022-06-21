using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEnrollment.Models
{
    public interface IResponse
    {
        List<ValidationError> ValidationErrors { get; set; }
    }
}