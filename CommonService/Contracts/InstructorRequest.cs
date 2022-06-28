using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class InstructorRequest : Request
    {
        public int InstructorID { get; set; }
        public string InstructorNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string SelectedRow { get; set; }
    }
}