using System;
using System.Collections.Generic;
using System.Linq;
using Plug.Exceptions;

namespace Plug.Tests.Factories
{
    /// <summary>
    /// Base class for common mock resources
    /// </summary>
    public abstract class MockFactory
    {
        // Keep a list of mock registrations
        internal readonly IDictionary<Type, Type> mockDependencies;

        public MockFactory()
        {
            mockDependencies = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Register a mock type. When the factory resolves an instance, the mock copy will take precedence
        /// over the original instance
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <param name="instanceType">The instance type of this registration</param>
        public void RegisterMockDependency<T>(Type instanceType)
        {
            var registrationType = typeof(T);

            if (mockDependencies.Any(r => r.Key == registrationType))
            {
                throw new DuplicateRegistrationException(registrationType);
            }

            mockDependencies.Add(registrationType, instanceType);
        }

        /// <summary>
        /// Register a mock type. When the factory resolves an instance, the mock copy will take precedence
        /// </summary>
        /// <typeparam name="TD">The dependency type of this registration (the interface type)</typeparam>
        /// <typeparam name="TI">The instance type of this registration</typeparam>
        public void RegisterMockDependency<TD, TI>()
        {
            RegisterMockDependency<TD>(typeof(TI));
        }
    }
}
