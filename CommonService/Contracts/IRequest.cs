using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public interface IRequest
    {
        /// <summary>
        /// Uniquely identifies inbound requests throughout the system
        /// </summary>
        Guid TrailId { get; set; }
    }
}