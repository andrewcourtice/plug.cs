using System;
using System.Diagnostics;

namespace Plug.Tests.Scopes.Services
{
    public class ScopedService : IScopedService
    {
        public void DoSomethingInScope()
        {
            Debug.WriteLine(AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
