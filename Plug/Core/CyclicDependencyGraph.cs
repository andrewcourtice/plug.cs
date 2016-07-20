using Plug.Exceptions;
using System;
using System.Collections.Generic;


namespace Plug.Core
{
    public class CyclicDependencyGraph<T>
    {
        private readonly IList<T> _nodes;
        private readonly IList<T> _sortedNodes;
        private readonly IDictionary<T, bool> _visitedNodes;

        private Func<T, IEnumerable<T>> resolver;

        public CyclicDependencyGraph(IList<T> nodes)
        {
            _nodes = nodes;
            _sortedNodes = new List<T>();
            _visitedNodes = new Dictionary<T, bool>();
        }

        private void VisitNode(T node)
        {
            bool visiting;
            var visited = _visitedNodes.TryGetValue(node, out visiting);

            if (visited && visiting)
            {
                throw new ClosedLoopException<T>(node);
            }

            _visitedNodes[node] = true;

            var dependencies = resolver(node);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    VisitNode(dependency);
                }
            }

            _visitedNodes[node] = false;
            _sortedNodes.Add(node);
        }

        public IList<T> Sort(Func<T, IEnumerable<T>> dependencyResolver)
        {
            resolver = dependencyResolver;

            foreach (var node in _nodes)
            {
                VisitNode(node);
            }

            return _sortedNodes;
        }
    }
}
