using System;
using System.Reflection;

namespace Plug
{
    public class Scope
    {
        private readonly Guid _domainKey;
        private readonly AppDomain _domain;

        public Scope()
        {           
            _domainKey = Guid.NewGuid();
            var domainSetup = new AppDomainSetup()
            {
                ApplicationBase = Environment.CurrentDirectory
            };

            var domainEvidence = AppDomain.CurrentDomain.Evidence;

            _domain = AppDomain.CreateDomain(_domainKey.ToString(), domainEvidence, domainSetup);
        }

        public object CreateObject(Type objectType)
        {
            var assemblyName = Assembly.GetExecutingAssembly().FullName;
            return _domain.CreateInstanceAndUnwrap(objectType.Assembly.FullName, objectType.FullName);
        }

        public object CreateObject(Type objectType, params object[] args)
        {
            var assemblyName = Assembly.GetExecutingAssembly().FullName;
            return _domain.CreateInstanceAndUnwrap(objectType.Assembly.FullName, objectType.FullName, false, BindingFlags.Default, null, args, null, null);
        }

        public T CreateObject<T>()
        {
            return (T) CreateObject(typeof(T));
        }

        public T CreateObject<T>(params object[] args)
        {
            return (T) CreateObject(typeof(T), args);
        }

        ~Scope()
        {
            if (!_domain.IsFinalizingForUnload())
            {
                AppDomain.Unload(_domain);
            }
        }
    }
}
