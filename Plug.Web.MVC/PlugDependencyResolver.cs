using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Plug.Web.MVC
{
    public class PlugDependencyResolver : IDependencyResolver
    {
        public Container Container { get; }

        public PlugDependencyResolver(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || !typeof(IController).IsAssignableFrom(serviceType))
            {
                throw new Exception("Invalid controller registration");
            }

            return Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}
