using System.Diagnostics;

namespace Plug.Tests.Performance.Services
{
    public class ChildService : IChildService
    {
        public void Communicate(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
