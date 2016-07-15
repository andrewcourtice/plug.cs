using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Tests.Performance.Services;
using Plug.Factories;
using System.Diagnostics;

namespace Plug.Tests.Performance
{
    [TestClass]
    public class PerformanceTests
    {
        private TimeSpan GetPerformanceTime(int iterations, ContainerConfiguration configuration, IFactory factory)
        {
            var container = new Container(configuration);

            container.Register<IChildService, ChildService>(factory);
            container.Register<IParentService, ParentService>(factory);

            var stopWatch = Stopwatch.StartNew();

            for (var i = 0; i < iterations; i++)
            {
                var service = container.Resolve<IParentService>();
            }    
               
            return stopWatch.Elapsed;
        }

        private void RunPerformanceTest(ContainerConfiguration configuration, IFactory factory)
        {
            var baseIterations = 100000;
            var gainFactor = 50;
            var finalIterations = baseIterations + (int)(decimal.Divide(baseIterations, 100) * gainFactor);

            Debug.WriteLine($"Deep resolution enabled: { configuration.DeepResolution }");

            var baseTime = GetPerformanceTime(baseIterations, configuration, factory);
            Debug.WriteLine($"{ factory.GetType().Name } @ { baseIterations } iterations: { baseTime }");

            var predictedFinalTicks = baseTime.Ticks + (decimal.Divide(gainFactor, 100) * baseTime.Ticks);
            var predictedFinalTime = new TimeSpan((long) predictedFinalTicks);
            Debug.WriteLine($"Predicted final time: { predictedFinalTime }");

            var actualFinalTime = GetPerformanceTime(finalIterations, configuration, factory);
            Debug.WriteLine($"{ factory.GetType().Name } @ { finalIterations } iterations: { actualFinalTime }");

            var timeDifferenceFactor = gainFactor - (decimal.Divide(actualFinalTime.Ticks - baseTime.Ticks, baseTime.Ticks) * 100);
            
            Debug.WriteLine($"Workload increase: { gainFactor }%");
            Debug.WriteLine($"Time difference factor: { Math.Round(timeDifferenceFactor, 2) }%");
            Debug.WriteLine(new string('=', 50));
        }

        [TestMethod]
        public void TestSingletonPerformance()
        {
            RunPerformanceTest(new ContainerConfiguration(), new SingletonFactory());
        }

        [TestMethod]
        public void TestTransientPerformance()
        {
            RunPerformanceTest(new ContainerConfiguration(), new TransientFactory());
        }

        [TestMethod]
        public void TestNestedSingletonPerformance()
        {
            var configuration = new ContainerConfiguration()
            {
                DeepResolution = true
            };

            RunPerformanceTest(configuration, new SingletonFactory());
        }

        [TestMethod]
        public void TestNestedTransientPerformance()
        {
            var configuration = new ContainerConfiguration()
            {
                DeepResolution = true
            };

            RunPerformanceTest(configuration, new TransientFactory());
        }
    }
}
