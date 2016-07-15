using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Tests.CustomFactories.Factories;
using Plug.Tests.CustomFactories.Services;
using System;
using System.Threading;

namespace Plug.Tests.CustomFactories
{
    [TestClass]
    public class CustomFactoryTests
    {
        [TestMethod]
        public void TestCustomFactory()
        {
            var container = new Container();

            var cacheTime = new TimeSpan(0, 0, 10);
            var factory = new CacheFactory(cacheTime);
   
            var registration = container.Register<ICustomFactoryService, CustomFactoryService>(factory);

            var service1 = container.Resolve<ICustomFactoryService>();
            Thread.Sleep(10001);
            var service2 = container.Resolve<ICustomFactoryService>();

            Assert.IsFalse(service1 == service2);
        }
    }
}
