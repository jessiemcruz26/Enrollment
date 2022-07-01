using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Contracts
{
    public class StudentClassResponse : Response
    {
        public StudentClassResponse()
        {
            StudentClassList = new List<StudentClass>();
        }
        public int StudentID { get; set; }
        public int ClassID { get; set; }

        public List<StudentClass> StudentClassList;
    }
}