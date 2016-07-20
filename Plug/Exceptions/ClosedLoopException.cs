using System;

namespace Plug.Exceptions
{
    public class ClosedLoopException<T> : Exception
    {
        public T Node { get; }

        public ClosedLoopException(T node)
            : base("A closed loop exists in the current graph. Resolve any circular dependencies and re-sort the graph.")
        {
            Node = node;
        }
    }
}
