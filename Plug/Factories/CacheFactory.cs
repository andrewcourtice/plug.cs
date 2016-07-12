using System;

namespace Plug.Factories
{
    public class CacheFactory : IFactory
    {
        public TimeSpan CacheInterval { get; }

        public CacheFactory(TimeSpan cacheInterval)
        {
            CacheInterval = cacheInterval;
        }

        public void Resolve(Registration registration)
        {
            if (DateTime.UtcNow.Subtract(registration.LastResolutionDate).Ticks >= CacheInterval.Ticks)
            {
                registration.Instance = Activator.CreateInstance(registration.InstanceType);
            }
        }
    }
}
