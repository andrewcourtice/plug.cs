namespace Plug.Tests.Performance.Services
{
    public interface IParentService
    {
        IChildService ChildService { get; }

        void SendMessage(string message);
    }
}
