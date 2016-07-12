using System;
using System.Reflection;

namespace Plug
{
    public class Scope
    {
        private readonly Guid domainKey;
        private readonly AppDomain domain;

        public Scope()
        {
            domainKey = Guid.NewGuid();
            domain = AppDomain.CreateDomain(domainKey.ToString());
        }

        public object CreateInstance(Type instanceType)
        {
            var assemblyName = Assembly.GetAssembly(instanceType).FullName;
            return Activator.CreateInstance(domain, assemblyName, instanceType.FullName);
        }

        ~Scope()
        {
            AppDomain.Unload(domain);
        }
    }
}
