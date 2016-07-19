using System;
using System.Collections.Generic;


namespace Plug.Core
{
    public class CyclicDependencyGraph<T>
    {
        private readonly IList<T> nodes;
        private readonly IList<T> sortedNodes;
        private readonly IDictionary<T, bool> visitedNodes;

        private Func<T, IEnumerable<T>> resolver;

        public CyclicDependencyGraph(IList<T> edges)
        {
            nodes = edges;
            sortedNodes = new List<T>();
            visitedNodes = new Dictionary<T, bool>();
        }

        private void VisitNode(T node)
        {
            if (visitedNodes[node])
            {
                throw new Exception("Cyclic Dependency Detected");
            }

            visitedNodes[node] = true;

            var dependencies = resolver(node);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    VisitNode(dependency);
                }
            }

            visitedNodes[node] = false;
            sortedNodes.Add(node);
        }

        public IList<T> Sort(Func<T, IEnumerable<T>> dependencyResolver)
        {
            resolver = dependencyResolver;

            foreach (var node in nodes)
            {
                VisitNode(node);
            }

            return sortedNodes;
        }
    }
}
