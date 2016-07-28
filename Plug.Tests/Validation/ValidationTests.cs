using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Factories;
using Plug.Tests.Validation.Services;

namespace Plug.Tests.Validation
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestContainerValidation()
        {
            var container = new Container();
            var factory = new SingletonFactory();

            container.Register<IValidationService, ValidationService>(factory);

            var service = container.Resolve<IValidationService>();
        }
    }
}
