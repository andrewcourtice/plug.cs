using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.Services;
using Plug.Exceptions;

namespace Plug.Tests
{
    [TestClass]
    public class DeepResolutionTests
    {
        private Container GetContainer()
        {
            var configuration = new ContainerConfiguration()
            {
                DeepResolution = true
            };

            return new Container(configuration);
        }

        [TestMethod]
        public void TestRegisteredDeepResolution()
        {
            var container = GetContainer();

            container.Register<ICommunicationsService, CommunicationsService>(new SingletonFactory());
            container.Register<IMessagingService, MessagingService>(new SingletonFactory());

            var messagingService = container.Resolve<IMessagingService>();

            Assert.IsNotNull(messagingService.CommunicationsService);
        }

        [TestMethod]
        [ExpectedException(typeof(NotRegisteredException))]
        public void TestUnregisteredDeepResolution()
        {
            var container = GetContainer();

            container.Register<IMessagingService, MessagingService>(new SingletonFactory());

            var messagingService = container.Resolve<IMessagingService>();

            Assert.Fail("Should not reach this point");
        }
    }
}
