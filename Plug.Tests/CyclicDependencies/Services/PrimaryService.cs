using System;

namespace Plug.Tests.CyclicDependencies.Services
{
    public class PrimaryService : IPrimaryService
    {
        private readonly ISecondaryService _secondaryService;

        public PrimaryService(ISecondaryService secondaryService)
        {
            _secondaryService = secondaryService;
        }

        public void DoSomethingPrimary()
        {
            throw new NotImplementedException();
        }
    }
}
