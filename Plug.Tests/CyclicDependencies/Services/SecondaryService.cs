using System;

namespace Plug.Tests.CyclicDependencies.Services
{
    public class SecondaryService : ISecondaryService
    {
        private readonly ITertiaryService _tertiaryService;

        public SecondaryService(ITertiaryService tertiaryService)
        {
            _tertiaryService = tertiaryService;
        }

        public void DoSomethingSecondary()
        {
            throw new NotImplementedException();
        }
    }
}
