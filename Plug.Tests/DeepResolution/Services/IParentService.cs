namespace Plug.Tests.DeepResolution.Services
{
    public interface IParentService
    {
        IChildService ChildService { get; }

        void SendMessage(string message);
    }
}
