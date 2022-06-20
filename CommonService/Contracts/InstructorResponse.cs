using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class InstructorResponse : Response
    {
        public List<Instructor> Instructors { get; set; }
    }
}