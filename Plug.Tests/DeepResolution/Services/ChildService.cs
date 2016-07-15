using System.Diagnostics;

namespace Plug.Tests.DeepResolution.Services
{
    public class ChildService : IChildService
    {
        public void Communicate(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
