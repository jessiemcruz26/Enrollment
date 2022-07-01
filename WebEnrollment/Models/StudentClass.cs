using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEnrollment.Models
{
    public class StudentClass : Response
    {
        public int ClassID { get; set; }
        public int StudentID { get; set; }
    }
}