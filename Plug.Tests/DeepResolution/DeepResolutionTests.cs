using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.DeepResolution.Services;
using Plug.Exceptions;
using System.Diagnostics;

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
            var factory = new SingletonFactory();

            container.Register<IChildService, ChildService>(factory)
                     .Register<IParentService, ParentService>(factory)
                     .Validate();

            var parentService = container.Resolve<IParentService>();

            Assert.IsNotNull(parentService.ChildService);
        }

        [TestMethod]
        [ExpectedException(typeof(NotRegisteredException))]
        public void TestUnregisteredDeepResolution()
        {
            var container = GetContainer();

            container.Register<IParentService, ParentService>(new SingletonFactory());
            container.Validate();

            var messagingService = container.Resolve<IParentService>();

            Assert.Fail("Should not reach this point");
        }
    }
}
