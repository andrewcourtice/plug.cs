using System.Diagnostics;

namespace Plug.Tests.CustomFactories.Services
{
    public class CustomFactoryService : ICustomFactoryService
    {
        public void Alert(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
