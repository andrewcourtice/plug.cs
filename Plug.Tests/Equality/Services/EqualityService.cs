using System.Diagnostics;

namespace Plug.Tests.Equality.Services
{
    public class EqualityService : IEqualityService
    {
        public void Alert(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
