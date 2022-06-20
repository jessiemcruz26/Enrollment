using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public interface IResponse
    {
        List<ValidationError> ValidationErrors { get; set; }
    }
}