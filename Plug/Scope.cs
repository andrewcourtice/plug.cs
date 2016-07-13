using System;
using System.Reflection;

namespace Plug
{
    public class Scope
    {
        private readonly Guid domainKey;

        public AppDomain Domain { get; }

        public Scope()
        {
            domainKey = Guid.NewGuid();
            Domain = AppDomain.CreateDomain(domainKey.ToString());
        }

        ~Scope()
        {
            AppDomain.Unload(Domain);
        }
    }
}
