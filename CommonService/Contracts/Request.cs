using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class Request : IRequest
    {
        public Guid TrailId { get; set; }
    }
}