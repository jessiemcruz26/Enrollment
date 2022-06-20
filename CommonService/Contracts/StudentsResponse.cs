using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Contracts
{
    public class StudentsResponse : Response
    {
        public StudentsResponse()
        {
            Students = new List<StudentResponse>();
        }

        public List<StudentResponse> Students;
    }
}