using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Contracts
{
    public class StudentResponse : Response
    {
        public StudentResponse()
        {
            Students = new List<StudentResponse>();
        }

        public List<StudentResponse> Students;

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
        public DateTime? Birthday { get; set; }
    }
}