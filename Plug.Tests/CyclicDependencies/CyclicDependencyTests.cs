using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.CyclicDependencies.Services;
using Plug.Exceptions;

namespace Plug.Tests.CyclicDependencies
{
    [TestClass]
    public class CyclicDependencyTests
    {
        /// <summary>
        /// Testing for the existence of a closed loop in the container
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ClosedLoopException<Registration>))]
        public void TestCyclicDependecy()
        {
            var configuration = new ContainerConfiguration()
            {
                DeepResolution = true
            };

            var container = new Container(configuration);
            var factory = new SingletonFactory();

            container.Register<IPrimaryService, PrimaryService>(factory)
                     .Register<ISecondaryService, SecondaryService>(factory)

                     // Tertiary service has a reference to primary service.
                     // This is a closed loop and should cause a ClosedLoopException
                     // when validating the container
                     .Register<ITertiaryService, TertiaryService>(factory)
                     .Validate();

            var parentService = container.Resolve<IPrimaryService>();
        }
    }
}
