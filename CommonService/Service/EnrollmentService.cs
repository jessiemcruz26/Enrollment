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

        CourseResponse GetCourse(CourseRequest request);
        CourseResponse CreateCourse(CourseRequest request);
        CourseResponse UpdateCourse(CourseRequest request);
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

        public CourseResponse GetCourse(CourseRequest request)
        {
            var serviceHandler = new GetCourseHandler();
            return serviceHandler.Execute(request);
        }

        public CourseResponse CreateCourse(CourseRequest request)
        {
            var serviceHandler = new CreateCourseHandler();
            return serviceHandler.Execute(request);
        }

        public CourseResponse UpdateCourse(CourseRequest request)
        {
            var serviceHandler = new UpdateCourseHandler();
            return serviceHandler.Execute(request);
        }
    }
}