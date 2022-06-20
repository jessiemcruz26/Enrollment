using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class CourseResponse : Response
    {
        public List<Course> Courses { get; set; }
    }
}