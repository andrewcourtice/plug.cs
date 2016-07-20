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
                     .Register<ITertiaryService, TertiaryService>(factory)
                     .Validate();

            var parentService = container.Resolve<IPrimaryService>();
        }
    }
}
