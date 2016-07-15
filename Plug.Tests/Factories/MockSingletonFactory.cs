using System.Linq;
using Plug.Factories;
using Plug.Core;

namespace Plug.Tests.Factories
{
    public class MockSingletonFactory : MockFactory, IFactory
    {
        public InstanceConstructor GenerateInstanceConstructor(Registration registration)
        {
            var instanceType = registration.InstanceType;

            if (mockDependencies.Any(md => md.Key == registration.RegistrationType))
            {
                instanceType = mockDependencies.Single(md => md.Key == registration.RegistrationType).Value;
            }

            return Compiler.CompileInstance(instanceType);
        }

        public object Resolve(Registration registration, object[] args = null)
        {
            if (registration.HasInstance)
            {
                return null;
            }

            return registration.InstanceConstructor(args);
        }
    }
}
