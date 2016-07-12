using System;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating transient instances of a registration
    /// </summary>
    public class TransientFactory : IFactory
    {
        public virtual void Resolve(Registration registration)
        {
            registration.Instance = Activator.CreateInstance(registration.InstanceType);
        }
    }
}
