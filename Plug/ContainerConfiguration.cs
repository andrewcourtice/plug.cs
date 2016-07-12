using Plug.Factories;
using System;

namespace Plug
{
    public class ContainerConfiguration
    {
        public int ConcurrencyLevel { get; set; }
        public IFactory DefaultFactory { get; set; }

        public ContainerConfiguration()
        {
            ConcurrencyLevel = Environment.ProcessorCount * 2;
            DefaultFactory = new SingletonFactory();
        }
    }
}
