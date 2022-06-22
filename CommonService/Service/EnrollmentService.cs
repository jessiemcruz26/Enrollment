using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Handlers;
using CommonService.Contracts;

namespace CommonService.Service
{
    public interface IEnrollmentService
    {
        StudentResponse GetStudent(StudentRequest request);
        StudentResponse CreateStudent(StudentRequest request);
        StudentsResponse GetStudents(StudentRequest request);
        StudentResponse UpdateStudent(StudentRequest request);

        InstructorResponse GetInstructor(InstructorRequest request);
        InstructorsResponse GetInstructors(InstructorRequest request);
        InstructorResponse UpdateInstructor(InstructorRequest request);
    }

    public class EnrollmentService : IEnrollmentService
    {
        public StudentResponse GetStudent(StudentRequest request)
        {
            var serviceHandler = new GetStudentHandler();
            return serviceHandler.Execute(request);
        }

        public StudentsResponse GetStudents(StudentRequest request)
        {
            var serviceHandler = new GetStudentsHandler();
            return serviceHandler.Execute(request);
        }

        public StudentResponse CreateStudent(StudentRequest request)
        {
            var serviceHandler = new CreateStudentHandler();
            return serviceHandler.Execute(request);
        }

        public StudentResponse UpdateStudent(StudentRequest request)
        {
            var serviceHandler = new UpdateStudentHandler();
            return serviceHandler.Execute(request);
        }

        public InstructorResponse GetInstructor(InstructorRequest request)
        {
            var serviceHandler = new GetInstructorHandler();
            return serviceHandler.Execute(request);
        }

        public InstructorsResponse GetInstructors(InstructorRequest request)
        {
            var serviceHandler = new GetInstructorsHandler();
            return serviceHandler.Execute(request);
        }

        public InstructorResponse UpdateInstructor(InstructorRequest request)
        {
            var serviceHandler = new UpdateInstructorHandler();
            return serviceHandler.Execute(request);
        }
    }
}