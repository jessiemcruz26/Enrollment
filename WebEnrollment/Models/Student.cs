using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEnrollment.Models
{
    public class Student : Response
    {
        public int MyProperty { get; set; }
        public int StudentID { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Program { get; set; }
        public string Level { get; set; }
        public string Birthday { get; set; }

        public IEnumerable<SelectListItem> CourseListItems { get; set; }
        public string CourseID { get; set; }
    }

    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
    }

}