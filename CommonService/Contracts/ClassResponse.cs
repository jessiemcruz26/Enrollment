using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class ClassResponse : Response
    {
        public ClassResponse()
        {
            Classes = new List<ClassResponse>();
            Courses = new List<Course>();
        }

        public List<ClassResponse> Classes;
        public List<Course> Courses;
        public int ClassID { get; set; }
        public string CourseID { get; set; }
        public string InstructorID { get; set; }
        public string ClassTime { get; set; }
        public string ClassDate { get; set; }
        public string RoomNumber { get; set; }
    }
}