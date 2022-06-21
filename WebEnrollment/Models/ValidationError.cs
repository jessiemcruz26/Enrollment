using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEnrollment.Models
{
    public class ValidationError
    {
        public string Code { get; set; }

        /// <summary>
        /// Property that holds validation error message
        /// </summary>
        public string Message { get; set; }
    }
}