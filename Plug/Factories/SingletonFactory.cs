using Plug.Core;

namespace Plug.Factories
{
    /// <summary>
    /// A simple factory for creating singleton classes
    /// </summary>
    public class SingletonFactory : IFactory
    {
        public virtual void Resolve(Registration registration)
        {
            if (registration.Instance == null)
            {
                var instance = ObjectActivator.GetInstance(registration.InstanceType);
                registration.Instance = instance();
            }
        }
    }
}
