using Plug.Core;
using System;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating transient instances of a registration
    /// </summary>
    public class TransientFactory : MarshalByRefObject, IFactory
    {
        public virtual InstanceConstructor GenerateInstanceConstructor(Registration registration)
        {
            return Compiler.CompileInstance(registration.InstanceType);
        }

        public virtual object Resolve(Registration registration, object[] args = null)
        {
            return registration.InstanceConstructor(args);
        }
    }
}
