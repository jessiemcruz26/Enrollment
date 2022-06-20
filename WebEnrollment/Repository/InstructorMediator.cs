using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Service;
using CommonService.Contracts;

namespace WebEnrollment.Repository
{
    public interface IInstructorMediator
    {
        //StudentResponse GetStudents();

        //void Save();
    }

    public class InstructorMediator : IInstructorMediator
    {
        private readonly IInstructorMediator _mediator;
        public InstructorMediator(IInstructorMediator mediator)
        {
            _mediator = mediator;
        }



    }
}