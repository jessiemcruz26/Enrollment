using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class StudentClassRequest : Request
    {
        public int StudentID { get; set; }
        public int ClassID { get; set; }
    }
}