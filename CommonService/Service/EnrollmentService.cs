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
        StudentResponse UpdateStudentGrid(StudentRequest request);
        
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly UpdateStudentHandler _updateStudentHandler;
        private readonly GetStudentHandler _getStudentHandler;
        private readonly CreateStudentHandler _createStudentHandler;
        private readonly UpdateStudentGridHandler _updateStudentGridHandler;
        private readonly GetInstructorHandler _getInstructorHandler;
        private readonly UpdateInstructorHandler _updateInstructorHandler;
        private readonly CreateInstructorHandler _createInstructorHandler;
        private readonly GetCourseHandler _getCourseHandler;
        private readonly CreateCourseHandler _createCourseHandler;
        private readonly UpdateCourseHandler _updateCourseHandler;
        private readonly GetClassHandler _getClassHandler;
        private readonly CreateClassHandler _createClassHandler;
        private readonly UpdateClassHandler _updateClassHandler;

        public EnrollmentService(UpdateStudentHandler updateStudentHandler, GetStudentHandler getStudentHandler, 
            CreateStudentHandler createStudentHandler, UpdateStudentGridHandler updateStudentGridHandler, GetInstructorHandler getInstructorHandler, 
            UpdateInstructorHandler updateInstructorHandler, CreateInstructorHandler createInstructorHandler, GetCourseHandler getCourseHandler, 
            CreateCourseHandler createCourseHandler, UpdateCourseHandler updateCourseHandler, GetClassHandler getClassHandler, 
            CreateClassHandler createClassHandler, UpdateClassHandler updateClassHandler)
        {
            _updateStudentHandler = updateStudentHandler;
            _getStudentHandler = getStudentHandler;
            _createStudentHandler = createStudentHandler;
            _updateStudentGridHandler = updateStudentGridHandler;
            _getInstructorHandler = getInstructorHandler;
            _updateInstructorHandler = updateInstructorHandler;
            _createInstructorHandler = createInstructorHandler;
            _getCourseHandler = getCourseHandler;
            _createCourseHandler = createCourseHandler;
            _updateCourseHandler = updateCourseHandler;
            _getClassHandler = getClassHandler;
            _createClassHandler = createClassHandler;
            _updateClassHandler = updateClassHandler;
        }

        public StudentResponse GetStudent(StudentRequest request)
        {
            return _getStudentHandler.Execute(request);
        }


        public StudentResponse CreateStudent(StudentRequest request)
        {
            return _createStudentHandler.Execute(request);
        }

        public StudentResponse UpdateStudent(StudentRequest request)
        {
            return _updateStudentHandler.Execute(request);
        }

        public StudentResponse UpdateStudentGrid(StudentRequest request)
        {
            return _updateStudentGridHandler.Execute(request);
        }

        public InstructorResponse GetInstructor(InstructorRequest request)
        {
            return _getInstructorHandler.Execute(request);
        }

        public InstructorResponse UpdateInstructor(InstructorRequest request)
        {
            return _updateInstructorHandler.Execute(request);
        }

        public InstructorResponse CreateInstructor(InstructorRequest request)
        {
            return _createInstructorHandler.Execute(request);
        }

        public CourseResponse GetCourse(CourseRequest request)
        {
            return _getCourseHandler.Execute(request);
        }

        public CourseResponse CreateCourse(CourseRequest request)
        {
            return _createCourseHandler.Execute(request);
        }

        public CourseResponse UpdateCourse(CourseRequest request)
        {
            return _updateCourseHandler.Execute(request);
        }

        public ClassResponse GetClass(ClassRequest request)
        {
            return _getClassHandler.Execute(request);
        }

        public ClassResponse CreateClass(ClassRequest request)
        {
            return _createClassHandler.Execute(request);
        }

        public ClassResponse UpdateClass(ClassRequest request)
        {
            return _updateClassHandler.Execute(request);
        }
    }
}