using Plug.Core;
using System;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating singleton classes
    /// </summary>
    public class SingletonFactory : IFactory
    {
        public virtual InstanceConstructor GenerateInstanceConstructor(Registration registration)
        {
            return Compiler.CompileInstance(registration.InstanceType);
        }

        public virtual object Resolve(Registration registration, object[] args = null)
        {
            if (registration.HasInstance)
            {
                return null;
            }

            return registration.InstanceConstructor(args);
        }
    }
}
