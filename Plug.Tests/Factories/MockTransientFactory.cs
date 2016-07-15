using System;
using System.Linq;
using Plug.Core;
using Plug.Factories;

namespace Plug.Tests.Factories
{
    public class MockTransientFactory : MockFactory, IFactory
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
            return registration.InstanceConstructor(args);
        }
    }
}
