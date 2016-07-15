using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Tests.CustomFactories.Factories;
using Plug.Tests.CustomFactories.Services;
using System.Text;

namespace Plug.Tests.CustomFactories
{
    [TestClass]
    public class CustomFactoryTests
    {
        [TestMethod]
        public void TestCustomFactory()
        {
            var container = new Container();
            var factory = new CustomFactory();
   
            var registration = container.Register<ICustomFactoryService, CustomFactoryService>(factory);
            registration.Update(typeof(MockCustomFactoryService));

            var service = container.Resolve<ICustomFactoryService>();

            Assert.IsTrue(service.GetType() == typeof(MockCustomFactoryService));
        }
    }
}
