using System;

namespace Plug.Tests.Services
{
    public class MockMessagingService : IMessagingService
    {
        public ICommunicationsService CommunicationsService { get; }

        public void SendMessage(string message)
        {
            Console.WriteLine($"Mock: { message }");
        }
    }
}
