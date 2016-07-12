using System;

namespace Plug.Tests.Services
{
    public class MockMessagingService : IMessagingService
    {
        public string Message { get; set; }

        public void SendMessage(string message)
        {
            Console.WriteLine($"Mock: { message }");
        }
    }
}
