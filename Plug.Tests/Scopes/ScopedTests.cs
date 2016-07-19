using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Tests.Scopes.Services;
using Plug.Factories;

namespace Plug.Tests.Scopes
{
    [TestClass]
    public class ScopedTests
    {
        [TestMethod]
        public void TestScope()
        {
            var scope = new Scope();
            var container = Container.NewContainer(scope);

            container.Register<IScopedService, ScopedService>(new SingletonFactory());
            container.Validate();

            var scopedService = container.Resolve<IScopedService>();

            scopedService.DoSomethingInScope();
        }
    }
}
