using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEnrollment.Models
{
    public class Class : Response
    {
        public Class(){
            Courses = new List<Course>();
        }
        
        public List<Course> Courses;
        public string ClassID { get; set; }
        public string CourseID { get; set; }
        public string InstructorID { get; set; }
        public string ClassTime { get; set; }
        public string ClassDate { get; set; }
        public string RoomNumber { get; set; }
        public string ID { get; set; }


    }
}