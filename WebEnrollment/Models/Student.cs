using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEnrollment.Models
{
    public class Student
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
    }
}