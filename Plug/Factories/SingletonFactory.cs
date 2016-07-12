using System;
using System.Reflection;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating singleton classes
    /// </summary>
    public class SingletonFactory : IFactory
    {
        public virtual void Resolve(Registration registration)
        {
            if (registration.Instance == null)
            {
                var assemblyName = Assembly.GetAssembly(registration.InstanceType).FullName;
                registration.Instance = Activator.CreateInstance(registration.Domain, assemblyName, registration.InstanceType.Name);
            }
        }
    }
}
