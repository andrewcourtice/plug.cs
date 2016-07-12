namespace Plug.Tests.Services
{
    internal interface IMessagingService
    {
        string Message { get; set; }

        void SendMessage(string message);
    }
}
