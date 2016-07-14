using System;
using Plug.Core;

namespace Plug.Factories
{
    public class CacheFactory : IFactory
    {
        public TimeSpan CacheInterval { get; }

        public CacheFactory(TimeSpan cacheInterval)
        {
            CacheInterval = cacheInterval;
        }

        public void Resolve(Registration registration, object[] args = null)
        {
            if (DateTime.UtcNow.Subtract(registration.LastResolutionDate).Ticks >= CacheInterval.Ticks)
            {
                var instance = ObjectActivator.GetInstance(registration.InstanceType);
                registration.Instance = instance(args);
            }
        }
    }
}
