using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using WebEnrollment.Modules;

namespace WebEnrollment.App_Start
{
    public class AutoConfig
    {
        public static void ConfigContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register our Repo dependencies
            builder.RegisterModule(new RepositoryModule());

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}