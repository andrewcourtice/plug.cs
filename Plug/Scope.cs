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

        public object CreateObject(Type objectType)
        {
            var assemblyName = Assembly.GetExecutingAssembly().FullName;
            return domain.CreateInstanceAndUnwrap(assemblyName, objectType.FullName);
        }

        public object CreateObject(Type objectType, params object[] args)
        {
            var assemblyName = Assembly.GetExecutingAssembly().FullName;
            return domain.CreateInstanceAndUnwrap(assemblyName, objectType.FullName, false, BindingFlags.Default, null, args, null, null);
        }

        public T CreateObject<T>()
        {
            return (T) CreateObject(typeof(T));
        }

        public T CreateObject<T>(params object[] args)
        {
            return (T)CreateObject(typeof(T), args);
        }

        ~Scope()
        {
            AppDomain.Unload(domain);
        }
    }
}
