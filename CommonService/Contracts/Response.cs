﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class Response : IResponse
    {
        public List<ValidationError> ValidationErrors { get; set; }
    }
}