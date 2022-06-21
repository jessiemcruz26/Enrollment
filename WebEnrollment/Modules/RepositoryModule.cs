using Autofac;
using WebEnrollment.Repository;
using CommonService.Service;

namespace WebEnrollment.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataAccess>().As<IDataAccess>(); 
            builder.RegisterType<Business>().As<IBusiness>();
            builder.RegisterType<StudentMediator>().As<IStudentMediator>();
            builder.RegisterType<CourseMediator>().As<ICourseMediator>();
            builder.RegisterType<InstructorMediator>().As<IInstructorMediator>();
            builder.RegisterType<EnrollmentService>().As<IEnrollmentService>();

            base.Load(builder);
        }
    }
}