using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.Equality.Services;

namespace Plug.Tests.Equality
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void TestSingletonFactory()
        {
            var container = new Container();

            container.Register<IEqualityService, EqualityService>(new SingletonFactory());
            container.Validate();

            var service1 = container.Resolve<IEqualityService>();
            var service2 = container.Resolve<IEqualityService>();

            Assert.IsTrue(service1 == service2);
        }

        [TestMethod]
        public void TestTransientFactory()
        {
            var container = new Container();

            container.Register<IEqualityService, EqualityService>(new TransientFactory());
            container.Validate();

            var service1 = container.Resolve<IEqualityService>();
            var service2 = container.Resolve<IEqualityService>();

            Assert.IsFalse(service1 == service2);
        }
    }
}
