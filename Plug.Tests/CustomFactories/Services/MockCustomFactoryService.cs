using System.Diagnostics;

namespace Plug.Tests.CustomFactories.Services
{
    public class MockCustomFactoryService : ICustomFactoryService
    {
        public void Alert(string message)
        {
            Debug.WriteLine($"Mock: { message }");
        }
    }
}
