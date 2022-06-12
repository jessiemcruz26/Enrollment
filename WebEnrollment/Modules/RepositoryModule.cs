using Autofac;
using WebEnrollment.Repository;

namespace WebEnrollment.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataAccess>().As<IDataAccess>(); 
            builder.RegisterType<Business>().As<IBusiness>(); 

            base.Load(builder);
        }
    }
}