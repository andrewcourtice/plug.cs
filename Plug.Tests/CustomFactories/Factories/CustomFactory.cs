using System;
using System.Collections.Generic;
using System.Linq;
using Plug.Core;
using Plug.Exceptions;
using Plug.Factories;

namespace Plug.Tests.CustomFactories.Factories
{
    /// <summary>
    /// Base class for common mock resources
    /// </summary>
    public class CustomFactory : IFactory
    {
        // Keep a list of mock registrations
        internal readonly IDictionary<Type, Type> mockDependencies;

        public CustomFactory()
        {
            mockDependencies = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Register a mock type. When the factory resolves an instance, the mock copy will take precedence
        /// over the original instance
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <param name="instanceType">The instance type of this registration</param>
        public void RegisterMockDependency<T>(Registration registration)
        {
            var instanceType = typeof(T);

            if (mockDependencies.Any(r => r.Key == registration.RegistrationType))
            {
                throw new DuplicateRegistrationException(registration.RegistrationType);
            }

            mockDependencies.Add(registration.RegistrationType, instanceType);

            registration.Update(instanceType);
        }

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
