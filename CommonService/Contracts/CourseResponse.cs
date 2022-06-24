using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class CourseResponse : Response
    {
        public CourseResponse()
        {
            Courses = new List<CourseResponse>();
        }

        public List<CourseResponse> Courses;
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
    }
}