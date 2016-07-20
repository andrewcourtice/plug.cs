using Plug.Factories;
using Plug.Exceptions;
using System;
using Plug.Core;
using System.Linq;

namespace Plug.Helpers
{
    internal static class Validator
    {
        internal static void Required(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        internal static void ValidateRegistration(Registration registration)
        {
            Required(registration.RegistrationType, nameof(registration.RegistrationType));
            Required(registration.InstanceType, nameof(registration.InstanceType));
            Required(registration.Factory, nameof(registration.Factory));

            if (!registration.RegistrationType.IsInterface)
            {
                throw new InvalidTypeException("Registration type must be an interface");
            }

            if (!registration.InstanceType.IsClass)
            {
                throw new InvalidTypeException("Instance type must be a class");
            }
        }

        internal static void ValidateRegistrations(Container container)
        {
            foreach (var registration in container.Registrations)
            {
                ValidateRegistration(registration);
            }
        }

        internal static void ValidateContainer(Container container)
        {
            if (container.Configuration.DeepResolution)
            {
                var dependencyGraph = new CyclicDependencyGraph<Registration>(container.Registrations.ToList());

                dependencyGraph.Sort(r => r.GetDependencies());
            }

            ValidateRegistrations(container);
        }

        internal static void ValidateInstance(object instance, Type registrationType, Type instanceType, bool strictMode)
        {
            if (instance == null)
            {
                throw new NullReferenceException("Factories cannot resolve a null instance. Check the factory to ensure the instance of the registration is not being set to null.");
            }

            if (!registrationType.IsAssignableFrom(instanceType))
            {
                throw new NotAssignableFromException(registrationType, instanceType);
            }

            if (strictMode && (instance.GetType() != instanceType))
            {
                throw new InvalidCastException("The instance created by the factory does not match the instance type defined for this registration");
            }
        }
    }
}
