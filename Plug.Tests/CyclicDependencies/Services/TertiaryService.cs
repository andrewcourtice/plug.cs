using System;

namespace Plug.Tests.CyclicDependencies.Services
{
    public class TertiaryService : ITertiaryService
    {
        private readonly IPrimaryService _primaryService;

        public TertiaryService(IPrimaryService primaryService)
        {
            _primaryService = primaryService;
        }

        public void DoSomethingTertiary()
        {
            throw new NotImplementedException();
        }
    }
}
