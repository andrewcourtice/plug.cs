using Plug.Core;

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
            return registration.HasInstance ? null : registration.InstanceConstructor(args);
        }
    }
}
