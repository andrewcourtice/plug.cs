using System;

namespace Plug.Tests.Services
{
    public class MessagingService : IMessagingService
    {
        public string Message { get; set; }

        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
