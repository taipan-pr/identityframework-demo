using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace WebApi
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();
        }
    }
}