using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace WebApi.Identity.Providers
{
    public static class OwinContextExtensions
    {
        public static IComponentContext GetComponentContext(this IDictionary<string, object> environment)
        {
            return (from o in environment
                    where o.Key.Contains("autofac:OwinLifetimeScope")
                    select (IComponentContext)o.Value)
                .FirstOrDefault();
        }
    }
}
