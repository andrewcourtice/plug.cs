using Plug.Core;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating singleton classes
    /// </summary>
    public class SingletonFactory : IFactory
    {
        public virtual void Resolve(Registration registration, object[] args = null)
        {
            if (registration.Instance == null)
            {
                var instance = ObjectActivator.GetInstance(registration.InstanceType);
                registration.Instance = (args == null ? instance() : instance(args));
            }
        }
    }
}
