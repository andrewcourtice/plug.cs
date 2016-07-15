using System;
using System.Collections.Generic;
using System.Linq;
using Plug.Core;
using Plug.Factories;

namespace Plug.Tests.CustomFactories.Factories
{
    public class CacheFactory : IFactory
    {
        public TimeSpan CacheTime { get; }

        public CacheFactory(TimeSpan cacheTime)
        {
            CacheTime = cacheTime;
        }

        public InstanceConstructor GenerateInstanceConstructor(Registration registration)
        {
            return Compiler.CompileInstance(registration.InstanceType);
        }

        public object Resolve(Registration registration, object[] args = null)
        {

            if (DateTime.UtcNow.Subtract(registration.LastResolutionDate) < CacheTime)
            {
                return null;
            }

            return registration.InstanceConstructor(args);
        }
    }
}
