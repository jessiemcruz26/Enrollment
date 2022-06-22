using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class InstructorsResponse : Response
    {
        public InstructorsResponse()
        {
            Instructors = new List<InstructorResponse>();
        }

        public List<InstructorResponse> Instructors { get; set; }
    }
}