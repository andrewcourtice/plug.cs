
using Plug.Core;
using System;

namespace Plug.Factories
{
    public interface IFactory
    {
        InstanceConstructor GenerateInstanceConstructor(Registration registration);

        /// <summary>
        /// Resolve an instance of a registration
        /// </summary>
        /// <typeparam name="T">The dependency type of the registration (the interface type)</typeparam>
        /// <param name="registration">The registration to resolve</param>
        /// <returns>An instance of the registration</returns>
        object Resolve(Registration registration, object[] args = null);
    }
}
