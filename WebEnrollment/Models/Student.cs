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
        public Student()
        {
            AssociatedClasses = new List<Class>();
            UnassociatedClasses = new List<Class>();
        }

        public int MyProperty { get; set; }
        public string StudentID { get; set; }
        public string StudentNumberSearch { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Program { get; set; }
        public string Level { get; set; }
        public string Birthday { get; set; }
        public string ClassSelection { get; set; }

        public IEnumerable<SelectListItem> ProgramListItems { get; set; }
        public List<Class> AssociatedClasses { get; set; }
        public List<Class> UnassociatedClasses { get; set; }

        public String[] ClassIds { get; set; }

        public string SelectedRow { get; set; }
    }

}