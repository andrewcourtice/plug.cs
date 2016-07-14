using System;
using System.Diagnostics;

namespace Plug.Tests.Services
{
    public class CommunicationsService : ICommunicationsService
    {
        public void Communicate(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
