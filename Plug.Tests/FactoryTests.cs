using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.Services;
using System.Diagnostics;

namespace Plug.Tests
{
    [TestClass]
    public class FactoryTests
    {

        private void BasicEqualityTest(IFactory factory)
        {
            var container = new Container();
            container.Register<IMessagingService, MessagingService>(factory);

            var service1 = container.Resolve<IMessagingService>();            
            var service2 = container.Resolve<IMessagingService>();
            
            Assert.IsTrue(service1 == service2);
        }

        [TestMethod]
        public void TestSingletonFactory()
        {
            BasicEqualityTest(new SingletonFactory());
        }

        [TestMethod]
        public void TestTransientFactory()
        {
            BasicEqualityTest(new TransientFactory());
        }
    }
}
