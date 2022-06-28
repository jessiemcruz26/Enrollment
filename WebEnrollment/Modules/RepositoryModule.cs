using Autofac;
using WebEnrollment.Mediator;
using CommonService.Service;

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

            base.Load(builder);
        }
    }
}