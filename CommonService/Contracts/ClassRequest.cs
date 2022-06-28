using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonService.Contracts
{
    public class ClassRequest : Request
    {
        public int ClassID { get; set; }
        public int CourseID { get; set; }
        public int InstructorID { get; set; }
        public string ClassTime { get; set; }
        public string ClassDate { get; set; }
        public string RoomNumber { get; set; }

        public string SelectedRow { get; set; }
    }
}