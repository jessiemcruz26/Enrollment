using Autofac;
using WebEnrollment.Mediator;
using CommonService.Service;
using CommonService;
using CommonService.Handlers;

namespace WebEnrollment.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentMediator>().As<IStudentMediator>();
            builder.RegisterType<CourseMediator>().As<ICourseMediator>();
            builder.RegisterType<InstructorMediator>().As<IInstructorMediator>();
            builder.RegisterType<ClassMediator>().As<IClassMediator>();
            builder.RegisterType<EnrollmentService>().As<IEnrollmentService>();
            builder.RegisterType<UpdateStudentHandler>();
            builder.RegisterType<GetStudentHandler>();
            builder.RegisterType<CreateStudentHandler>();
            builder.RegisterType<UpdateStudentGridHandler>();
            builder.RegisterType<GetInstructorHandler>();
            builder.RegisterType<UpdateInstructorHandler>();
            builder.RegisterType<CreateInstructorHandler>();
            builder.RegisterType<GetCourseHandler>();
            builder.RegisterType<UpdateCourseHandler>();
            builder.RegisterType<CreateCourseHandler>();
            builder.RegisterType<GetClassHandler>();
            builder.RegisterType<UpdateClassHandler>();
            builder.RegisterType<CreateClassHandler>();

            builder.RegisterType<EnrollmentDB>();

            base.Load(builder);
        }
    }
}