using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Contracts;

namespace CommonService.Contracts
{
    public class StudentResponse : Response
    {
        public List<Student>  Students { get; set; }

    }
}