using System;

namespace Plug.Tests.DeepResolution.Services
{
    public class ParentService : IParentService
    {
        public IChildService ChildService { get; }

        public ParentService(IChildService childService)
        {
            ChildService = childService;
        }

        public void SendMessage(string message)
        {
            ChildService.Communicate(message);
        }
    }
}
