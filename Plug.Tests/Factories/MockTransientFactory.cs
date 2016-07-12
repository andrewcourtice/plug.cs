using System;
using System.Linq;
using Plug.Factories;

namespace Plug.Tests.Factories
{
    public class MockTransientFactory : MockFactory, IFactory
    {
        public void Resolve(Registration registration)
        {
            var instanceType = registration.InstanceType;

            if (mockDependencies.Any(md => md.Key == registration.RegistrationType))
            {
                instanceType = mockDependencies.Single(md => md.Key == registration.RegistrationType).Value;
            }

            registration.Instance = Activator.CreateInstance(instanceType);
        }
    }
}
