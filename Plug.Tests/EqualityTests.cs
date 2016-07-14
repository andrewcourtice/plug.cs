using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.Services;

namespace Plug.Tests
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void TestSingletonFactory()
        {
            var container = new Container();

            container.Register<IMessagingService, MessagingService>(new SingletonFactory());

            var service1 = container.Resolve<IMessagingService>();
            var service2 = container.Resolve<IMessagingService>();

            Assert.IsTrue(service1 == service2);
        }

        [TestMethod]
        public void TestTransientFactory()
        {
            var container = new Container();

            container.Register<IMessagingService, MessagingService>(new TransientFactory());

            var service1 = container.Resolve<IMessagingService>();
            var service2 = container.Resolve<IMessagingService>();

            Assert.IsFalse(service1 == service2);
        }

        [TestMethod]
        public void TestCustomFactory()
        {
            //BasicEqualityTest(new TransientFactory());
        }
    }
}
