namespace Plug.Tests.Services
{
    public interface IMessagingService
    {
        ICommunicationsService CommunicationsService { get; }

        void SendMessage(string message);
    }
}
