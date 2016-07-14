using Plug.Core;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating transient instances of a registration
    /// </summary>
    public class TransientFactory : IFactory
    {
        public virtual void Resolve(Registration registration, object[] args = null)
        {
            var instance = ObjectActivator.GetInstance(registration.InstanceType);
            registration.Instance = instance(args);
        }
    }
}
