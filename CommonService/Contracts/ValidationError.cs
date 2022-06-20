using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
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