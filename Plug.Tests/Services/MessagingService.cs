using System;

namespace Plug.Tests.Services
{
    public class MessagingService : IMessagingService
    {
        public ICommunicationsService CommunicationsService { get; }

        public MessagingService(ICommunicationsService communicationsService)
        {
            CommunicationsService = communicationsService;
        }

        public void SendMessage(string message)
        {
            CommunicationsService.Communicate(message);
        }
    }
}
