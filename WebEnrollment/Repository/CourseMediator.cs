using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Service;
using CommonService.Contracts;

namespace WebEnrollment.Repository
{
    public interface ICourseMediator
    {
        //StudentResponse GetStudents();

        //void Save();
    }

    public class CourseMediator : ICourseMediator
    {
        private readonly ICourseMediator _mediator;
        public CourseMediator(ICourseMediator mediator)
        {
            _mediator = mediator;
        }
    }
}