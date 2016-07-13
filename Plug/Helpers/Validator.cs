using Plug.Factories;
using Plug.Exceptions;
using System;

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

        internal static void ValidateRegistration(Type registrationType, Type instanceType, IFactory factory)
        {
            Required(registrationType, nameof(registrationType));
            Required(instanceType, nameof(instanceType));
            Required(factory, nameof(factory));

            if (!registrationType.IsInterface)
            {
                throw new InvalidTypeException("Registration type must be an interface");
            }

            if (!instanceType.IsClass)
            {
                throw new InvalidTypeException("Instance type must be a class");
            }
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
