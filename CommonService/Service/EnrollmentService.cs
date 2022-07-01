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
        StudentResponse UpdateStudent(StudentRequest request);

        InstructorResponse GetInstructor(InstructorRequest request);
        InstructorResponse UpdateInstructor(InstructorRequest request);

        InstructorResponse CreateInstructor(InstructorRequest request);

        CourseResponse GetCourse(CourseRequest request);
        CourseResponse CreateCourse(CourseRequest request);
        CourseResponse UpdateCourse(CourseRequest request);

        ClassResponse GetClass(ClassRequest request);
        ClassResponse CreateClass(ClassRequest request);
        ClassResponse UpdateClass(ClassRequest request);

        //StudentClassResponse CreateStudentClass(StudentClassRequest request);
        //StudentClassResponse GetStudentClassByStudentID(StudentClassRequest request);
        //StudentClassResponse GetStudentClassExcludingByStudentID(StudentClassRequest request);
    }

    public class EnrollmentService : IEnrollmentService
    {
        public StudentResponse GetStudent(StudentRequest request)
        {
            var serviceHandler = new GetStudentHandler();
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

        public InstructorResponse UpdateInstructor(InstructorRequest request)
        {
            var serviceHandler = new UpdateInstructorHandler();
            return serviceHandler.Execute(request);
        }

        public InstructorResponse CreateInstructor(InstructorRequest request)
        {
            var serviceHandler = new CreateInstructorHandler();
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

        public ClassResponse GetClass(ClassRequest request)
        {
            var serviceHandler = new GetClassHandler();
            return serviceHandler.Execute(request);
        }

        public ClassResponse CreateClass(ClassRequest request)
        {
            var serviceHandler = new CreateClassHandler();
            return serviceHandler.Execute(request);
        }

        public ClassResponse UpdateClass(ClassRequest request)
        {
            var serviceHandler = new UpdateClassHandler();
            return serviceHandler.Execute(request);
        }

        //public StudentClassResponse CreateStudentClass(StudentClassRequest request)
        //{
        //    var serviceHandler = new CreateStudentClassHandler();
        //    return serviceHandler.Execute(request);
        //}

        //public StudentClassResponse GetStudentClassByStudentID(StudentClassRequest request)
        //{
        //    var serviceHandler = new GetStudentClassByStudentIDHandler();
        //    return serviceHandler.Execute(request);
        //}

        //public StudentClassResponse GetStudentClassExcludingByStudentID(StudentClassRequest request)
        //{
        //    var serviceHandler = new GetStudentClassExcludingByStudentIDHandler();
        //    return serviceHandler.Execute(request);
        //}

    }
}