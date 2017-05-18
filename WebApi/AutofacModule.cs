using System;
using System.Reflection;
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using WebApi.Identity.Managers;
using WebApi.Identity.Providers;
using WebApi.Identity.Stores;
using WebApi.Models;
using Module = Autofac.Module;

namespace WebApi
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();
            builder.Register(e => e.Resolve<IDataProtectionProvider>().Create()).AsImplementedInterfaces();
            builder.Register(e => new DataProtectorTokenProvider<UserProfile, Guid>(e.Resolve<IDataProtector>())
            {
                TokenLifespan = TimeSpan.FromDays(1)
            }).AsImplementedInterfaces();
            builder.Register(e =>
            {
                var manager = new UserManager(e.Resolve<IUserStore>())
                {
                    EmailService = e.Resolve<EmailService>(),
                    UserTokenProvider = e.Resolve<IUserTokenProvider<UserProfile, Guid>>()
                };
                return manager;
            }).AsImplementedInterfaces();
        }
    }
}
